class TrembleCrossGame : IGame
{
    public List<Player> players { get; set; } = [];
    public int turn { get; set; }
    public Board? gameCurrentState { get; set; } = null;
    public bool isDraw { get; set; } = false;
    public List<Board> listMoveHistories { get; set; }
    public Player? winner { get; set; }
    public string pathFileName { get; set; }
    public TrembleCrossGame(int boardSize, bool isPlayWithHuman)
    {
        pathFileName = "/TrembleCross/game.json";
        GameController gameController = new GameController();
        Player player1 = new Player("player1", 1);
        player1.piece = new Piece("X", player1.PlayerID);
        players.Add(player1);
        if (isPlayWithHuman)
        {
            Player player2 = new Player("player2", 2);
            player2.piece = new Piece("X", player2.PlayerID);
            players.Add(player2);
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
    public TrembleCrossGame(string board, int turn)
    {

        this.gameCurrentState = stringToBoard(board);
        this.turn = turn;
    }
    public void play()
    {
        int currentPlayer = 1;
        while (true)
        {
            Console.WriteLine($"Player {currentPlayer}'s turn - please input your cell want to play:");
            bool checkuserInput = int.TryParse(Console.ReadLine(), out int userInput);
            if (!checkuserInput)
            {
                Console.WriteLine("Invalid input format. Please enter your move again.");
                continue;
            }
            else
            {
                if(!this.gameCurrentState.updateCells(userInput, players.FirstOrDefault(p => p.PlayerID == currentPlayer).piece)){
                    Console.WriteLine("This cell is already occupied. Please choose another cell.");
                    continue;
                }
            }
            listMoveHistories.Add(gameCurrentState);
            Console.WriteLine(this.gameCurrentState.formatTable());
            currentPlayer = currentPlayer == 1 ? 2 : 1;

        }
        throw new NotImplementedException();
    }

    // Convert from String to Board class for Json file
    public Board stringToBoard(string board)
    {
        return null;
        // string[] allPositions = board.Split(",");


        // Board loadBoard = new Board(1, allPositions.Length);
        // for (int i = 0; i < allPositions.Length; i++)
        // {
        //     if (allPositions[i] == "X")
        //     {
        //         loadBoard.updateCells(i);
        //     }
        // }
        // return loadBoard;
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
