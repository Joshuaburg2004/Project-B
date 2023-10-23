using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Quic;
using System.Text;
public class Reservation
{

    public static List<Reservation> All_Reservations = new();
    public int CustomerId;
    static int nextID;
    public int Reservation_ID;
    public int Table;
    public int Guests;
    public string Date;
    public string Time;


    public Reservation(int customerId, int table, int guest, string date, string time)
    {
        Reservation_ID = Interlocked.Increment(ref nextID);
        CustomerId = customerId;
        Table = table;
        Guests = guest;
        Date = date;
        Time = time;
    }

    public static void Add_Reservation(Reservation reservation)
    {
        All_Reservations.Add(reservation);
    }

    public string? Reservation_Info() => $"Customer: {CustomerId}, Reservation_ID: {Reservation_ID}, Table {Table}, number of guests: {Guests}, Date: {Date}, Time: {Time}";
    public static string Info(Reservation reservation, Customer customer) => $"{Customer.Info(customer)}, Table: {reservation.Table}, Guests: {reservation.Guests}, Date: {reservation.Date}, Time: {reservation.Time}";
    public bool SendJson()
    {
        string json = JsonConvert.SerializeObject(All_Reservations, Formatting.Indented);
        JArray Object = JArray.Parse(json);
        return ControllerJson.WriteJson(Object, "Reservations.json");
    }

}
