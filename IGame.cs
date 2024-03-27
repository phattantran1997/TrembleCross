interface IGame
{

    List<Player> players { get; set; }
    int turn { get; set; }
    Board gameCurrentState { get; set; }
    List<Board> listMoveHistories { get; set; }
    Player winner { get; set; }
    void loadGame(GameFile selectedGame);
    void saveGame();

    void redo();

    void undo();
    void play();

    string getGameType();
}