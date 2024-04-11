interface IPlayer{
    public string Name {get; set;}
    public int PlayerID {get;set;}
    public int Score {get; set;}

    public Piece piece{get;set;}
    
    public string makeMove(Board gameCurrentState);
}
class HumanPlayer: IPlayer{
    public string Name {get; set;}
    public int PlayerID {get;set;}
    public int Score {get; set;}

    public Piece piece{get;set;}

    public HumanPlayer(string name, int playerID){
        this.Name = name;
        this.PlayerID = playerID;
    }
    public string makeMove(Board gameCurrentState){
        string input = Console.ReadLine();

        if(int.Parse(input) >= 0 && int.Parse(input) < gameCurrentState.cells.Count){
            return input;
        }
        else if(input.ToUpper() == "U" || input.ToUpper() == "R" || input.ToUpper() == "P" || input.ToUpper() == "S"){
            return input.ToUpper();
        }
        else{
            System.Console.WriteLine("Invalid Input please try again");
            return makeMove(gameCurrentState);
        }
    }
}
class ComputerPlayer : IPlayer
{
    public string Name {get; set;}
    public int PlayerID {get;set;}
    public int Score {get; set;}

    public Piece piece{get;set;}
    public ComputerPlayer(string name, int playerID)
    {
        this.Name = name;
        this.PlayerID = playerID;
    }
    
    public string makeMove(Board gameCurrentState){
        int userInput;
        Random random = new Random(); 
        do
        {
            // Random until valid value
            userInput = random.Next(0, gameCurrentState.cells.Count);
        } while (!gameCurrentState.checkNotConflictCells(userInput));
        return userInput.ToString();
    }
}
