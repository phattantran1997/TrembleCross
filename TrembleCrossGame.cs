using System.Xml;

class TrembleCrossGame : IGame
{
    public List<Player> players { get; set; } = [];
    public int turn { get; set; }
    public Board? gameCurrentState { get; set; } = null;
    public bool isDraw { get; set; } = false;
    public List<Board> listMoveHistories { get; set; }
    public Player? winner { get; set; }
    public string pathFileName { get; set; }
    public bool isPlayWithHuman { get; set; }
    public TrembleCrossGame(int boardSize, bool isPlayWithHuman)
    {
        // pathFileName = "/TrembleCross/game.json";
        // GameController gameController = new GameController();

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


        this.turn = 0;
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
        this.gameCurrentState = stringToBoard(board);
        // Add initial board to move history
        listMoveHistories = [this.gameCurrentState];
    }
    // public void play(bool isPlayWithHuman)
    // {
    //     int currentPlayer = 1;
    //     int userInput =0 ;
    //     while (true)
    //     {

    //         Console.WriteLine($"👉 Player {currentPlayer}'s turn - please input your cell want to play:");
    //         if(isPlayWithHuman)
    //             userInput = int.TryParse(Console.ReadLine(), out userInput);
    //         else{

    //         }
    //         if (!checkuserInput)
    //         {
    //             Console.WriteLine("⛳️ Invalid input format. Please enter your move again. ⛳️");
    //             continue;
    //         }
    //         else
    //         {
    //             if(!this.gameCurrentState.updateCells(userInput, players.FirstOrDefault(p => p.PlayerID == currentPlayer).piece)){
    //                 Console.WriteLine("This cell is already occupied. Please choose another cell.");
    //                 continue;
    //             }
    //         }
    //         listMoveHistories.Add(gameCurrentState);
    //         Console.WriteLine(this.gameCurrentState.formatTable());

    //         if(!processWinner(userInput))
    //         {

    //             currentPlayer = currentPlayer == 1 ? 2 : 1;

    //         }else{
    //             break;
    //         }    

    //     }
    // }

    public void play(bool isPlayWithHuman)
    {
    int currentPlayer = 1;
    int userInput = 0;
    Random random = new Random(); // Random number generator for computer's move

    while (true)
    {
        Console.WriteLine($"👉 Player {currentPlayer}'s turn - please input your cell to play:");
        Console.WriteLine(this.gameCurrentState.formatTable());

        if (!isPlayWithHuman && currentPlayer == 2)
        {  
            do
            {
                //random until valid Value
                userInput = random.Next(0, this.gameCurrentState.cells.Count);
            } while (!this.gameCurrentState.checkNotConflictCells(userInput));
        }
        else if (!int.TryParse(Console.ReadLine(), out userInput))
        {
            Console.WriteLine("⛳️ Invalid input format. Please enter your move again. ⛳️");
            continue;
        }

        if (userInput == -1){
            // save game
            break;
        }
        if (!this.gameCurrentState.checkNotConflictCells(userInput))
        {            
            Console.WriteLine("⛳️ This cell is already occupied. Please choose another cell. ⛳️");
            continue;
        }

        this.gameCurrentState.updateCells(userInput, players.FirstOrDefault(p => p.PlayerID == currentPlayer).piece);
        listMoveHistories.Add(this.gameCurrentState); // Save game state

        if (processWinner())
        {
            winner = this.players.FirstOrDefault(item => item.PlayerID == this.gameCurrentState.cells[userInput].valuePiece.ownedByPlayer);
            Console.WriteLine($"🎉 Player {currentPlayer} wins! 🎉");
            break;
        }
        currentPlayer = currentPlayer == 1 ? 2 : 1;
        
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
    public Board stringToBoard(string board)
    {
        // return null;
        string[] allPositions = board.Split(",");


        Board loadBoard = new Board(1, allPositions.Length);
        for (int i = 0; i < allPositions.Length; i++)
        {
            if (allPositions[i] == "X")
            {
                loadBoard.updateCells(i,"X");
            }
        }
        return loadBoard;
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

    public void saveFile()
    {

        throw new NotImplementedException();
    }

    public void start()
    {
        throw new NotImplementedException();
    }

    public void finish()
    {
        throw new NotImplementedException();
    }


}
