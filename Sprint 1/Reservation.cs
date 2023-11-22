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
    // leest reservations.json uit en zet het binnen de list
    static Reservation()
    {
        string FileCont = ControllerJson.ReadJson("Reservation.json");
        if (FileCont is not null)
        { 
            All_Reservations = JsonConvert.DeserializeObject<List<Reservation>>(FileCont);
        }
        else
        {
            All_Reservations = new();
        }   
    }
    // constructor. Schrijft het ook naar de json
    public Reservation(int customerId, int table, int guest, string date, string time)
    {
        Reservation_ID = Interlocked.Increment(ref nextID);
        CustomerId = customerId;
        Table = table;
        Guests = guest;
        Date = date;
        Time = time;
        All_Reservations.Add(this);
        string json = JsonConvert.SerializeObject(All_Reservations, Formatting.Indented);
        JArray Object = JArray.Parse(json);
        ControllerJson.WriteJson(Object, "Reservations.json");
    }

    // returned de informatie van de reservatie
    public string Reservation_Info() => $"CustomerID: {CustomerId}, Reservation_ID: {Reservation_ID}, Table {Table}, number of guests: {Guests}, Date: {Date}, Time: {Time}";
    // print de info van de reserevatie en de customer
    public static string Info(Reservation reservation, Customer customer) => $"{Customer.Info(customer)}, Table: {reservation.Table}, Guests: {reservation.Guests}, Date: {reservation.Date}, Time: {reservation.Time}";
}