using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;
public class GameController{

 
    internal List<TrembleCrossGame> loadGame(){

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

            var GameType = root.GetProperty("files")[i].GetProperty("GameType").ToString();

            // Check gametype
            if(GameType == "trembleCross"){
                // Extract Board and Turn information from Json file
                var Board = root.GetProperty("files")[i].GetProperty("Board").GetString();

                var Turn = Convert.ToInt32(root.GetProperty("files")[i].GetProperty("Turn").GetString()); 

                var Humans = bool.Parse(root.GetProperty("files")[i].GetProperty("Humans").ToString());            
           
                // Add it to the TrembleCross List
                trembleCrossGames.Add(new TrembleCrossGame(Board, Turn, Humans));
            }
            else if(GameType == "ReversiGame")
            {
                // Implement for Reversi Game
                continue;
            }
        }
        return trembleCrossGames;

    }


    // Only saves 1 game
    public void SaveGame(object game){

        // Get the directory for Tremblecross
        var TrembleCrossDir = new DirectoryInfo(Directory.GetCurrentDirectory());
        string filePath = TrembleCrossDir.Parent.Parent.Parent.ToString()+"\\TrembleCross\\game.json";

        List<TrembleCrossGame> trembleCrossGames = new List<TrembleCrossGame>();
        
        // Convert game object to Tremblecross
        TrembleCrossGame games = game as TrembleCrossGame;

        // Read existing data from file
        string existingGame = File.ReadAllText(filePath);

        // Deserialize data
        var existingObj = JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string,string>>>>(existingGame);

        if(existingObj != null && existingObj.ContainsKey("files")){
            foreach (var file in existingObj["files"]){
                string board = file["Board"].ToString();
                int turn = Convert.ToInt32(file["Turn"]);
                bool humans = bool.Parse(file["Humans"]);
                trembleCrossGames.Add(new TrembleCrossGame(board, turn,humans));
            }
        }
        
        trembleCrossGames.Add(games);

        // Setup Json data
        // Name of Game, current state of board, player turn
        var obj = new {
            files = trembleCrossGames.Select(g => new{
                GameType = "trembleCross",
                Board= g.gameCurrentState.ToString(),
                Turn = g.turn.ToString(),
                Humans = g.isPlayWithHuman.ToString()
                }
            )
            
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