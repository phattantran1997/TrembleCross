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
        string inputGameType = "";
        GameController controller = new GameController();


        while(true){
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Create new game");
            Console.WriteLine("2. Load existing game");
            Console.WriteLine("0. Exit");
            inputGameType = Console.ReadLine();
            if(inputGameType =="0")
                break;
            else if(inputGameType =="1"){
                
                Console.WriteLine("What game do you play? \n 1. TrembleCross \n 2. Reversi");
                inputGameType = Console.ReadLine();
                Console.WriteLine("Who do you want to play against? \n 1. Human \n 2. Computer");
                var isPlayWithHuman = Console.ReadLine() == "1" ? true :false;
                if(inputGameType =="1"){
                    Console.WriteLine("How many size do you want?");
                    int boardSize = Int32.Parse(Console.ReadLine());
                    TrembleCrossGame trembleCrossGame = new TrembleCrossGame(boardSize,isPlayWithHuman);
                    trembleCrossGame.play(isPlayWithHuman);
                    controller.SaveGame(trembleCrossGame);

                }else if(inputGameType =="2"){
                    //Todo GAME REVERSI 
                }
                else{
                    Console.WriteLine("Back to previous stage....");
                    continue; 
                }
            }
            else if(inputGameType =="2"){

                List<TrembleCrossGame> trembleCrossGames = controller.loadGame();
                System.Console.WriteLine("\nThese are the current game boards");
                for(int i = 0; i < trembleCrossGames.Count; i++){
                    System.Console.WriteLine($"Pick {i} for \n{trembleCrossGames[i].ReturnBoardState()}");
                }

                int input = 0;
                try{
                    if(!int.TryParse(Console.ReadLine(), out input)){
                        System.Console.WriteLine("Please enter a valid value\n");
                        continue;
                    }
                    else
                    trembleCrossGames[input].play(trembleCrossGames[input].isPlayWithHuman);
                    controller.SaveGame(trembleCrossGames[input]);
                }
                catch(Exception e){
                    System.Console.WriteLine(e);
                }
            }
            else if(inputGameType =="0"){
                break;
            }
            else{
                Console.WriteLine("Invalid option. Please choose again.");
            }
        }
       

        
    }
}
