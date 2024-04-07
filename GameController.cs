using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;
public class GameController{
    private readonly IGame _game;
    internal GameController(IGame game){
        _game = game;
    }

    public GameController()
    {
    }

    internal void LoadGame(GameFile selectedGame)
    {
        _game.loadGame(selectedGame);
    }


    // Only saves 1 game
    public void SaveGame(){

      _game.saveGame();
    }
    internal IGame createNewGame(string gameType , int boardSize, bool isPlayWithHuman){
        IGame newGame;
        // Create the appropriate type of game based on gameType
        switch (gameType)
        {
            case "TrembleCross":
                newGame = new TrembleCrossGame(boardSize, isPlayWithHuman);
                break;
            // Add cases for other game types if needed
            default:
                throw new ArgumentException("Invalid game type.");
        }

        return newGame;
    }

}