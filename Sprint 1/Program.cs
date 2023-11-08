public class Program
{
    public static void Main()
    {
        Table_6 table_1 = new Table_6();
        Table_6 table_2 = new Table_6();

        Table_4 table_3 = new Table_4();
        Table_4 table_4 = new Table_4();
        Table_4 table_5 = new Table_4();
        Table_4 table_6 = new Table_4();
        Table_4 table_7 = new Table_4();

        Table_2 table_8 = new Table_2();
        Table_2 table_9 = new Table_2();
        Table_2 table_10 = new Table_2();
        Table_2 table_11 = new Table_2();

        Table_2 table_12 = new Table_2();
        Table_2 table_13 = new Table_2();
        Table_2 table_14 = new Table_2();
        Table_2 table_15 = new Table_2();

        /*--------------------------------------------------------------------------------------------------------------------------------*/
        Logo();
        Console.ForegroundColor = ConsoleColor.Green;
        RestaurantLayout.ViewLayout();
        Console.ForegroundColor = ConsoleColor.White;
        Account? curr_account = null;
        string? input = null;
        while (input != "Q")
        {
            if (curr_account == null)
            {
                curr_account = No_Account_Menu();
            }
            else if (curr_account as Admin is not null)
            {
                Admin_Menu();
                curr_account = null;
            }
            else if (curr_account as Customer is not null)
            {
                CustomerMenu((Customer)curr_account);
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

    public static SuperAdmin? SuperAdmin_log_in(string? name, string? email, string? password)
    {
        SuperAdmin admin = AccountManager.superAdmin;
        if (admin.Name == name && admin.Email == email && admin.Password == password)
        {
            return admin;
        }
        return null;
    }

    public static Admin? Admin_log_in(string? name, string? email, string? password)
    {
        foreach (Admin admin in AccountManager.Admins)
        {
            if (admin.Name == name && admin.Password == password && admin.Email == email)
            {
                return admin;
            }
        }
        return null;
    }

    public static Customer? Customer_log_in(string? name, string? email, string? password)
    {
        foreach (Customer customer in AccountManager.Customers)
        {
            if (customer.Name == name && customer.Password == password && customer.Email == email)
            {
                return customer;
            }
        }
        return null;
    }
    public static Account? No_Account_Menu()
    {
        Console.WriteLine("Here are your options:");
        Console.WriteLine("(1) Log in");
        Console.WriteLine("(2) View Menu");
        Console.WriteLine("(3) View Restaurant info");
        Console.WriteLine("(4) Create account");
        string? input = Console.ReadLine();
        if (input == "1")
        {
            Console.Write("Enter the role for the appropriate log in page (C)ustomer, (A)dmin, (S)uperAdmin: ");
            string? Role = Console.ReadLine();
            Console.Write("Enter your name: ");
            string? Name = Console.ReadLine();
            Console.Write("Enter your email: ");
            string? Email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string? Password = Console.ReadLine();
            if (Role is not null)
            {
                if (Role.ToUpper() == "C")
                {
                    return Customer_log_in(Name, Email, Password);
                }
                if (Role.ToUpper() == "A")
                {
                    return Admin_log_in(Name, Email, Password);
                }
                if (Role.ToUpper() == "S")
                {
                    return SuperAdmin_log_in(Name, Email, Password);
                }
            }
        }
        else if (input == "2")
        {
            Menu.view();
        }
        else if (input == "3")
        {
            RestaurantInfo info = new();
            info.Info_Restaurant();
        }
        else if (input == "4")
        {
            Console.Write("What is your name? ");
            string? name = Console.ReadLine();
            Console.Write("What is your email? ");
            string? email = Console.ReadLine();
            Console.WriteLine("What do you want your password to be? ");
            string? password = Console.ReadLine();
            if(name is not null && email is not null && password is not null)
                return Customer.CreateAccount(name, email, password);
        }
        return null;
    }
    public static void Admin_Menu()
    {
        while (true)
        {
            Console.WriteLine("Here are your options:");
            Console.WriteLine("(1) View Menu");
            Console.WriteLine("(2) Change Menu");
            Console.WriteLine("(3) View all reservations");
            Console.WriteLine("(4) Log out");
            string? input = Console.ReadLine();
            if (input == "1")
            {
                Menu.view();
            }
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
            if (input == "3")
            {
                foreach (Reservation reservation in Reservation.All_Reservations)
                {
                    Console.WriteLine(reservation.Reservation_Info());
                }
            }
            if (input == "4")
            {
                return;
            }
        }
    }
    public static void CustomerMenu(Customer customer)
    {
        while (true)
        {
            Console.WriteLine("(1) View Menu");
            Console.WriteLine("(2) Reserve a table");
            Console.WriteLine("(3) View reservations");
            Console.WriteLine("(4) Log out");
            string? input = Console.ReadLine();
            if (input is not null)
            {
                if (input == "1")
                {
                    Menu.view();
                }
                if (input == "2")
                {
                    RestaurantLayout.ViewLayout();
                    Console.Write("What table do you wish to reserve? (1-15, bar cannot be reserved) ");
                    int? table = Convert.ToInt32(Console.ReadLine());
                    Console.Write("How many guests do you expect? ");
                    int? guests = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Which date do you wish to reserve the table? (DD/MM/YYYY) ");
                    string? date = Console.ReadLine();
                    Console.Write("What time do you wish to arrive? ");
                    string? time = Console.ReadLine();
                    if (table is not null && guests is not null && date is not null && time is not null)
                        customer.Add_Reservation((int)table, (int)guests, date, time);
                }
                if (input == "3")
                {
                    customer.View_Reservation();
                }
                if (input == "4")
                {
                    return;
                }
            }
        }
    }
}
