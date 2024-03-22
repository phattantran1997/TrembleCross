class Player{
    private string Name {get; set;}
    public int PlayerID {get;set;}
    private int Score {get; set;}

    public Piece piece{get;set;}

    public Player(string name, int playerID){
        this.Name = name;
        this.PlayerID = playerID;
    }
}

