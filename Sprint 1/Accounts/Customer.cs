using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


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
    // Made by Alperen, Checked en waar nodig fixed by Joshua en Berkan
    public void Change_Reservation()
    {
    View_Reservation();

    Console.WriteLine("Which reservation do you want to change? Enter the reservation id:");
    int input = Convert.ToInt32(Console.ReadLine());

    // Check if the reservation exists. made by Alperen en Joshua
    foreach (Reservation reservation in My_Reservation)
    {
        if (input == reservation.Reservation_ID)
        {
            Console.WriteLine(reservation.Reservation_Info());
            Console.WriteLine("Is this the reservation you want to change?\n(1) Yes\n(2) No");
            string input2 = Console.ReadLine();

            if (input2 == "1")
            {
                Console.WriteLine("What would you like to change?\n(1) Table\n(2) Guests\n(3) Date\n(4) Time\n(5)Delete reservation");
                string input3 = Console.ReadLine();
                Table? tableReserve;
                tableReserve = reservation.Table switch
                {
                    1 => Manager.table_1,
                    2 => Manager.table_2,
                    3 => Manager.table_3,
                    4 => Manager.table_4,
                    5 => Manager.table_5,
                    6 => Manager.table_6,
                    7 => Manager.table_7,
                    8 => Manager.table_8,
                    9 => Manager.table_9,
                    10 => Manager.table_10,
                    11 => Manager.table_11,
                    12 => Manager.table_12,
                    13 => Manager.table_13,
                    14 => Manager.table_14,
                    15 => Manager.table_15,
                    _ => null
                };
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
                        reservation.Date = DateOnly.Parse(newDate);
                        break;

                    case "4":
                        
                        bool t1 = false;
                        bool t2 = false;
                        bool t3 = false;
                        bool t4 = false;
                        if (!tableReserve.TimeSlot_1_reserved.Contains(reservation.Date)) { Console.WriteLine("Timeslot 1: 17:00-17:30"); t1 = true; }
                        if (!tableReserve.TimeSlot_2_reserved.Contains(reservation.Date)) { Console.WriteLine("Timeslot 2: 17:30-18:00"); t2 = true; }
                        if (!tableReserve.TimeSlot_3_reserved.Contains(reservation.Date)) { Console.WriteLine("Timeslot 3: 18:00-18:30"); t3 = true; }
                        if (!tableReserve.TimeSlot_4_reserved.Contains(reservation.Date)) { Console.WriteLine("Timeslot 4: 18:30-19:00"); t4 = true; }
                        string? time = Console.ReadLine();
                        if (Int32.TryParse(time, out int timeslot))
                        {
                            switch (timeslot)
                            {
                                case 1:
                                    if (t1) { tableReserve.TimeSlot_1_reserved.Add(reservation.Date); Console.WriteLine($"Table {reservation.Table} reserved for 17:00 until 17:30"); reservation.Time = time; }
                                    else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                                    break;
                                case 2:
                                    if (t2) { tableReserve.TimeSlot_2_reserved.Add(reservation.Date); Console.WriteLine($"Table {reservation.Table} reserved for 17:30 until 18:00"); reservation.Time = time; }
                                    else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                                    break;
                                case 3:
                                    if (t3) { tableReserve.TimeSlot_3_reserved.Add(reservation.Date); Console.WriteLine($"Table {reservation.Table} reserved for 18:00 until 18:30"); reservation.Time = time; }
                                    else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                                    break;
                                case 4:
                                    if (t4) { tableReserve.TimeSlot_4_reserved.Add(reservation.Date); Console.WriteLine($"Table {reservation.Table} reserved for 18:30 until 19:00"); reservation.Time = time; }
                                    else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                                    break;
                            }
                        }
                        break;
                    case "5":
                        Console.WriteLine("U sure you want to delete this reservation (1)y/(2)n");
                        string ans = Console.ReadLine();
                        if(ans == "1")
                        {
                            My_Reservation.Remove(reservation);
                            if(tableReserve.TimeSlot_1_reserved.Contains(reservation.Date))
                            {
                                tableReserve.TimeSlot_1_reserved.Remove(reservation.Date);
                            }
                            else if(tableReserve.TimeSlot_2_reserved.Contains(reservation.Date))
                            {
                               tableReserve.TimeSlot_2_reserved.Remove(reservation.Date); 
                            }
                            else if(tableReserve.TimeSlot_3_reserved.Contains(reservation.Date))
                            {
                                tableReserve.TimeSlot_3_reserved.Remove(reservation.Date);
                            }
                            else if(tableReserve.TimeSlot_4_reserved.Contains(reservation.Date))
                            {
                                tableReserve.TimeSlot_4_reserved.Remove(reservation.Date);
                            }
                            
                        }
                        else
                        {
                            Console.WriteLine("\nPress enter to continue");
                            Console.ReadLine();
                        }
                        break;

                    default:
                        Console.WriteLine("Unknown input, try again.");
                        break;
                }

                // Update the JSON file after making changes
                string json = JsonConvert.SerializeObject(My_Reservation, Formatting.Indented);
                JArray Object = JArray.Parse(json);
                ControllerJson.WriteJson(Object, "Reservations.json");
                string json1 = JsonConvert.SerializeObject(Manager.Customers, Formatting.Indented);
                JArray Object1 = JArray.Parse(json1);
                ControllerJson.WriteJson(Object1, "Customers.json");

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

    public static string Info(Customer customer)
    {
        return $"ID: {customer.ID}, Name: {customer.Name}, Email: {customer.Email}, Role: {customer.Role}";
    }
}
