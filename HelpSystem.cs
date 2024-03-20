public class HelpSystem{
    public string gameRule {get;set;}
    public string gameCommand  {get;set;}

    public void show(){
        Console.WriteLine("Game Rule : {0}", this.gameRule);
        Console.WriteLine("Game Command : {0}", this.gameCommand);
    }
}