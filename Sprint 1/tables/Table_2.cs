public class Table_2 : Table
{
    private static int _nextId = 7;
    public int TableNumber;

    public Table_2() : base()
    {
        MinGuests = 1;
        MaxGuests = 2;
        _nextId++;
        TableNumber = _nextId;
    }
}
