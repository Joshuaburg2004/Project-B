using System.Net.Sockets;
using System.Text.RegularExpressions;

public class Program
{
    public static void Main()
    {
        /*--------------------------------------------------------------------------------------------------------------------------------*/
        Logo();
        //Console.ForegroundColor = ConsoleColor.Green;
        //RestaurantLayout.ViewLayout();
        IAccount? curr_account = null;
        string? input = null;
        while (input != "Q")
        {
            if (curr_account == null)
            {
                curr_account = NoAccountMenu();
            }
            else if (curr_account as Admin is not null && curr_account as SuperAdmin is null)
            {
                AdminMenu();
                curr_account = null;
            }
            else if (curr_account as Customer is not null)
            {
                CustomerMenu((Customer)curr_account);
                curr_account = null;
            }
            else if (curr_account as SuperAdmin is not null)
            {
                SuperAdminMenu();
                curr_account = null;
            }
            if (input == "Q")
                break;
        }
    }

    public static void Logo()
    {
        Console.WriteLine("""
             _____           _                              _      ___  __  ___  
            |  __ \         | |                            | |    / _ \/_ |/ _ \ 
            | |__) |___  ___| |_ __ _ _   _ _ __ __ _ _ __ | |_  | | | || | | | |
            |  _  // _ \/ __| __/ _` | | | | '__/ _` | '_ \| __| | | | || | | | |
            | | \ \  __/\__ \ || (_| | |_| | | | (_| | | | | |_  | |_| || | |_| |
            |_|  \_\___||___/\__\__,_|\__,_|_|  \__,_|_| |_|\__|  \___/ |_|\___/ 
                                                                        
                                                                      
            """);
    }

    public static SuperAdmin? SuperAdminLogIn(string? name, string? email, string? password)
    {
        SuperAdmin admin = Manager.superAdmin;
        if (admin.Name == name && admin.Email == email && admin.Password == password)
        {
            return admin;
        }
        return null;
    }

    public static Admin? AdminLogIn(string? name, string? email, string? password)
    {
        foreach (Admin admin in Manager.Admins)
        {
            if (admin.Name == name && admin.Password == password && admin.Email == email)
            {
                return admin;
            }
        }
        return null;
    }

