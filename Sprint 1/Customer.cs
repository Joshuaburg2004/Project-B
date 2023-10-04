using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class Customer : Account
{
    public Customer(string name, string email, string password) : base(name, email, password) { }
    public List<Reservation> My_Reservation = new List<Reservation>() { };

    public static Customer CreateAccount(string name, string email, string password)
    {

        Customer customer = new Customer(name, email, password);
        AccountManager.Accounts.Add(customer);
        string json = JsonConvert.SerializeObject(AccountManager.Accounts, Formatting.Indented);
        JArray Object = JArray.Parse(json);
        ControllerJson.WriteJson(Object, "Accounts.json");
        return customer;
    }

    public static Customer? GetAccount(int id)
    {
        foreach (Customer customer in AccountManager.Accounts)
        {
            if (customer.Id == id)
            {
                return customer;
            }
        }
        return null;
    }

    public void Add_Reservation(int table, int guest, string date, string time)
    {
        Reservation.Add_Reservation(new Reservation(Customer.GetAccount(this.Id), table, guest, date, time));
    }

    // dit zet mijn reserveringen in een lijst en returned het.
    public List<Reservation> View_Reservation()
    {
        foreach (Reservation reservation in Reservation.All_Reservations)
        {
            if (reservation.Customer.Id == Id)
            {
                My_Reservation.Add(reservation);
                return My_Reservation;
            }
        }
        return null;
    }

    // later door.
    public Reservation Change_Reservation()
    {
        foreach (Reservation reservation in My_Reservation)
        {
            if ()
            {
                
            }
            return reservation;
        }
        return null;
    }

    public static string Info(Customer customer)
    {
        return $"ID: {customer.Id}, Name: {customer.Name}, Email: {customer.Email}, Role: {customer.Role}";
    }
}
