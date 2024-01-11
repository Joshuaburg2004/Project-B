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
        ControllerJson.WriteJson(Manager.Customers, "Customers.json");
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
                ControllerJson.WriteJson(Manager.Customers, "Customers.json");
                return customer.Password;
            }
        }
        return null;
    }
    // Made by Alperen, Checked en waar nodig fixed by Joshua en Berkan
    public void Change_Reservation()
    {
        Console.WriteLine("Please exit the pages when you are ready to make your choice");
        View_Reservation();

    Console.WriteLine("Which reservation do you want to delete? Enter the reservation id:");
    int input = Convert.ToInt32(Console.ReadLine());

    // Check if the reservation exists. made by Alperen en Joshua
    foreach (Reservation reservation in My_Reservation)
    {
        if (input == reservation.Reservation_ID)
        {
            Console.WriteLine(reservation.Reservation_Info());
            Console.WriteLine("Is this the reservation you want to delete?\n(1) Yes\n(2) No");
            string input2 = Console.ReadLine();

            if (input2 == "1")
            {
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
                else if(tableReserve.TimeSlot_5_reserved.Contains(reservation.Date))
                {
                    tableReserve.TimeSlot_5_reserved.Remove(reservation.Date);
                }
                else if(tableReserve.TimeSlot_6_reserved.Contains(reservation.Date))
                {
                    tableReserve.TimeSlot_6_reserved.Remove(reservation.Date);
                }
                else if(tableReserve.TimeSlot_7_reserved.Contains(reservation.Date))
                {
                    tableReserve.TimeSlot_7_reserved.Remove(reservation.Date);
                }
                else if(tableReserve.TimeSlot_8_reserved.Contains(reservation.Date))
                {
                    tableReserve.TimeSlot_8_reserved.Remove(reservation.Date);
                }
                else if(tableReserve.TimeSlot_9_reserved.Contains(reservation.Date))
                {
                    tableReserve.TimeSlot_9_reserved.Remove(reservation.Date);
                }
                else if(tableReserve.TimeSlot_10_reserved.Contains(reservation.Date))
                {
                    tableReserve.TimeSlot_10_reserved.Remove(reservation.Date);
                }

                // Update the JSON file after making changes
                ControllerJson.WriteJson(My_Reservation, "Reservations.json");
                ControllerJson.WriteJson(Manager.Customers, "Customers.json");

                Console.WriteLine("Reservation updated successfully.");
                return;
            }
            else
            {
                // User chose not to change this reservation
                Console.WriteLine("Reservation not deleted.");
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
            ControllerJson.WriteJson(Manager.Customers, "Customers.json");
        }
    }

    // Print de info van de Reservations die de customer heeft gemaakt.
    public void View_Reservation()
    {
        My_Reservation.Sort((r1, r2) =>
        {
            int dateComparison = r1.Date.CompareTo(r2.Date);
            if (dateComparison != 0)
            {
                return dateComparison;
            }
            return string.Compare(r1.Time, r2.Time);
        });
        List<Reservation[]> reservations = My_Reservation.Chunk(14).ToList();
        int Index = 0;
        while (true)
        {
            foreach (Reservation reservation in reservations[Index])
            {
                Console.WriteLine(reservation.Reservation_Info());
            }
            Console.WriteLine($"You are now at page {Index + 1}/{reservations.Count()}");
            Console.WriteLine("Enter a page, or (Q) to exit");
            string Page = Console.ReadLine()!.ToUpper();
            bool Convert = int.TryParse(Page, out int page);
            if (Page == "Q") { break; }
            if (Convert)
            {
                if (page > reservations.Count()) { Console.WriteLine("This page does not exist"); }
                else { Index = page - 1; }
            }
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

public void LeaveReview()
//gemaakt door sami
{
    Console.WriteLine("Enter your review text:");
    string reviewText = Console.ReadLine();

    Console.WriteLine("Enter the number of stars (1-5):");
    if (int.TryParse(Console.ReadLine(), out int stars) && stars >= 1 && stars <= 5)
    {
        DateTime currentDate = DateTime.Now;

        Review review = new Review(Review.AllReviews.Count + 1, ID, reviewText, stars, currentDate);
        Review.AllReviews.Add(review);

        string jsonReviews = JsonConvert.SerializeObject(Review.AllReviews, Formatting.Indented, new JsonSerializerSettings
        {
            DateFormatString = "yyyy-MM-dd"
        });

        JArray reviewsObject = JArray.Parse(jsonReviews);
        ControllerJson.WriteJson(reviewsObject, "Reviews.json");

        Console.WriteLine("Review submitted successfully.");
    }
    else
    {
        Console.WriteLine("Invalid input for stars. Review not submitted.");
    }
}
}
