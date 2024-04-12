interface IGame
{
    List<IPlayer> players { get; set; }
    int turn { get; set; }
    Board gameCurrentState { get; set; }
    List<Board> listMoveHistories { get; set; }
    IPlayer winner { get; set; }
    string gameType{get;}
    Stack<Board> redoGameState {get; set;} 

    void loadGame(GameFile selectedGame);
    void saveGame();
    void redo();
    void undo();
    void play();

}