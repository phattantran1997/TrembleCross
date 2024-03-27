using System;
using System.IO;
using System.Text.Json;

public class UISystem
{
    private GameController gameController { get; set; } = new GameController();
    private IGame game = null;

    private IGame InitializeGame(string gameType)
    {
        switch (gameType)
        {
            case "1":
                game = new TrembleCrossGame();
                break;
            case "2":
                Console.WriteLine("this game not yet implemented so game will be empty");
                break;
            default:
                throw new ArgumentException("Invalid game type.");
        }
        gameController = new GameController(game);
        return game;
    }

    internal void createNewGameDialog(IGame game)
    {
        Console.WriteLine("Choose opponent:\n1. Human\n2. Computer");
        bool isPlayWithHuman = Console.ReadLine() == "1";
        Console.WriteLine("Enter board size:");
        int boardSize = int.Parse(Console.ReadLine());
        gameController.createNewGame(game.getGameType(), boardSize, isPlayWithHuman).play();
    }

    internal void loadGameDialog(IGame game)
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), game.getGameType(), "game.json");
        string jsonContent = File.ReadAllText(filePath);
        GameData gameData = JsonSerializer.Deserialize<GameData>(jsonContent);
        Console.WriteLine("Available Games:");
        for (int i = 0; i < gameData.files.Count; i++)
        {
            Console.WriteLine($"Game {i + 1}: Board - {gameData.files[i].Board}, Turn - {gameData.files[i].Turn}, Humans - {gameData.files[i].Humans}");
        }
        Console.WriteLine("Enter the number of the game you want to load:");
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > gameData.files.Count)
        {
            Console.WriteLine("Invalid input. Please enter a valid game number:");
        }
        gameController.LoadGame(gameData.files[choice - 1]);
    }

    public void Initialize()
    {
        while (true)
        {
            Console.WriteLine("What game do you want to play?\n1. TrembleCross\n2. Reversi\n0. Exit");
            string inputGameType = Console.ReadLine();

            if (inputGameType == "0")
            {
                Console.WriteLine("Exiting the game.");
                return;
            }

            if (inputGameType != "1" && inputGameType != "2")
            {
                Console.WriteLine("Invalid option. Please choose again.");
                continue;
            }

            Console.WriteLine("Choose an option:\n1. Create new game\n2. Load existing game");
            string inputGameMode = Console.ReadLine();

            if (inputGameMode != "1" && inputGameMode != "2")
            {
                Console.WriteLine("Invalid option. Please choose again.");
                continue;
            }

            IGame game = InitializeGame(inputGameType);
            if (inputGameMode == "1")
            {
                createNewGameDialog(game);
            }
            else if (inputGameMode == "2")
            {
                loadGameDialog(game);
            }
        }
    }
}
