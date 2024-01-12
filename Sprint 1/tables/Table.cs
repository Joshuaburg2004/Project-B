//gemaakt door Berkan en Aymane
public abstract class Table
{
    public string tafel_beschikbaar = "| O |";
    public string tafel_bezet = "| O |";
    public string tafel_zelf_gereserveerd = "| O |";
    
    // datum gaat in list
    public List<DateOnly> TimeSlot_1_reserved = new() { };
    public List<DateOnly> TimeSlot_2_reserved = new() { };
    public List<DateOnly> TimeSlot_3_reserved = new() { };
    public List<DateOnly> TimeSlot_4_reserved = new() { };
    public List<DateOnly> TimeSlot_5_reserved = new() { };
    public List<DateOnly> TimeSlot_6_reserved = new() { };
    public List<DateOnly> TimeSlot_7_reserved = new() { };
    public List<DateOnly> TimeSlot_8_reserved = new() { };
    public List<DateOnly> TimeSlot_9_reserved = new() { };
    public List<DateOnly> TimeSlot_10_reserved = new() { };
  


    public int MinGuests;
    public int MaxGuests;

    public Table() { }
    
    public virtual void reserve(int timeslot, DateOnly date)
    {
        switch (timeslot)
        {
            case 1:
                TimeSlot_1_reserved.Add(date);
                break;
            case 2:
                TimeSlot_2_reserved.Add(date);
                break;
            case 3:
                TimeSlot_3_reserved.Add(date);
                break;
            case 4:
                TimeSlot_4_reserved.Add(date);
                break;
            case 5:
                TimeSlot_5_reserved.Add(date);
                break;
            case 6:
                TimeSlot_6_reserved.Add(date);
                break;
            case 7:
                TimeSlot_7_reserved.Add(date);
                break;
            case 8:
                TimeSlot_8_reserved.Add(date);
                break;
            case 9:
                TimeSlot_9_reserved.Add(date);
                break;
            case 10:
                TimeSlot_10_reserved.Add(date);
                break;
        }
    }
}
