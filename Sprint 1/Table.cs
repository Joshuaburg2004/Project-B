public abstract class Table
{
    // weet niet zeker hoeveel timeslots we hebben en hoelaat ze beginnen
    // false is beschikbaar
    // true is gereserveerd
    public bool Timeslot_1;
    public bool Timeslot_2;
    public bool Timeslot_3;
    public bool Timeslot_4;
    public bool Timeslot_5;
    public bool Timeslot_6;

    public Table() { }

    // verandert timeslot van 
    public void reserve(int timeslot)
    {
        if (timeslot == 1) { Timeslot_1 = true; }
        else if (timeslot == 2) {  Timeslot_2 = true; }
        else if (timeslot == 3) {  Timeslot_3 = true; }
        else if (timeslot == 4) {  Timeslot_4 = true; }
        else if (timeslot == 5) {  Timeslot_5 = true; }
        else if (timeslot == 6) {  Timeslot_6 = true; }
    }
}
