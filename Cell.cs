class Cell
{
    public int Row { get; set; }
    public int Column { get; set; }
    public Piece valuePiece { get; set; }

    public Cell(int row, int column, Piece valuePiece)
    {
        Row = row;
        Column = column;
        this.valuePiece = valuePiece;
    }
}
