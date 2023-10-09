using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class Customer : Account
{
    public Customer(string name, string email, string password, string role = "Customer") : base(name, email, password, role) { }
    public List<Reservation> My_Reservation = new List<Reservation>() { };

    public static Customer CreateAccount(string name, string email, string password, string role = "Customer")
    {

        Customer customer = new Customer(name, email, password);
        AccountManager.Customers.Add(customer);
        string json = JsonConvert.SerializeObject(AccountManager.Customers, Formatting.Indented);
        JArray Object = JArray.Parse(json);
        ControllerJson.WriteJson(Object, "Customers.json");
        return customer;
    }

    public static Customer? GetCustomer(int id)
    {
        foreach (Customer customer in AccountManager.Customers)
        {
            if (customer.Id == id)
            {
                return customer;
            }
        }
        return null;
    }

    public static string? ChangePassword(string name, string password)
    {
        foreach (Customer customer in AccountManager.Customers)
        {
            if (customer.Name == name && customer.Password == password)
            {
                Console.Write("Please enter your new password: ");
                customer.Password = Console.ReadLine() ?? password;
                string json = JsonConvert.SerializeObject(AccountManager.Customers, Formatting.Indented);
                JArray Object = JArray.Parse(json);
                ControllerJson.WriteJson(Object, "Customers.json");
                return customer.Password;
            }
        }
        return null;
    }

    public void Add_Reservation(int table, int guest, string date, string time)
    {
        if(GetCustomer(Id) != null)
        {
            Reservation.Add_Reservation(new Reservation(Id, table, guest, date, time));
        }
    }

    // dit zet mijn reserveringen in een lijst en returned het.
    public List<Reservation>? View_Reservation()
    {
        foreach (Reservation reservation in Reservation.All_Reservations)
        {
            if (reservation.CustomerId == Id)
            {
                My_Reservation.Add(reservation);
                return My_Reservation;
            }
        }
        return null;
    }

    // later door.
/*    public Reservation? Change_Reservation()
    {
        foreach (Reservation reservation in My_Reservation)
        {
            if ()
            {
                
            }
            return reservation;
        }
        return null;
    }*/

    public static string Info(Customer customer)
    {
        return $"ID: {customer.Id}, Name: {customer.Name}, Email: {customer.Email}, Role: {customer.Role}";
    }
}
