using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
class Program{

    static void Main(string[] args)
    {
        
 
        // string input = "";
        GameController controller = new GameController();

        TrembleCrossGame gim = new TrembleCrossGame(4);
        controller.loadGame();
        // while(true){
        //     if(input =="0")
        //         break;
        //     Console.WriteLine("Do you want to load previous game or create new game? \n 1. Load game \n 2. Create new game");
        //     input = Console.ReadLine();
        //     if(input == "1"){
        //         controller.loadGame();
        //     }
        //     Console.WriteLine("What game do you play? \n 1. GameA \n 2. GameB");
        //     input = Console.ReadLine();
        //     if(input == "gameA"){
        //         TrembleCrossGame trembleCross = new TrembleCrossGame(10);
        //         trembleCross.start();
        //         System.Console.WriteLine(trembleCross.ReturnBoardState());
        //     }else{

        //     }
        // }
       

        
    }
}
