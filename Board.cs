class Board{

    List<Cell> cells {get; set;}
    public Board(int x_dim, int y_dim){
        List<Cell> cells = new List<Cell>();

        for(int i = 0; i < x_dim; i++){
            for(int j =0; j < y_dim; j++){
                cells.Add(new Cell(i,j," "));
            }
        }
        this.cells = cells;
    }

    public override string ToString(){   
        return string.Join(",", cells.Select(cell => cell.Value));
;
    }
    public void updateCells(int cell){
        cells[cell].Value = "X";
    }

    // Return Human readable table
    public string formatTable(){
        int rowCount = cells.Max(cell => cell.Row) + 1;
        int colCount = cells.Max(cell => cell.Column) + 1;

        string[,] tableArray = new string[rowCount, colCount];

        foreach (var cell in cells)
        {
            tableArray[cell.Row, cell.Column] = cell.Value;
        }


        Console.Clear();
        
        int tableWidth = colCount * 4;

        // Empty String
        string result = "";

        // top of table
        result += new string('-',tableWidth + colCount)+"\n";

        
        int width = (tableWidth - colCount) / colCount;
        
        for(int i = 0; i < rowCount;i++){
            for(int j = 0; j < colCount;j++){
                result += "|" + AlignCentre(tableArray[i,j], width) + "|";

                
            }
            result += "\n"+new string('-',tableWidth+colCount) + "\n";
        }      

        return result;
    }

    // Format for the human readable table
    static string AlignCentre(string text, int width)
    {
        text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

        if (string.IsNullOrEmpty(text))
        {
            return new string(' ', width);
        }
        else
        {
            return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }
    }
}
