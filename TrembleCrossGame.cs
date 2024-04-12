using System.Text.Json;
public class GameFile
{
    public string Board { get; set; }
    public int Turn { get; set; }
    public bool Humans { get; set; }
}
public class GameData
{
    public List<GameFile> files { get; set; }
}
class TrembleCrossGame : IGame
{

    public List<IPlayer> players { get; set; } = [];
    public Board? gameCurrentState { get; set; } = null;
    public List<Board> listMoveHistories { get; set; }
    public IPlayer? winner { get; set; }
    public bool isPlayWithHuman { get; set; }
    public int turn { get; set; }
    public bool isDraw { get; set; } = false;
    public Stack<Board> redoGameState { get; set; } = new Stack<Board>();
    public string gameType { get; } = "TrembleCross";

    public TrembleCrossGame()
    {
    }
    public TrembleCrossGame(int boardSize, bool isPlayWithHuman)
    {

        IPlayer player1 = new HumanPlayer("player1", 1);
        player1.piece = new Piece("X", player1.PlayerID);
        players.Add(player1);
        if (isPlayWithHuman)
        {
            //human player
            IPlayer player2 = new HumanPlayer("player2", 2);
            player2.piece = new Piece("X", player2.PlayerID);
            players.Add(player2);
        }
        else
        {
            //computer
            IPlayer computerPlayer = new ComputerPlayer("computer", 2);
            computerPlayer.piece = new Piece("X", computerPlayer.PlayerID);
            players.Add(computerPlayer);
        }


        this.turn = 1;
        this.isPlayWithHuman = isPlayWithHuman;
        this.gameCurrentState = new Board(1, boardSize);
        // Add initial board to move history
        listMoveHistories = [this.gameCurrentState];
    }
    public TrembleCrossGame(TrembleCrossGame existingGame)
    {
        this.players = existingGame.players;

    }
    public TrembleCrossGame(string board, int turn, bool isPlayWithHuman)
    {
        IPlayer player1 = new HumanPlayer("player1", 1);
        player1.piece = new Piece("X", player1.PlayerID);
        players.Add(player1);
        this.isPlayWithHuman = isPlayWithHuman;
        if (isPlayWithHuman)
        {
            // Add Human player
            IPlayer player2 = new HumanPlayer("player2", 2);
            player2.piece = new Piece("X", player2.PlayerID);
            players.Add(player2);
        }
        else
        {
            // Add Computer player
            IPlayer computerPlayer = new ComputerPlayer("computer", 2);
            computerPlayer.piece = new Piece("X", computerPlayer.PlayerID);
            players.Add(computerPlayer);
        }


        this.turn = turn;
        stringToBoard(board);
        // Add initial board to move history
        listMoveHistories = [this.gameCurrentState];
    }



    public void play()
    {
        string gameRule = "Players takes turns placing a piece 'X'. The first player that makes 3 consecutive X wins";
        string gameCommand = $"You can input 0-{this.gameCurrentState.cells.Count - 1} to make a move \nYou can press 's' to save game\nYou can press 'u' to undo a move\nYou can press 'r' to redo your move\nYou can press 'p' to display current board state";

        HelpSystem helpSystem = new HelpSystem(gameRule, gameCommand);
        int userInput = 0;
        // Random random = new Random(); // Random number generator for computer's move

        while (true)
        {
            if (!this.isPlayWithHuman && this.turn == 2)
            {
                Console.WriteLine($"\n👉 Player computer is playing ....");
                ComputerPlayer computerPlayer = (ComputerPlayer)players[this.turn - 1];
                userInput = int.Parse(computerPlayer.makeMove(gameCurrentState));
            }
            else
            {
                Console.WriteLine($"\n👉 Player {players[turn - 1].PlayerID} is playing ....");

                helpSystem.show();
                string input = players[turn - 1].makeMove(gameCurrentState); // Read user input

                // Convert input to integer if possible
                if (int.TryParse(input, out userInput))
                {
                    if (input == "-1")
                    {
                        System.Console.WriteLine("Invalid input");
                        continue;
                    }
                    userInput = int.Parse(input);
                }
                else
                {
                    // Non-numeric input
                    input = input.ToUpper(); // Convert to uppercase
                }

                if (input == "S")
                {
                    // Save game
                    saveGame();
                    break;
                }
                else if (input == "U")
                {
                    undo();
                    continue;
                }
                else if (input == "R")
                {
                    redo();
                    continue;
                }
                else if (input == "P")
                {
                    // Display the board's current state
                    PrintBoardState();
                    continue;
                }
            }


            // Check if move is valid
            if (!this.gameCurrentState.checkNotConflictCells(userInput))
            {
                Console.WriteLine("⛳️ This cell is already occupied. Please choose another cell. ⛳️");
                PrintBoardState();
                continue;
            }

            // Update board
            this.gameCurrentState = new Board(this.gameCurrentState);
            makeMove(userInput);

            // Check for a possible winner
            if (processWinner())
            {
                winner = this.players.FirstOrDefault(item => item.PlayerID == this.gameCurrentState.cells[userInput].valuePiece.ownedByPlayer);
                Console.WriteLine($"\n 🎉 Player {winner.Name} wins! 🎉 \n");
                PrintBoardState();
                break;
            }
            PrintBoardState();
            // Switch turns
            this.turn = this.turn == 1 ? 2 : 1;
        }
    }

