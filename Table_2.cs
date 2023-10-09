public class Table_2 : Table
{
    private static int _nextId = 7;
    public int TableNumber;

    public Table_2() : base()
    {
        _nextId++;
        TableNumber = _nextId;
    }
}