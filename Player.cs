class Player{
    public string Name {get; set;}
    public int PlayerID {get;set;}
    public int Score {get; set;}

    public Piece piece{get;set;}

    public Player(string name, int playerID){
        this.Name = name;
        this.PlayerID = playerID;
    }
}

