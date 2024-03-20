class Cell
{
    public int Row { get; set; }
    public int Column { get; set; }
    public string Value { get; set; }

    public Cell(int row, int column, string value)
    {
        Row = row;
        Column = column;
        Value = value;
    }
}
