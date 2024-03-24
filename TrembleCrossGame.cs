using System.Text.Json;
using System.Xml;
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

    public List<Player> players { get; set; } = [];
    public Board? gameCurrentState { get; set; } = null;
    public bool isDraw { get; set; } = false;
    public List<Board> listMoveHistories { get; set; }
    public Player? winner { get; set; }
    public bool isPlayWithHuman { get; set; }
    public int turn { get; set; }

    public TrembleCrossGame()
    {
    }
    public TrembleCrossGame(int boardSize, bool isPlayWithHuman)
    {

        Player player1 = new Player("player1", 1);
        player1.piece = new Piece("X", player1.PlayerID);
        players.Add(player1);
        if (isPlayWithHuman)
        {
            //human player
            Player player2 = new Player("player2", 2);
            player2.piece = new Piece("X", player2.PlayerID);
            players.Add(player2);
        }
        else
        {
            //computer
            Player computerPlayer = new Player("computer", 2);
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
        Player player1 = new Player("player1", 1);
        player1.piece = new Piece("X", player1.PlayerID);
        players.Add(player1);
        this.isPlayWithHuman = isPlayWithHuman;
        if (isPlayWithHuman)
        {
            //human player
            Player player2 = new Player("player2", 2);
            player2.piece = new Piece("X", player2.PlayerID);
            players.Add(player2);
        }
        else
        {
            //computer
            Player computerPlayer = new Player("computer", 2);
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
        int userInput = 0;
        Random random = new Random(); // Random number generator for computer's move

        while (true)
        {
            if (!this.isPlayWithHuman && this.turn == 2)
            {
                Console.WriteLine($"👉 Player computer is playing ....");
                do
                {
                    // Random until valid value
                    userInput = random.Next(0, this.gameCurrentState.cells.Count);
                } while (!this.gameCurrentState.checkNotConflictCells(userInput));
            }
            else
            {
                Console.WriteLine($"👉 Player {this.turn}'s turn - please input your cell to play. \nor input 'S' to save the game state \n'U' to undo a step \n'R' to redo a step \n'P' to print the table");
                string input = Console.ReadLine(); // Read user input

                // Convert input to integer if possible
                if (int.TryParse(input, out userInput))
                {
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
                    Console.WriteLine("Game state saved.");
                    break;
                }
                else if (input == "U")
                {
                    // Undo step
                    if (listMoveHistories.Count > 1)
                    {
                        listMoveHistories.RemoveAt(listMoveHistories.Count - 1);
                        gameCurrentState = listMoveHistories.LastOrDefault();
                        Console.WriteLine("Undo step.");
                    }
                    else
                    {
                        Console.WriteLine("Cannot undo further.");
                    }
                    continue;
                }
                else if (input == "R")
                {
                    // Redo step
                    // Implement redo step logic if needed
                    Console.WriteLine("Redo step.");
                    continue;
                }
                else if (input == "P")
                {
                    // Redo step
                    // Implement redo step logic if needed
                    Console.WriteLine(this.gameCurrentState.formatTable());
                    continue;
                }


            }


            if (!this.gameCurrentState.checkNotConflictCells(userInput))
            {
                Console.WriteLine("⛳️ This cell is already occupied. Please choose another cell. ⛳️");
                continue;
            }

            this.gameCurrentState.updateCells(userInput, players.FirstOrDefault(p => p.PlayerID == this.turn).piece);
            listMoveHistories.Add(this.gameCurrentState); // Save game state
            Console.WriteLine(this.gameCurrentState.formatTable());

            if (processWinner())
            {
                winner = this.players.FirstOrDefault(item => item.PlayerID == this.gameCurrentState.cells[userInput].valuePiece.ownedByPlayer);
                Console.WriteLine($"🎉 Player {winner.Name} wins! 🎉");
                break;
            }
            this.turn = this.turn == 1 ? 2 : 1;
        }
    }

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

    public void redo()
    {
        throw new NotImplementedException();
    }

    public void undo()
    {
        throw new NotImplementedException();
    }

    public IGame loadGame()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "TrembleCross", "game.json");
        string jsonContent = File.ReadAllText(filePath);
        GameData gameData = JsonSerializer.Deserialize<GameData>(jsonContent);

        // Display available games
        Console.WriteLine("Available Games:");
        for (int i = 0; i < gameData.files.Count; i++)
        {
            Console.WriteLine($"Game {i + 1}: Board - {gameData.files[i].Board}, Turn - {gameData.files[i].Turn}, Humans - {gameData.files[i].Humans}");
        }

        // Ask user to choose a game
        Console.WriteLine("Enter the number of the game you want to load:");
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > gameData.files.Count)
        {
            Console.WriteLine("Invalid input. Please enter a valid game number:");
        }

        // Load the selected game
        var selectedGame = gameData.files[choice - 1];
        var newTrembleCrossGame = new TrembleCrossGame(selectedGame.Board, selectedGame.Turn, selectedGame.Humans);
        Console.WriteLine("Game loaded successfully.");
        Console.WriteLine(newTrembleCrossGame.gameCurrentState.formatTable());
        newTrembleCrossGame.play();

        return newTrembleCrossGame;
    }


    public void saveGame()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "TrembleCross", "game.json");

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

        // Add the new game data
        TrembleCrossGame trembleCrossGame =  new TrembleCrossGame();
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
    }
}
