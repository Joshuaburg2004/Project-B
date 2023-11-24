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
    public void Change_Reservation()
    {
    View_Reservation();

    Console.WriteLine("Which reservation do you want to change? Enter the reservation id:");
    int input = Convert.ToInt32(Console.ReadLine());

    // Check if the reservation exists.
    foreach (Reservation reservation in My_Reservation)
    {
        if (input == reservation.Reservation_ID)
        {
            Console.WriteLine(reservation.Reservation_Info());
            Console.WriteLine("Is this the reservation you want to change?\n(1) Yes\n(2) No");
            string input2 = Console.ReadLine();

            if (input2 == "1")
            {
                Console.WriteLine("What would you like to change?\n(1) Table\n(2) Guests\n(3) Date\n(4) Time");
                string input3 = Console.ReadLine();

                switch (input3)
                {
                    case "1":
                        Console.WriteLine("Enter the new table number:");
                        int newTable = Convert.ToInt32(Console.ReadLine());
                        reservation.Table = newTable;
                        break;

                    case "2":
                        Console.WriteLine("Enter the new number of guests:");
                        int newGuests = Convert.ToInt32(Console.ReadLine());
                        reservation.Guests = newGuests;
                        break;

                    case "3":
                        Console.WriteLine("Enter the new date (DD/MM/YYYY):");
                        string newDate = Console.ReadLine();
                        reservation.Date = newDate;
                        break;

                    case "4":
                        Console.WriteLine("Enter the new time:");
                        string newTime = Console.ReadLine();
                        reservation.Time = newTime;
                        break;

                    default:
                        Console.WriteLine("Unknown input, try again.");
                        break;
                }

                // Update the JSON file after making changes
                string json = JsonConvert.SerializeObject(My_Reservation, Formatting.Indented);
                JArray Object = JArray.Parse(json);
                ControllerJson.WriteJson(Object, "Reservations.json");

                Console.WriteLine("Reservation updated successfully.");
                return;
            }
            else
            {
                // User chose not to change this reservation
                Console.WriteLine("Reservation not changed.");
                return;
            }
        }
    }

    // If the loop completes, the reservation ID was not found
    Console.WriteLine("Reservation not found.");
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
