class Piece {
    public string name {get; set;}
    public int ownedByPlayer { get; set; }
    public Piece(){
    }
    public Piece(string name){
        this.name = name;
    }
    public Piece(string name, int ownedByPlayer){
        this.name = name;
        this.ownedByPlayer = ownedByPlayer;
    }


}