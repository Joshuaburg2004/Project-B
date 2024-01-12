//gemaakt door Berkan en Aymane
public class Table_6 : Table
{
    private static int _nextId;
    public int TableNumber;

    public Table_6() : base()
    {
        MinGuests = 5;
        MaxGuests = 6;
        _nextId++;
        TableNumber = _nextId;
    }
}
