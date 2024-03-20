using System;
using System.Security;
class Program{

    static void Main(string[] args)
    {
        string input = "";
        GameController controller =new GameController();

        while(true){
            if(input =="0")
                break;
            Console.WriteLine("Do you want to load previous game or create new game? \n 1. Load game \n 2. Create new game");
            input = Console.ReadLine();
            if(input == "1"){
                controller.loadGame();
            }
            Console.WriteLine("What game do you play? \n 1. GameA \n 2. GameB");
            input = Console.ReadLine();
            if(input == "gameA"){
                TrembleCrossGame trembleCross = new TrembleCrossGame(10);
                trembleCross.start();
                System.Console.WriteLine(trembleCross.ReturnBoardState());
            }else{

            }
        }
       

        
    }
}


class TrembleCrossGame : IGame{
    public List<Player> players {get;set;}
    public int turn {get; set;}
    public Board gameCurrentState {get;set;}
    public bool isDraw {get;set; } = false;
    public List<Board> moveHistory {get;set;}
    public Player? winner { get; set; }

    public TrembleCrossGame(int boardSize){
        // Assuming only 2 players
        players = new List<Player>();
        players.Add(new Player("Tiu",1));
        players.Add(new Player("Dat",2));

        //Set first turn to be 0
        this.turn = 0;

        // Set boardSize to be nth 
        this.gameCurrentState = new Board(1,boardSize);

        // Add initial board to move history
        moveHistory = new List<Board>();
        moveHistory.Add(this.gameCurrentState);

    }

     public TrembleCrossGame(int boardSize, List<Player> players){
        // Assuming only 2 players
        this.players = players;

        //Set first turn to be 0
        this.turn = 0;

        // Set boardSize to be nth 
        this.gameCurrentState = new Board(1,boardSize);

        // Add initial board to move history
        moveHistory = new List<Board>();
        moveHistory.Add(this.gameCurrentState);

    }
    public string ReturnBoardState(){
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

