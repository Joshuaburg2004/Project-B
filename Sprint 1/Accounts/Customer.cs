using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;
using System.Xml.Linq;

public class Customer : IAccount
{
    public static int nextID = Manager.Customers.Count;
    public int ID { get; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public Customer(string name, string email, string password, string role = "Customer")
    {
        ID = Interlocked.Increment(ref nextID);
        Name = name;
        Password = password;
        Email = email;
        Role = role;
    }
    public List<Reservation> My_Reservation = new List<Reservation>() { };

    public static Customer CreateAccount(string name, string email, string password, string role = "Customer")
    {
        Customer customer = new Customer(name, email, password);
        Manager.Customers.Add(customer);
        string json = JsonConvert.SerializeObject(Manager.Customers, Formatting.Indented);
        JArray Object = JArray.Parse(json);
        ControllerJson.WriteJson(Object, "Customers.json");
        return customer;
    }

    public static Customer? GetCustomerByID(int id)
    {
        foreach (Customer customer in Manager.Customers)
        {
            if (customer.ID == id)
            {
                return customer;
            }
        }
        return null;
    }

    public static string? ChangePassword(int ID, string password)
    {
        foreach (Customer customer in Manager.Customers)
        {
            if (customer.ID == ID && customer.Password == password)
            {
                Console.Write("Please enter your new password: ");
                customer.Password = Console.ReadLine() ?? password;
                string json = JsonConvert.SerializeObject(Manager.Customers, Formatting.Indented);
                JArray Object = JArray.Parse(json);
                ControllerJson.WriteJson(Object, "Customers.json");
                return customer.Password;
            }
        }
        return null;
    }

    public void Add_Reservation(int table, int guest, DateOnly date, string time)
    {
        if (GetCustomerByID(ID) != null)
        {
            My_Reservation.Add(new Reservation(ID, table, guest, date, time));
            string json = JsonConvert.SerializeObject(Manager.Customers, Formatting.Indented);
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

    public static Customer? Log_in(string name, string email, string password)
    {
        foreach(Customer customer in Manager.Customers)
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

    public static string Info(Customer customer)
    {
        return $"ID: {customer.ID}, Name: {customer.Name}, Email: {customer.Email}, Role: {customer.Role}";
    }
}
