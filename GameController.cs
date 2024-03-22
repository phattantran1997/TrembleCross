using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;
public class GameController{

 
    public void loadGame(){

        // path to Tremble cross
        var TrembleCrossDir = new DirectoryInfo(Directory.GetCurrentDirectory());
        string filePath = TrembleCrossDir.Parent.Parent.Parent.ToString()+"\\TrembleCross\\game.json";

        // Read File
        string existingJson = File.ReadAllText(filePath);

        // Parse the JSON data
        var jsonDocument = JsonDocument.Parse(existingJson);
        var root = jsonDocument.RootElement;

        // Make TrembleCross List
        List<TrembleCrossGame> trembleCrossGames = new List<TrembleCrossGame>();
        for( int i = 0; i < root.GetProperty("files").GetArrayLength();i++){

            // In future can do if code to check gametype
            // var GameType = root.GetProperty("files")[i].GetProperty("GameType");

            // Extract Board and Turn information from Json file
            var Board = root.GetProperty("files")[i].GetProperty("Board").GetString();

            var Turn = root.GetProperty("files")[i].GetProperty("Turn").GetInt32();            
            
            // Add it to the TrembleCross List
            trembleCrossGames.Add(new TrembleCrossGame(Board, Turn));
        }

    }


    // Only saves 1 game
    public void SaveGame(object game){

        // Get the directory for Tremblecross
        var TrembleCrossDir = new DirectoryInfo(Directory.GetCurrentDirectory());
        string filePath = TrembleCrossDir.Parent.Parent.Parent.ToString()+"\\TrembleCross\\game.json";

        // Convert game object to Tremblecross
        TrembleCrossGame games = game as TrembleCrossGame;

        // Setup Json data
        // Name of Game, current state of board, player turn
        var obj = new {
            files = new[]{
                new {
                    GameType = "trembleCross",
                    Board= games.gameCurrentState.ToString(),
                    Turn = games.turn
                }
            }
        };

        // Serialise it
        var json = JsonSerializer.Serialize(obj);

        // Write it to file
        File.WriteAllText(filePath,json);
    }
    public void createNewGame(){

    }

}

// 