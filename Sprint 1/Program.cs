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
        Console.ForegroundColor = ConsoleColor.Green;
        RestaurantLayout.ViewLayout();
        Account? curr_account = null;
        while (true)
        {
            Logo();
            string? input = null;
            while (input != "Q")
            {
                if (curr_account == null)
                {
                    No_Account_Menu();
                }
                else if (curr_account.Role == "Admin")
                {
                    Admin_Menu();
                }
                else if (curr_account.Role == "Customer")
                {
                    Console.WriteLine("(1) View Menu");
                    Console.WriteLine("(2) Reserve a table");
                    Console.WriteLine("(3) Log out");

                }
                if (input == "Q")
                    break;
                
            }
        }

    }

    public static void Logo()
    {
        Console.WriteLine("""










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
            if(Role is not null)
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
        return null;
    }
    public static void Admin_Menu()
    {
        Console.WriteLine("Here are your options:");
        Console.WriteLine("(1) View Menu");
        Console.WriteLine("(2) Change Menu");
        Console.WriteLine("(3) View all reservations");
        Console.WriteLine("(4) Log out");
        string? input = Console.ReadLine();
        if(input == "1")
        {
            Menu.view();
        }
        if(input == "2")
        {
            //TODO
        }
        if(input == "3")
        {
            foreach(Reservation reservation in Reservation.All_Reservations)
            {
                Console.WriteLine(reservation.Reservation_Info());
            }
        }
    }
}
