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
                    Console.WriteLine("(1) Delete Reservation");
                    Console.WriteLine("(2) Change date");
                    string toChange = Console.ReadLine();
                    if(int.TryParse(toChange, out int c))
                    {
                        if(c == 1)
                        {
                            My_Reservation.Remove(reservation);
                            if (tableReserve.TimeSlot_1_reserved.Contains(reservation.Date))
                            {
                                tableReserve.TimeSlot_1_reserved.Remove(reservation.Date);
                            }
                            else if (tableReserve.TimeSlot_2_reserved.Contains(reservation.Date))
                            {
                                tableReserve.TimeSlot_2_reserved.Remove(reservation.Date);
                            }
                            else if (tableReserve.TimeSlot_3_reserved.Contains(reservation.Date))
                            {
                                tableReserve.TimeSlot_3_reserved.Remove(reservation.Date);
                            }
                            else if (tableReserve.TimeSlot_4_reserved.Contains(reservation.Date))
                            {
                                tableReserve.TimeSlot_4_reserved.Remove(reservation.Date);
                            }
                            else if (tableReserve.TimeSlot_5_reserved.Contains(reservation.Date))
                            {
                                tableReserve.TimeSlot_5_reserved.Remove(reservation.Date);
                            }
                            else if (tableReserve.TimeSlot_6_reserved.Contains(reservation.Date))
                            {
                                tableReserve.TimeSlot_6_reserved.Remove(reservation.Date);
                            }
                            else if (tableReserve.TimeSlot_7_reserved.Contains(reservation.Date))
                            {
                                tableReserve.TimeSlot_7_reserved.Remove(reservation.Date);
                            }
                            else if (tableReserve.TimeSlot_8_reserved.Contains(reservation.Date))
                            {
                                tableReserve.TimeSlot_8_reserved.Remove(reservation.Date);
                            }
                            else if (tableReserve.TimeSlot_9_reserved.Contains(reservation.Date))
                            {
                                tableReserve.TimeSlot_9_reserved.Remove(reservation.Date);
                            }
                            else if (tableReserve.TimeSlot_10_reserved.Contains(reservation.Date))
                            {
                                tableReserve.TimeSlot_10_reserved.Remove(reservation.Date);
                            }
                        }
                        else if(c == 2)
                        {
                            Console.WriteLine("Are you sure you wish to change the date?\n(1) Yes\n(2)No\n");
                            string Confirmation = Console.ReadLine();
                            if(Confirmation == "1")
                            {
                                Console.WriteLine("Enter your new date (DD/MM/YYYY):\n");
                                string newDate = Console.ReadLine();
                                DateOnly date = DateOnly.Parse(newDate);
                                int timeSlot = Convert.ToInt32(reservation.Time);
                                bool available = false;
                                switch (timeSlot)
                                {
                                    case 1:
                                        if (!tableReserve.TimeSlot_1_reserved.Contains(date)){ available = true; }
                                        else 
                                        { 
                                            Console.WriteLine("Your current timeslot is not available on this date.");
                                            Console.WriteLine("Do you wish to also change the timeslot, to delete the reservation, or to keep the current arrangement?");
                                            Console.WriteLine("(1) Change timeslot\n(2) Delete Reservation\n(3) Keep current arrangement");
                                            string ChangeChoice = Console.ReadLine();
                                            if(ChangeChoice == "1") { My_Reservation.Remove(reservation); _changeTimeSlot(date, tableReserve, reservation); Console.WriteLine("Reservation updated\nPress enter to continue"); Console.ReadLine();}
                                            if (ChangeChoice == "2") { My_Reservation.Remove(reservation); Console.WriteLine("Reservation deleted\nPress enter to continue"); Console.ReadLine(); }
                                            if(ChangeChoice == "3") { Console.WriteLine("Date not changed\nPress enter to continue"); Console.ReadLine(); }

                                        }
                                        break;
                                    case 2:
                                        if (!tableReserve.TimeSlot_2_reserved.Contains(date)) { available = true; }
                                        else
                                        {
                                            Console.WriteLine("Your current timeslot is not available on this date.");
                                            Console.WriteLine("Do you wish to also change the timeslot, to delete the reservation, or to keep the current arrangement?");
                                            Console.WriteLine("(1) Change timeslot\n(2) Delete Reservation\n(3) Keep current arrangement");
                                            string ChangeChoice = Console.ReadLine();
                                            if (ChangeChoice == "1") { My_Reservation.Remove(reservation); _changeTimeSlot(date, tableReserve, reservation); Console.WriteLine("Reservation updated\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "2") { My_Reservation.Remove(reservation); Console.WriteLine("Reservation deleted\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "3") { Console.WriteLine("Date not changed\nPress enter to continue"); Console.ReadLine(); }

                                        }
                                        break;
                                    case 3:
                                        if (!tableReserve.TimeSlot_3_reserved.Contains(date)) { available = true; }
                                        else
                                        {
                                            Console.WriteLine("Your current timeslot is not available on this date.");
                                            Console.WriteLine("Do you wish to also change the timeslot, to delete the reservation, or to keep the current arrangement?");
                                            Console.WriteLine("(1) Change timeslot\n(2) Delete Reservation\n(3) Keep current arrangement");
                                            string ChangeChoice = Console.ReadLine();
                                            if (ChangeChoice == "1") { My_Reservation.Remove(reservation); _changeTimeSlot(date, tableReserve, reservation); Console.WriteLine("Reservation updated\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "2") { My_Reservation.Remove(reservation); Console.WriteLine("Reservation deleted\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "3") { Console.WriteLine("Date not changed\nPress enter to continue"); Console.ReadLine(); }

                                        }
                                        break;
                                    case 4:
                                        if (!tableReserve.TimeSlot_4_reserved.Contains(date)) { available = true; }
                                        else
                                        {
                                            Console.WriteLine("Your current timeslot is not available on this date.");
                                            Console.WriteLine("Do you wish to also change the timeslot, to delete the reservation, or to keep the current arrangement?");
                                            Console.WriteLine("(1) Change timeslot\n(2) Delete Reservation\n(3) Keep current arrangement");
                                            string ChangeChoice = Console.ReadLine();
                                            if (ChangeChoice == "1") { My_Reservation.Remove(reservation); _changeTimeSlot(date, tableReserve, reservation); Console.WriteLine("Reservation updated\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "2") { My_Reservation.Remove(reservation); Console.WriteLine("Reservation deleted\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "3") { Console.WriteLine("Date not changed\nPress enter to continue"); Console.ReadLine(); }

                                        }
                                        break;
                                    case 5:
                                        if (!tableReserve.TimeSlot_5_reserved.Contains(date)) { available = true; }
                                        else
                                        {
                                            Console.WriteLine("Your current timeslot is not available on this date.");
                                            Console.WriteLine("Do you wish to also change the timeslot, to delete the reservation, or to keep the current arrangement?");
                                            Console.WriteLine("(1) Change timeslot\n(2) Delete Reservation\n(3) Keep current arrangement");
                                            string ChangeChoice = Console.ReadLine();
                                            if (ChangeChoice == "1") { My_Reservation.Remove(reservation); _changeTimeSlot(date, tableReserve, reservation); Console.WriteLine("Reservation updated\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "2") { My_Reservation.Remove(reservation); Console.WriteLine("Reservation deleted\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "3") { Console.WriteLine("Date not changed\nPress enter to continue"); Console.ReadLine(); }

                                        }
                                        break;
                                    case 6:
                                        if (!tableReserve.TimeSlot_6_reserved.Contains(date)) { available = true; }
                                        else
                                        {
                                            Console.WriteLine("Your current timeslot is not available on this date.");
                                            Console.WriteLine("Do you wish to also change the timeslot, to delete the reservation, or to keep the current arrangement?");
                                            Console.WriteLine("(1) Change timeslot\n(2) Delete Reservation\n(3) Keep current arrangement");
                                            string ChangeChoice = Console.ReadLine();
                                            if (ChangeChoice == "1") { My_Reservation.Remove(reservation); _changeTimeSlot(date, tableReserve, reservation); Console.WriteLine("Reservation updated\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "2") { My_Reservation.Remove(reservation); Console.WriteLine("Reservation deleted\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "3") { Console.WriteLine("Date not changed\nPress enter to continue"); Console.ReadLine(); }

                                        }
                                        break;
                                    case 7:
                                        if (!tableReserve.TimeSlot_7_reserved.Contains(date)) { available = true; }
                                        else
                                        {
                                            Console.WriteLine("Your current timeslot is not available on this date.");
                                            Console.WriteLine("Do you wish to also change the timeslot, to delete the reservation, or to keep the current arrangement?");
                                            Console.WriteLine("(1) Change timeslot\n(2) Delete Reservation\n(3) Keep current arrangement");
                                            string ChangeChoice = Console.ReadLine();
                                            if (ChangeChoice == "1") { My_Reservation.Remove(reservation); _changeTimeSlot(date, tableReserve, reservation); Console.WriteLine("Reservation updated\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "2") { My_Reservation.Remove(reservation); Console.WriteLine("Reservation deleted\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "3") { Console.WriteLine("Date not changed\nPress enter to continue"); Console.ReadLine(); }

                                        }
                                        break;
                                    case 8:
                                        if (!tableReserve.TimeSlot_8_reserved.Contains(date)) { available = true; }
                                        else
                                        {
                                            Console.WriteLine("Your current timeslot is not available on this date.");
                                            Console.WriteLine("Do you wish to also change the timeslot, to delete the reservation, or to keep the current arrangement?");
                                            Console.WriteLine("(1) Change timeslot\n(2) Delete Reservation\n(3) Keep current arrangement");
                                            string ChangeChoice = Console.ReadLine();
                                            if (ChangeChoice == "1") { My_Reservation.Remove(reservation); _changeTimeSlot(date, tableReserve, reservation); Console.WriteLine("Reservation updated\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "2") { My_Reservation.Remove(reservation); Console.WriteLine("Reservation deleted\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "3") { Console.WriteLine("Date not changed\nPress enter to continue"); Console.ReadLine(); }

                                        }
                                        break;
                                    case 9:
                                        if (!tableReserve.TimeSlot_9_reserved.Contains(date)) { available = true; }
                                        else
                                        {
                                            Console.WriteLine("Your current timeslot is not available on this date.");
                                            Console.WriteLine("Do you wish to also change the timeslot, to delete the reservation, or to keep the current arrangement?");
                                            Console.WriteLine("(1) Change timeslot\n(2) Delete Reservation\n(3) Keep current arrangement");
                                            string ChangeChoice = Console.ReadLine();
                                            if (ChangeChoice == "1") { My_Reservation.Remove(reservation); _changeTimeSlot(date, tableReserve, reservation); Console.WriteLine("Reservation updated\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "2") { My_Reservation.Remove(reservation); Console.WriteLine("Reservation deleted\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "3") { Console.WriteLine("Date not changed\nPress enter to continue"); Console.ReadLine(); }

                                        }
                                        break;
                                    case 10:
                                        if (!tableReserve.TimeSlot_10_reserved.Contains(date)) { available = true; }
                                        else
                                        {
                                            Console.WriteLine("Your current timeslot is not available on this date.");
                                            Console.WriteLine("Do you wish to also change the timeslot, to delete the reservation, or to keep the current arrangement?");
                                            Console.WriteLine("(1) Change timeslot\n(2) Delete Reservation\n(3) Keep current arrangement");
                                            string ChangeChoice = Console.ReadLine();
                                            if (ChangeChoice == "1") { My_Reservation.Remove(reservation); _changeTimeSlot(date, tableReserve, reservation); Console.WriteLine("Reservation updated\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "2") { My_Reservation.Remove(reservation); Console.WriteLine("Reservation deleted\nPress enter to continue"); Console.ReadLine(); }
                                            if (ChangeChoice == "3") { Console.WriteLine("Date not changed\nPress enter to continue"); Console.ReadLine(); }

                                        }
                                        break;
                                }
                                if (available)
                                {
                                    reservation.Date = date;
                                    Console.WriteLine("Date updated\nPress enter to continue");
                                    Console.ReadLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Date not changed\nPress enter to continue");
                                Console.ReadLine();
                            }
                        }
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

    private void _changeTimeSlot(DateOnly date, Table tableReserve, Reservation reservation)
    {
        bool t1 = false;
        bool t2 = false;
        bool t3 = false;
        bool t4 = false;
        bool t5 = false;
        bool t6 = false;
        bool t7 = false;
        bool t8 = false;
        bool t9 = false;
        bool t10 = false;

        if (!tableReserve.TimeSlot_1_reserved.Contains(date)) { Console.WriteLine("Timeslot 1: 10:00-11:00"); t1 = true; }
        if (!tableReserve.TimeSlot_2_reserved.Contains(date)) { Console.WriteLine("Timeslot 2: 11:00-12:00"); t2 = true; }
        if (!tableReserve.TimeSlot_3_reserved.Contains(date)) { Console.WriteLine("Timeslot 3: 12:00-13:00"); t3 = true; }
        if (!tableReserve.TimeSlot_4_reserved.Contains(date)) { Console.WriteLine("Timeslot 4: 13:00-14:00"); t4 = true; }
        if (!tableReserve.TimeSlot_5_reserved.Contains(date)) { Console.WriteLine("Timeslot 5: 14:00-15:00"); t5 = true; }
        if (!tableReserve.TimeSlot_6_reserved.Contains(date)) { Console.WriteLine("Timeslot 6: 15:00-16:00"); t6 = true; }
        if (!tableReserve.TimeSlot_7_reserved.Contains(date)) { Console.WriteLine("Timeslot 7: 16:00-17:00"); t7 = true; }
        if (!tableReserve.TimeSlot_8_reserved.Contains(date)) { Console.WriteLine("Timeslot 8: 17:00-18:00"); t8 = true; }
        if (!tableReserve.TimeSlot_9_reserved.Contains(date)) { Console.WriteLine("Timeslot 9: 18:00-20:00"); t9 = true; }
        if (!tableReserve.TimeSlot_10_reserved.Contains(date)) { Console.WriteLine("Timeslot 10:20:00-22:00"); t10 = true; }
        string? time = Console.ReadLine();
        if (Int32.TryParse(time, out int timeslot))
        {
            switch (timeslot)
            {
                case 1:
                    if (t1) { tableReserve.TimeSlot_1_reserved.Add(date); Console.WriteLine($"Table {reservation.Table} reserved for 10:00 until 11:00"); reservation.Time = time; reservation.Date = date;  My_Reservation.Add(reservation); }
                    else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                    break;
                case 2:
                    if (t2) { tableReserve.TimeSlot_2_reserved.Add(date); Console.WriteLine($"Table {reservation.Table} reserved for 11:00 until 12:00"); reservation.Time = time; reservation.Date = date; My_Reservation.Add(reservation); }
                    else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                    break;
                case 3:
                    if (t3) { tableReserve.TimeSlot_3_reserved.Add(date); Console.WriteLine($"Table {reservation.Table} reserved for 12:00 until 13:00"); reservation.Time = time; reservation.Date = date; My_Reservation.Add(reservation); }
                    else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                    break;
                case 4:
                    if (t4) { tableReserve.TimeSlot_4_reserved.Add(date); Console.WriteLine($"Table {reservation.Table} reserved for 13:00 until 14:00"); reservation.Time = time; reservation.Date = date; My_Reservation.Add(reservation); }
                    else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                    break;
                case 5:
                    if (t5) { tableReserve.TimeSlot_5_reserved.Add(date); Console.WriteLine($"Table {reservation.Table} reserved for 14:00 until 15:00"); reservation.Time = time; reservation.Date = date; My_Reservation.Add(reservation); }
                    else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                    break;
                case 6:
                    if (t6) { tableReserve.TimeSlot_6_reserved.Add(date); Console.WriteLine($"Table {reservation.Table} reserved for 15:00 until 16:00"); reservation.Time = time; reservation.Date = date; My_Reservation.Add(reservation); }
                    else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                    break;
                case 7:
                    if (t7) { tableReserve.TimeSlot_7_reserved.Add(date); Console.WriteLine($"Table {reservation.Table} reserved for 16:00 until 17:00"); reservation.Time = time; reservation.Date = date; My_Reservation.Add(reservation); }
                    else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                    break;
                case 8:
                    if (t8) { tableReserve.TimeSlot_8_reserved.Add(date); Console.WriteLine($"Table {reservation.Table} reserved for 17:00 until 18:00"); reservation.Time = time; reservation.Date = date; My_Reservation.Add(reservation); }
                    else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                    break;
                case 9:
                    if (t9) { tableReserve.TimeSlot_9_reserved.Add(date); Console.WriteLine($"Table {reservation.Table} reserved for 18:00 until 20:00"); reservation.Time = time; reservation.Date = date; My_Reservation.Add(reservation); }
                    else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                    break;
                case 10:
                    if (t10) { tableReserve.TimeSlot_10_reserved.Add(date); Console.WriteLine($"Table {reservation.Table} reserved for 20:00 until 22:00"); reservation.Time = time; reservation.Date = date; My_Reservation.Add(reservation); }
                    else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                    break;
            }
        }
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
