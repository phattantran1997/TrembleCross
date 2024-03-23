﻿using System;
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
        // GameController controller = new GameController();

        // TrembleCrossGame gim = new TrembleCrossGame(4);
        // controller.loadGame();
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

                }else if(inputGameType =="2"){
                    //Todo GAME REVERSI 
                }
                else{
                    Console.WriteLine("Back to previous stage....");
                    continue; 
                }
            }
            else if(inputGameType =="2"){

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
