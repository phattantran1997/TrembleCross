interface IGame{

    List<Player> players {get; set;}
    int turn {get; set;}
    Board gameCurrentState {get; set;}
    List<Board> moveHistory {get; set;}
    Player winner {get; set;}
    
}