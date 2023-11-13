using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

// Account class voor de customers
public class Customer : Account
{
    public Customer(string name, string email, string password, string role = "Customer") : base(name, email, password, role) { }
    public List<Reservation> My_Reservation = new List<Reservation>() { };
    // use this instead of constructor
    public static Customer CreateAccount(string name, string email, string password, string role = "Customer")
    {
        // Writes all customers to the json when Customer gets made.
        Customer customer = new Customer(name, email, password);
        AccountManager.Customers.Add(customer);
        string json = JsonConvert.SerializeObject(AccountManager.Customers, Formatting.Indented);
        JArray Object = JArray.Parse(json);
        ControllerJson.WriteJson(Object, "Customers.json");
        return customer;
    }
    // gets customer by id
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
    // changes password based on name and password
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
    // voegt een reservatie aan de customer toe.
    public void Add_Reservation(int table, int guest, string date, string time)
    {
        if (GetCustomer(Id) != null)
        {
            My_Reservation.Add(new Reservation(Id, table, guest, date, time));
            string json = JsonConvert.SerializeObject(AccountManager.Customers, Formatting.Indented);
            JArray Object = JArray.Parse(json);
            ControllerJson.WriteJson(Object, "Customers.json");
        }
    }

    // Print de info van de Reservations die de customer heeft gemaakt.
    public void View_Reservation()
    {
        foreach(Reservation reservation in My_Reservation)
        {
            Console.WriteLine(reservation.Reservation_Info());
        }
    }
    // logt de customer in
    public static Customer? Log_in(string name, string email, string password)
    {
        foreach(Customer customer in AccountManager.Customers)
        {
            if(customer.Name == name && customer.Email == email && customer.Password == password)
            {
                return customer;
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
    // print de info van de customer
    public static string Info(Customer customer)
    {
        return $"ID: {customer.Id}, Name: {customer.Name}, Email: {customer.Email}, Role: {customer.Role}";
    }
}
