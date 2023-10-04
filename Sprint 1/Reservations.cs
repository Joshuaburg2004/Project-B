public class Reservation
{

    public static List<Reservation> All_Reservations = new();
    public Customer Customer { get; set; }
    static int nextID;
    public int Reservation_ID;
    public int Table;
    public int Guests;
    public string Date;
    public string Time;

    
    public Reservation(Customer customer, int table, int guest, string date, string time)
    {
        Reservation_ID = Interlocked.Increment(ref nextID);
        Customer = customer;
        Table = table;
        Guests = guest;
        Date = date;
        Time = time;
    }

    public static void Add_Reservation(Reservation reservation)
    {
        All_Reservations.Add(reservation);
    }

    public static Reservation Reservation_Info(Reservation reservation)
    {
        return reservation;
    }

    public static string Info(Reservation reservation, Customer customer)
    {
        return $"{Customer.Info(customer)}, Table: {reservation.Table}, Geusts: {reservation.Guests}, Date: {reservation.Date}, Time: {reservation.Time}"
    }

}
