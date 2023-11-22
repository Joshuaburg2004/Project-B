public class Table_4 : Table
{
    private static int _nextId = 2;
    public int TableNumber;

    public Table_4() : base()
    {
        MinGuests = 3;
        MaxGuests = 4;
        _nextId++;
        TableNumber = _nextId;
    }
}
