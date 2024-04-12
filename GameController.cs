public class GameController
{
    private IGame _game;
    private static GameController _instance;

    private GameController(IGame game)
    {
        _game = game;
    }

    public GameController()
    {
    }

    public static GameController Instance => _instance ?? (_instance = new GameController());

    internal void InitGame(IGame game)
    {
        _game = game;
    }

    internal void LoadGame(GameFile selectedGame)
    {
        _game.loadGame(selectedGame);
    }

    internal IGame CreateNewGame(string gameType, int boardSize, bool isPlayWithHuman)
    {
        return gameType switch
        {
            "TrembleCross" => new TrembleCrossGame(boardSize, isPlayWithHuman),
            // Add cases for other game types if needed
            _ => throw new ArgumentException("Invalid game type.")
        };
    }
}
