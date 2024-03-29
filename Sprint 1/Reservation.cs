using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Quic;
using System.Text;
public class Reservation : IComparable<Reservation>
{ 
    public static List<Reservation> All_Reservations = new();
    public int CustomerId;
    static int nextID;
    public int Reservation_ID;
    public int Table;
    public int Guests;
    public DateOnly Date;
    public string Time;
    // leest reservations.json uit en zet het binnen de list
    static Reservation()
    {
        All_Reservations = ControllerJson.ReadJson<Reservation>("Reservations.json") ?? new List<Reservation> { };
    }
    // constructor. Schrijft het ook naar de json
    public Reservation(int customerId, int table, int guest, DateOnly date, string time)
    {
        Reservation_ID = Interlocked.Increment(ref nextID);
        CustomerId = customerId;
        Table = table;
        Guests = guest;
        Date = date;
        Time = time;
        All_Reservations.Add(this);
        ControllerJson.WriteJson(All_Reservations, "Reservations.json");
    }
    public int CompareTo(Reservation? obj)
    {
        if (obj is not null)
            return obj.Date.CompareTo(Date);
        return 1;
    }
    // returned de informatie van de reservatie
    public string Reservation_Info_Admin() => $"CustomerID: {CustomerId}, Customer Name: {Customer.GetCustomerByID(CustomerId)!.Name}, Reservation_ID: {Reservation_ID}, Table {Table}, number of guests: {Guests}, Date: {Date}, TimeSlot: {Time}";
    public string Reservation_Info() => $"Reservation_ID: {Reservation_ID}, Table {Table}, number of guests: {Guests}, Date: {Date}, TimeSlot: {Time}";
    // print de info van de reserevatie en de customer
    public static string Info(Reservation reservation, Customer customer) => $"{Customer.Info(customer)}, Table: {reservation.Table}, Guests: {reservation.Guests}, Date: {reservation.Date}, Time: {reservation.Time}";
}
