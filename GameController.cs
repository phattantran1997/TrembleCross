using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;
public class GameController{


    // Can load Json data haven't figured out how to parse it to 
    // tremblecrossgame. might need to overload constructor 
    public void loadGame(){
        var TrembleCrossDir = new DirectoryInfo(Directory.GetCurrentDirectory());
        string filePath = TrembleCrossDir.Parent.Parent.Parent.ToString()+"\\TrembleCross\\game.json";

        string existingJson = File.ReadAllText(filePath);

        System.Console.WriteLine(existingJson);


        // Parse the JSON data
        var jsonDocument = JsonDocument.Parse(existingJson);
        var root = jsonDocument.RootElement;

        // Extract the GameType
        // var Board = root.GetProperty("files")[1].GetProperty("Board");
        var GameType = root.GetProperty("files")[0].GetProperty("GameType");
        var Board = root.GetProperty("files")[0].GetProperty("Board");
        var Turn = root.GetProperty("files")[0].GetProperty("Turn");

        System.Console.WriteLine(GameType);
        System.Console.WriteLine(Board);
        System.Console.WriteLine(Turn);
        
        // Create a list of TrembleCross games
        List<TrembleCrossGame> allGame = new List<TrembleCrossGame>();
        // allGame.Add(gameType);

        // Print the list
        // Console.WriteLine("TrembleCross Games:");
      
        // Console.WriteLine(allGame[0].ReturnBoardState());
        


        // List<TrembleCrossGame> ExistingGame = 
        // JsonSerializer.Deserialize<List<TrembleCrossGame>>(existingJson)??
        // new List<TrembleCrossGame>();
        //filename
        
        // System.Console.WriteLine($"Current number of existing game {ExistingGame.Count}");
        //object

    }
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