    // Check for winner
    private bool processWinner()
    {
        for (int i = 0; i < this.gameCurrentState.cells.Count - 2; i++)
        {
            if (!string.IsNullOrEmpty(this.gameCurrentState.cells[i].valuePiece.name) && this.gameCurrentState.cells[i].valuePiece.name == this.gameCurrentState.cells[i + 1].valuePiece.name && this.gameCurrentState.cells[i].valuePiece.name == this.gameCurrentState.cells[i + 2].valuePiece.name)
            {
                return true; // Found three consecutive items
            }
        }
        return false; // No three consecutive items found
    }

    // Convert from String to Board class for Json file
    public void stringToBoard(string board)
    {
        // return null;
        string[] allPositions = board.Split(",");
        this.gameCurrentState = new Board(1, allPositions.Length);
        for (int i = 0; i < allPositions.Length; i++)
        {
            if (allPositions[i] == "X")
            {
                gameCurrentState.updateCells(i, "X");
            }
        }
    }

    // Return Board State
    public string ReturnBoardState()
    {
        return this.gameCurrentState.formatTable();
    }

    // Print Board State
    public void PrintBoardState()
    {
        System.Console.WriteLine("\n" + ReturnBoardState());
    }

    // Update board
    public void makeMove(int userInput)
    {
        // Update the cell: Given by user. Where User has the same PlayerID 
        this.gameCurrentState.updateCells(userInput, players.FirstOrDefault(p => p.PlayerID == this.turn).piece);
        listMoveHistories.Add(new Board(this.gameCurrentState)); // Save game state
    }

    // Redo previous move
    public void redo()
    {
        if (this.redoGameState.Count != 0)
        {
            listMoveHistories.Add(this.redoGameState.Pop());
            listMoveHistories.Add(this.redoGameState.Pop());
            gameCurrentState = listMoveHistories.LastOrDefault();
            // this.redoGameState= new Stack<Board>();
            // listMoveHistories.Add(new Board(this.gameCurrentState));
            Console.WriteLine("Redo step complete.");
            PrintBoardState();
        }
    }

    // Undo move
    public void undo()
    {
        // Undo step
        if (listMoveHistories.Count > 2)
        {
            this.redoGameState.Push(listMoveHistories.LastOrDefault());
            listMoveHistories.RemoveAt(listMoveHistories.Count - 1);
            this.redoGameState.Push(listMoveHistories.LastOrDefault());
            listMoveHistories.RemoveAt(listMoveHistories.Count - 1);
            gameCurrentState = listMoveHistories.LastOrDefault();
            Console.WriteLine("Undo step complete.");
            PrintBoardState();
        }
        else
        {
            Console.WriteLine("Cannot undo further.");
        }

    }

    // Load game
    public void loadGame(GameFile selectedGame)
    {
        var newTrembleCrossGame = new TrembleCrossGame(selectedGame.Board, selectedGame.Turn, selectedGame.Humans);
        Console.WriteLine("Game loaded successfully.");
        newTrembleCrossGame.PrintBoardState();
        newTrembleCrossGame.play();
    }


    // Save current state of the game
    public void saveGame()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"{gameType}", "game.json");

        // Deserialize existing data from file
        GameData existingData;
        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            existingData = JsonSerializer.Deserialize<GameData>(existingJson);
        }
        else
        {
            existingData = new GameData { files = new List<GameFile>() };
        }

        // Format all the data
        existingData.files.Add(new GameFile
        {
            Board = this.gameCurrentState.ToString(),
            Turn = this.turn,
            Humans = this.isPlayWithHuman
        });

        // Serialize the updated data
        string json = JsonSerializer.Serialize(existingData);

        // Write it to file
        File.WriteAllText(filePath, json);
        Console.WriteLine("Game state saved.");
    }
}
