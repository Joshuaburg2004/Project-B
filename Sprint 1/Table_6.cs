public class Table_6 : Table
{
    private static int _nextId;
    public int TableNumber;

    public Table_6() : base()
    {
        _nextId++;
        TableNumber = _nextId;
    }
}