    public static Customer? CustomerLogIn(string? name, string? email, string? password)
    {
        foreach (Customer customer in Manager.Customers)
        {
            if (customer.Name == name && customer.Password == password && customer.Email == email)
            {
                return customer;
            }
        }
        return null;
    }
    //------------------------------------------------------------------ no account -------------------------------------------
    public static IAccount? NoAccountMenu()
    {
        Console.Clear();
        Logo();
        Console.WriteLine("Here are your options:");
        Console.WriteLine("(1) Log in");
        Console.WriteLine("(2) View Menu");
        Console.WriteLine("(3) View Restaurant info");
        Console.WriteLine("(4) Create account");
        Console.WriteLine("(5) Log in - Admin");
        Console.WriteLine("(6) Close app");
        string? input = Console.ReadLine();
        if (input == "1")
        {
            Console.Write("Enter your name: ");
            string? Name = Console.ReadLine();
            Console.Write("Enter your email: ");
            string? Email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string? Password = Console.ReadLine();
            return CustomerLogIn(Name, Email, Password);
        }
        else if (input == "2")
        {
            Menu.view();
            Console.WriteLine("\npress the enter key to continue");
            Console.ReadLine();
        }
        else if (input == "3")
        {
            RestaurantInfo info = new();
            info.Info_Restaurant();
            Console.WriteLine("\npress the enter key to continue");
            Console.ReadLine();
        }
        else if (input == "4")
        {
            bool NameCheck = false;
            bool EmailCheck = false;
            bool PassCheck = false;
            string? name = "";
            string? email = "";
            string? password = "";
            // Corrigeerd de naam
            while (NameCheck is false)
            {
                Console.Write("What is your name? ");
                name = Console.ReadLine();
                if (name is not null)
                {
                    if (!Regex.Match(name, @"[^\sa-zA-Z]").Success)
                    {
                        NameCheck = true;
                    }
                    else
                    {
                        Console.WriteLine("Name was invalid, please try again");
                    }
                }
            }
            // corrigeerd de email
            while (EmailCheck is false)
            {
                Console.Write("What is your email (Requires a dot and an @)? ");
                email = Console.ReadLine();
                if (email is not null)
                {
                    // prevents likes of @. - unlike contains
                    if (Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                    {
                        EmailCheck = true;
                    }
                    else
                    {
                        Console.WriteLine("Email was invalid, please try again");
                    }
                }
            }
            // corrigeerd de wachtwoorden
            while (PassCheck is false)
            {
                Console.Write("What do you want your password to be (Requires a capital letter, a number and a special character)? ");
                password = Console.ReadLine();
                if (password is not null)
                {
                    if (!Regex.Match(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$").Success) // => {8,15} is special character
                    {
                        PassCheck = true;
                    }
                    else
                    {
                        Console.WriteLine("Password was invalid, please try again");
                    }
                }
            }
            // maakt de account aan en logt je meteen in
            if (name is not null && email is not null && password is not null)
                return Customer.CreateAccount(name, email, password);
        }
        // logt de admins in
        else if (input == "5")
        {
            Console.WriteLine("Are you an (A)dmin or (S)uperAdmin");
            string? Role = Console.ReadLine();
            Console.Write("Enter your name: ");
            string? Name = Console.ReadLine();
            Console.Write("Enter your email: ");
            string? Email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string? Password = Console.ReadLine();
            if (Role!.ToUpper() == "A")
            {
                return AdminLogIn(Name, Email, Password);
            }
            if (Role!.ToUpper() == "S")
            {
                return SuperAdminLogIn(Name, Email, Password);
            }
        }
        // escapes the program                                    
        else if (input == "6")
        {
            Console.WriteLine("Goodbye and see you soon!");
            System.Environment.Exit(0);
        }
        return null;
    }
    // -------------------------------------------- menu voor de admins ------------------------------------------------------------------
    public static void AdminMenu()
    {
        while (true)
        {
            Console.Clear();
            Logo();
            Console.WriteLine("Here are your options:");
            Console.WriteLine("(1) View Menu");
            Console.WriteLine("(2) Change Menu");
            Console.WriteLine("(3) View all reservations");
            Console.WriteLine("(4) Log out");
            Console.WriteLine("(5) Close app");
            string? input = Console.ReadLine();
            // laat het menu zien
            if (input == "1")
            {
                Menu.view();
                Console.WriteLine("\npress the enter key to continue");
                Console.ReadLine();
            }
            // laat je een item toevoegen of verwijderen
            if (input == "2")
            {
                Console.WriteLine("(A) Add an item\n(R) Remove an item");
                string? choice = Console.ReadLine();
                if (choice is not null)
                {
                    if (choice == "A")
                    {
                        Console.WriteLine("Enter the name of the item: ");
                        string? name1 = Console.ReadLine();
                        Console.WriteLine("Enter the category (fish/meat/vegan/vegetarian):");
                        string? category1 = Console.ReadLine();
                        Console.WriteLine("Enter the price:");
                        string? price1 = Console.ReadLine();
                        bool Added_Bool = Menu_List.Add_item(name1, category1, price1);
                        if (Added_Bool)
                        {
                            Console.WriteLine($"Added {name1} to the menu.");
                        }
                        else
                        {
                            if (name1 is null)
                                Console.WriteLine("No name was submitted.");
                            if (category1 is null)
                                Console.WriteLine("No category was submitted.");
                            if (price1 is null)
                                Console.WriteLine("No price was submitted.");
                            Console.WriteLine("Item was not added.");
                        }
                    }
                    else if (choice == "R")
                    {
                        Console.WriteLine("Enter the name of the item: ");
                        string? name1 = Console.ReadLine();
                        Console.WriteLine("Enter the category (fish/meat/vegan/vegetarian):");
                        string? category1 = Console.ReadLine();
                        Console.WriteLine("Enter the price:");
                        string? price1 = Console.ReadLine();
                        bool Removed_Bool = Menu_List.Remove_item(name1, category1, price1);
                        if (Removed_Bool)
                        {
                            Console.WriteLine($"Removed {name1} from the menu.");
                        }
                        else
                        {
                            if (name1 is null)
                                Console.WriteLine("No name was submitted.");
                            if (category1 is null)
                                Console.WriteLine("No category was submitted.");
                            if (price1 is null)
                                Console.WriteLine("No price was submitted.");
                            Console.WriteLine("Item was not removed.");
                        }
                    }
                }
            }
            // laat de reservaties zien. TODO: per dag/met pagina's
            if (input == "3")
            {
                foreach (Reservation reservation in Reservation.All_Reservations)
                {
                    Console.WriteLine(reservation.Reservation_Info());
                }
                Console.WriteLine("\npress the enter key to continue");
                Console.ReadLine();
            }
            // logt uit
            if (input == "4")
            {
                return;
            }
            // exit het programma
            if (input == "5")
            {
                Console.WriteLine("Goodbye and see you soon!");
                System.Environment.Exit(0);
            }
        }
    }
    // ------------------------------------------------------- Superadmin --------------------------------------------------------
    public static void SuperAdminMenu()
    {
        while (true)
        {
            Console.Clear();
            Logo();
            Console.WriteLine("Here are your options:");
            Console.WriteLine("(1) View Menu");
            Console.WriteLine("(2) Change Menu");
            Console.WriteLine("(3) View all reservations");
            Console.WriteLine("(4) Add Admin");
            Console.WriteLine("(5) Log out");
            Console.WriteLine("(6) Close app");
            string? input = Console.ReadLine();
            // laat het menu zien
            if (input == "1")
            {
                Menu.view();
                Console.WriteLine("\npress the enter key to continue");
                Console.ReadLine();
            }
            // laat je een item toevoegen of verwijderen
            if (input == "2")
            {
                Console.WriteLine("(A) Add an item\n(R) Remove an item");
                string? choice = Console.ReadLine();
                if (choice is not null)
                {
                    if (choice == "A")
                    {
                        Console.WriteLine("Enter the name of the item: ");
                        string? name1 = Console.ReadLine();
                        Console.WriteLine("Enter the category (fish/meat/vegan/vegetarian):");
                        string? category1 = Console.ReadLine();
                        Console.WriteLine("Enter the price:");
                        string? price1 = Console.ReadLine();
                        bool Added_Bool = Menu_List.Add_item(name1, category1, price1);
                        if (Added_Bool)
                        {
                            Console.WriteLine($"Added {name1} to the menu.");
                        }
                        else
                        {
                            if (name1 is null)
                                Console.WriteLine("No name was submitted.");
                            if (category1 is null)
                                Console.WriteLine("No category was submitted.");
                            if (price1 is null)
                                Console.WriteLine("No price was submitted.");
                            Console.WriteLine("Item was not added.");
                        }
                    }
                    else if (choice == "R")
                    {
                        Console.WriteLine("Enter the name of the item: ");
                        string? name1 = Console.ReadLine();
                        Console.WriteLine("Enter the category (fish/meat/vegan/vegetarian):");
                        string? category1 = Console.ReadLine();
                        Console.WriteLine("Enter the price:");
                        string? price1 = Console.ReadLine();
                        bool Removed_Bool = Menu_List.Remove_item(name1, category1, price1);
                        if (Removed_Bool)
                        {
                            Console.WriteLine($"Removed {name1} from the menu.");
                        }
                        else
                        {
                            if (name1 is null)
                                Console.WriteLine("No name was submitted.");
                            if (category1 is null)
                                Console.WriteLine("No category was submitted.");
                            if (price1 is null)
                                Console.WriteLine("No price was submitted.");
                            Console.WriteLine("Item was not removed.");
                        }
                    }
                }
            }
            // laat de reservaties zien. TODO: per dag/met pagina's
            if (input == "3")
            {
                foreach (Reservation reservation in Reservation.All_Reservations)
                {
                    Console.WriteLine(reservation.Reservation_Info());
                }
                Console.WriteLine("\npress the enter key to continue");
                Console.ReadLine();
            }
            if (input == "4")
            {
                bool NameCheck = false;
                bool EmailCheck = false;
                bool PassCheck = false;
                string? name = "";
                string? email = "";
                string? password = "";
                while (NameCheck is false)
                {
                    Console.Write("What is your name? ");
                    name = Console.ReadLine();
                    if (name is not null)
                    {
                        if (!Regex.Match(name, @"[^\sa-zA-Z]").Success)
                        {
                            NameCheck = true;
                        }
                        else
                        {
                            Console.WriteLine("Name was invalid, please try again");
                        }
                    }
                }
                while (EmailCheck is false)
                {
                    Console.Write("What is your email? ");
                    email = Console.ReadLine();
                    if (email is not null)
                    {
                        if ((Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase)))
                        {
                            EmailCheck = true;
                        }
                        else
                        {
                            Console.WriteLine("Email was invalid, please try again");
                        }
                    }
                }
                while (PassCheck is false)
                {
                    Console.Write("What do you want your password to be? ");
                    password = Console.ReadLine();
                    if (password is not null)
                    {
                        if (!Regex.Match(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$").Success)
                        {
                            PassCheck = true;
                        }
                        else
                        {
                            Console.WriteLine("Password was invalid, please try again");
                        }
                    }
                }
                if (name is not null && email is not null && password is not null)
                    Admin.CreateAdmin(name, email, password);
            }
            // logt uit
            if (input == "5")
            {
                return;
            }
            // exit het programma
            if (input == "6")
            {
                Console.WriteLine("Goodbye and see you soon!");
                System.Environment.Exit(0);
            }
        }
    }
    // -------------------------------------------------  menu voor de customers -------------------------------------------------
    public static void CustomerMenu(Customer customer)
    {
        while (true)
        {
            Console.Clear();
            // laat het logo zien
            Logo();
            Console.WriteLine("(1) View Menu");
            Console.WriteLine("(2) Reserve a table");
            Console.WriteLine("(3) View reservations");
            Console.WriteLine("(4) Log out");
            Console.WriteLine("(5) Close app");
            string? input = Console.ReadLine();
            if (input is not null)
            {
                // laat het menu zien
                if (input == "1")
                {
                    Menu.view();
                    Console.WriteLine("\npress the enter key to continue");
                    Console.ReadLine();
                }
                // Reservatie: neemt een timeslot en een datum, samen met de table en guests, en reserveert dan die
                if (input == "2")
                {
                    RestaurantLayout.ViewLayout();
                    int? table = 0;
                    while(!(table >= 1 && table <= 15)) 
                    {
                        Console.Write("What table do you wish to reserve? (1-15, bar cannot be reserved) ");
                        table = Convert.ToInt32(Console.ReadLine());
                    }
                    Table? tableReserve;
                    tableReserve = table switch
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
                    int? guests = 0;
                    while (guests < tableReserve!.MinGuests || guests > tableReserve!.MaxGuests)
                    {
                        Console.Write("How many guests do you expect? ");
                        guests = Convert.ToInt32(Console.ReadLine());
                    }
                    DateOnly date;
                    while (true)
                    {
                        Console.Write("Which date do you wish to reserve the table? (DD/MM/YYYY) ");
                        string? dateIn = Console.ReadLine();
                        if (DateOnly.TryParseExact(dateIn, "d/MM/yyyy", out date))
                        {
                            break;
                        }
                    }
                    bool t1 = false;
                    bool t2 = false;
                    bool t3 = false;
                    bool t4 = false;
                    if (!tableReserve.TimeSlot_1_reserved.Contains(date)) { Console.WriteLine("Timeslot 1: 17:00-17:30"); t1 = true; }
                    if (!tableReserve.TimeSlot_2_reserved.Contains(date)) { Console.WriteLine("Timeslot 2: 17:30-18:00"); t2 = true; }
                    if (!tableReserve.TimeSlot_3_reserved.Contains(date)) { Console.WriteLine("Timeslot 3: 18:00-18:30"); t3 = true; }
                    if (!tableReserve.TimeSlot_4_reserved.Contains(date)) { Console.WriteLine("Timeslot 4: 18:30-19:00"); t4 = true; }
                    string? time = Console.ReadLine();
                    if(Int32.TryParse(time, out int timeslot))
                    {
                        switch (timeslot)
                        {
                            case 1:
                                if (t1) { tableReserve.TimeSlot_1_reserved.Add(date); Console.WriteLine($"Table {table} reserved for 17:00 until 17:30"); }
                                else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                                break;
                            case 2:
                                if (t2) { tableReserve.TimeSlot_2_reserved.Add(date); Console.WriteLine($"Table {table} reserved for 17:30 until 18:00"); }
                                else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                                break;
                            case 3:
                                if (t3) { tableReserve.TimeSlot_3_reserved.Add(date); Console.WriteLine($"Table {table} reserved for 18:00 until 18:30"); }
                                else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                                break;
                            case 4:
                                if (t4) { tableReserve.TimeSlot_4_reserved.Add(date); Console.WriteLine($"Table {table} reserved for 18:30 until 19:00"); }
                                else { Console.WriteLine("Table was already reserved for this time. Please try again"); }
                                break;

                        }
                    }
                    
                }
                // laat de reservatie zien
                if (input == "3")
                {
                    customer.View_Reservation();
                    Console.WriteLine("\npress the enter key to continue");
                    Console.ReadLine();
                }
                // verandert de reservatie
                if (input == "4")
                {
                    customer.Change_Reservation();
                    Console.WriteLine("\npress the enter key to continue");
                    Console.ReadLine();
                }
                // logt uit
                if (input == "5")
                {
                    return;
                }
                // laat je het programma uit
                if (input == "6")
                {
                    Console.WriteLine("Goodbye and see you soon!");
                    System.Environment.Exit(0);
                }
            }
        }
    }
}
