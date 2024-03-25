using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string inputGameType = "";
        // GameController controller = new GameController();

        while (true)
        {
            Console.WriteLine("What game do you want to play?\n1. TrembleCross\n2. Reversi\n0. Exit");
            inputGameType = Console.ReadLine();

            switch (inputGameType)
            {
                case "1":
                    IGame game = new TrembleCrossGame();
                    GameController controller = new GameController(game);

                    Console.WriteLine("Choose an option:\n1. Create new game\n2. Load existing game");
                    string option = Console.ReadLine();
                    if (option == "1")
                    {
                        Console.WriteLine("Choose opponent:\n1. Human\n2. Computer");
                        bool isPlayWithHuman = Console.ReadLine() == "1";
                        Console.WriteLine("Enter board size:");
                        int boardSize = int.Parse(Console.ReadLine());
                        game = controller.createNewGame("TrembleCross", boardSize, isPlayWithHuman);
                        game.play();
                    }
                    else if (option == "2")
                    {
                        controller.LoadGame();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please choose again.");
                    }
                    break;
                case "2":
                    Console.WriteLine("Reversi game is not implemented yet.");
                    break;
                case "0":
                    Console.WriteLine("Exiting the game.");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }
        }
    }
}
