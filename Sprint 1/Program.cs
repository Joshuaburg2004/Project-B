using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Diagnostics.Metrics;
using System.Net.Quic;
using System.Security.Principal;
using System.Text;
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
        else
        {
            Console.WriteLine("\nAccount not found \npress the enter key to continue");
            Console.ReadLine();
        }
        return null;
    }

    public static Admin? AdminLogIn(string? name, string? email, string? password)
    {
        int counter = 1;
        foreach (Admin admin in Manager.Admins)
        {
            if (admin.Name == name && admin.Password == password && admin.Email == email)
            {
                return admin;
            }
            if (counter == Manager.Admins.Count)
            {
                Console.WriteLine("\nAccount not found \npress the enter key to continue");
                Console.ReadLine();
            }
            counter++;
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
    public static Customer? CustomerLogIn(string? email, string? password)
    {
        int counter = 1;
        foreach (Customer customer in Manager.Customers)
        {
            if (customer.Password == password && customer.Email == email)
            {
                return customer;
            }
            if (counter == Manager.Customers.Count)
            {
                Console.WriteLine("\nAccount not found \npress the enter key to continue");
                Console.ReadLine();
            }
            counter++;
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
            Console.Write("Enter your email: ");
            string? Email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string? Password = Console.ReadLine();
            return CustomerLogIn(Email, Password);
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
                    else if (Regex.Match(name, @"\d").Success)
                    {
                        Console.WriteLine("Name cannot be a number. Please try again.");
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
                    else if (email.EndsWith("."))
                    {
                        Console.WriteLine("Email cannot end with a dot.");
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
                Console.Write("What do you want your password to be (Requires a capital letter, a lowercase, a number and a special character, 8 to 15 characters)? ");
                password = Console.ReadLine();
                if (password is not null)
                {
                    if (Regex.Match(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$").Success) // => {8,15} is special character
                    {
                        PassCheck = true;
                    }
                    else if(password.Length < 8)
                    {
                        Console.WriteLine("Password is too short, minimum of 8 characters needed.");
                    }
                    else if(password.Length > 15)
                    {
                        Console.WriteLine("Password is too long, maximum of 15 characters.");
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
                while (true)
                {
                    Console.WriteLine("(1) view all - 10 per page \n(2) sort by custommer ID \n(3) sort by date \n(4) sort by timeslot\n(5) show specific timeslot");
                    int? option = Convert.ToInt32(Console.ReadLine());
                    Reservation.All_Reservations.Sort();
                    if (option == 1)
                    {
                        IEnumerable<Reservation[]> reservations = Reservation.All_Reservations.Chunk(10);
                        int Index = 0;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine($"Page number {Index + 1}");
                                foreach (Reservation reservation in reservations.ElementAt(Index))
                                {
                                    Console.WriteLine(reservation.Reservation_Info());
                                }
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
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("List is empty\nPress enter to continue\n");
                                Console.ReadLine();
                                break;
                            }
                        }
                        break;
                    }
                    else if (option == 2)
                    {
                        List<Reservation[]> reservations = Reservation.All_Reservations.OrderBy(x => x.CustomerId).Chunk(10).ToList();
                        int Index = 0;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine($"Page number {Index + 1}");
                                foreach (Reservation reservation in reservations.ElementAt(Index))
                                {
                                    Console.WriteLine(reservation.Reservation_Info());
                                }
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
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("List is empty\nPress enter to continue\n");
                                Console.ReadLine();
                                break;
                            }
                            break;
                        }
                    }
                    else if (option == 3)
                    {
                        DateOnly today = DateOnly.FromDateTime(DateTime.Today);

                        List<Reservation[]> reservations = Reservation.All_Reservations.Where(x => x.Date == today).OrderBy(x => x.Date).Chunk(10).ToList();
                        int Index = 0;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine($"Page number {Index + 1}");
                                foreach (Reservation reservation in reservations.ElementAt(Index))
                                {
                                    Console.WriteLine(reservation.Reservation_Info());
                                }
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
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("List is empty\nPress enter to continue\n");
                                Console.ReadLine();
                                break;
                            }
                            break;
                        }
                        break;
                    }
                    else if (option == 4)
                    {
                        List<Reservation[]> reservations = Reservation.All_Reservations.OrderBy(x => x.Time).Where(x => x.Date == DateOnly.FromDateTime(DateTime.Now)).Chunk(10).ToList();
                        int Index = 0;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine($"Page number {Index + 1}");
                                foreach (Reservation reservation in reservations.ElementAt(Index))
                                {
                                    Console.WriteLine(reservation.Reservation_Info());
                                }
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
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("List is empty\nPress enter to continue\n");
                                Console.ReadLine();
                                break;
                            }
                            break;
                        }
                        break;
                    }
                    else if (option == 5)
                    {
                        Console.Write("Enter timeslot: ");
                        string timeslotInput = Console.ReadLine()!;

                        List<Reservation[]> reservations = Reservation.All_Reservations.Where(x => x.Time == timeslotInput).Where(x => x.Date == DateOnly.FromDateTime(DateTime.Now)).Chunk(10).ToList();

                        int Index = 0;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine($"Page number {Index + 1}");
                                foreach (Reservation reservation in reservations.ElementAt(Index))
                                {
                                    Console.WriteLine(reservation.Reservation_Info());
                                }
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
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("List is empty\nPress enter to continue\n");
                                Console.ReadLine();
                                break;
                            }
                            break;
                        }
                        break;
                    }
                    else
                        Console.WriteLine("Please enter a valid number");
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
            Console.WriteLine("(4) View reviews");
            Console.WriteLine("(5) Add Admin");
            Console.WriteLine("(6) Log out");
            Console.WriteLine("(7) Close app");
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
                while (true)
                {

                    Console.WriteLine("(1) view all - 10 per page \n(2) sort by custommer ID \n(3) sort by date \n(4) sort by timeslot\n(5) show specific timeslot");
                    int? option = Convert.ToInt32(Console.ReadLine());
                    Reservation.All_Reservations.Sort();
                    if (option == 1)
                    {
                        IEnumerable<Reservation[]> reservations = Reservation.All_Reservations.Chunk(10);
                        int Index = 0;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine($"Page number {Index + 1}");
                                foreach (Reservation reservation in reservations.ElementAt(Index))
                                {
                                    Console.WriteLine(reservation.Reservation_Info());
                                }
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
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("List is empty\nPress enter to continue\n");
                                Console.ReadLine();
                                break;
                            }
                        }
                        break;
                    }
                    else if (option == 2)
                    {
                        List<Reservation[]> reservations = Reservation.All_Reservations.OrderBy(x => x.CustomerId).Chunk(10).ToList();
                        int Index = 0;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine($"Page number {Index + 1}");
                                foreach (Reservation reservation in reservations.ElementAt(Index))
                                {
                                    Console.WriteLine(reservation.Reservation_Info());
                                }
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
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("List is empty\nPress enter to continue\n");
                                Console.ReadLine();
                                break;
                            }
                            break;
                        }
                    }
                    else if (option == 3)
                    {
                        DateOnly today = DateOnly.FromDateTime(DateTime.Today);

                        List<Reservation[]> reservations = Reservation.All_Reservations.Where(x => x.Date == today).OrderBy(x => x.Date).Chunk(10).ToList();
                        int Index = 0;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine($"Page number {Index + 1}");
                                foreach (Reservation reservation in reservations.ElementAt(Index))
                                {
                                    Console.WriteLine(reservation.Reservation_Info());
                                }
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
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("List is empty\nPress enter to continue\n");
                                Console.ReadLine();
                                break;
                            }
                            break;
                        }
                        break;
                    }
                    else if (option == 4)
                    {
                        List<Reservation[]> reservations = Reservation.All_Reservations.OrderBy(x => x.Time).Where(x => x.Date == DateOnly.FromDateTime(DateTime.Now)).Chunk(10).ToList();
                        int Index = 0;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine($"Page number {Index + 1}");
                                foreach (Reservation reservation in reservations.ElementAt(Index))
                                {
                                    Console.WriteLine(reservation.Reservation_Info());
                                }
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
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("List is empty\nPress enter to continue\n");
                                Console.ReadLine();
                                break;
                            }
                            break;
                        }
                        break;
                    }
                    else if (option == 5)
                    {
                        Console.Write("Enter timeslot: ");
                        string timeslotInput = Console.ReadLine()!;

                        List<Reservation[]> reservations = Reservation.All_Reservations.Where(x => x.Time == timeslotInput).Where(x => x.Date == DateOnly.FromDateTime(DateTime.Now)).Chunk(10).ToList();

                        int Index = 0;
                        while (true)
                        {
                            try
                            {
                                Console.WriteLine($"Page number {Index + 1}");
                                foreach (Reservation reservation in reservations.ElementAt(Index))
                                {
                                    Console.WriteLine(reservation.Reservation_Info());
                                }
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
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("List is empty\nPress enter to continue\n");
                                Console.ReadLine();
                                break;
                            }
                            break;
                        }
                        break;
                    }
                    else
                        Console.WriteLine("Please enter a valid number");
                }
                Console.WriteLine("\npress the enter key to continue");
                Console.ReadLine();
            }
            if (input == "4")
            {
                Console.WriteLine("(1) show all reviews");
                Console.WriteLine("(2) Show average rating");

                string choiceReview = Console.ReadLine()!;

                if (choiceReview == "1")
                {
                    Console.WriteLine($"All Reviews Count: {Review.AllReviews.Count}");

                    Console.WriteLine("All Reviews:");
                    foreach (Review review in Review.AllReviews)
                    {
                        Console.WriteLine($"ReviewID: {review.ReviewID}, CustomerID: {review.CustomerID}, Review: {review.Text}, Stars: {review.Stars}, Date: {review.Date.ToShortDateString()}");
                    }
                }
                if (choiceReview == "2")
                {
                    int totalreviews = Review.AllReviews.Count();
                    if(totalreviews > 0)
                    {
                        Console.WriteLine($"Total amounts of reviews: {Review.AllReviews.Count()}");
                        double stars_temp = 0;
                        

                        foreach( Review review in Review.AllReviews)
                        {
                            stars_temp += review.Stars;

                        }
                        double stars = stars_temp / totalreviews;
                        double stars_rounded = Math.Round(stars,1);
                        //Console.WriteLine($"All stars {stars_temp}");
                        if (stars_rounded >= 1 && stars_rounded < 2)
                        {
                            Console.WriteLine($"Average star rating: {stars_rounded} *");
                        }
                        if (stars_rounded >= 2 && stars_rounded < 3)
                        {
                            Console.WriteLine($"Average star rating: {stars_rounded} * *");
                        }
                        if (stars_rounded >= 3 && stars_rounded < 4)
                        {
                            Console.WriteLine($"Average star rating: {stars_rounded} * * *");
                        }
                        if (stars_rounded >= 4 && stars_rounded < 5)
                        {
                            Console.WriteLine($"Average star rating: {stars_rounded} * * * *");
                        }
                        if (stars_rounded >= 5)
                        {
                            Console.WriteLine($"Average star rating: {stars_rounded} * * * * *");
                        }

                    }
                    else
                    {
                        Console.WriteLine("There are no reviews");

                    }


                }
                

                Console.WriteLine("\npress the enter key to continue");
                Console.ReadLine();
            }

            if (input == "5")
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
            if (input == "6")
            {
                return;
            }
            // exit het programma
            if (input == "7")
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
            Console.WriteLine("(4) Change or Delete reservation");
            Console.WriteLine("(5) Leave a review");
            Console.WriteLine("(6) Log out");
            Console.WriteLine("(7) Close app");
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
                    // prototype reserveren
                    DateOnly date;
                    int guests = 0;
                    int table = 0;

                    bool end = false;
                    bool go_back_to_guests = true;
                    bool go_back_to_table = true;
                    bool go_back_to_date = true;
                    bool go_back_to_time = true;
                    bool bad_table_and_guests_combo = false;
                    //wat nieuws proberen
                    while (true)
                    {
                        bad_table_and_guests_combo = false;

                        // guest loop
                        while (go_back_to_guests is true)
                        {
                            Console.Write("How many guests do you expect? \n(0) to quit\n");
                            string? guestsIn = Console.ReadLine();
                            if (Int32.TryParse(guestsIn, out guests))
                            {
                                if (guests == 0)
                                {
                                    end = true;
                                    break;
                                }
                                else if (guests < 0)
                                {
                                    Console.WriteLine("Please enter a valid number");
                                    continue;
                                }
                                else if (guests > 6)
                                {
                                    RestaurantInfo info = new();
                                    Console.WriteLine($"If you wish to reserve a table for more than 6 guests you will need to call us at: {info.Telefoonnummer})");
                                    continue;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please enter a valid number");
                                continue;
                            }
                            

                            Console.WriteLine("Are you sure? \n(1) yes \n(2) no ");
                            string? confirmationString = Console.ReadLine();
                            if (int.TryParse(confirmationString, out int confirmation1))
                            {
                                if (confirmation1 == 1)
                                {
                                    go_back_to_guests = false;
                                    break;
                                }
                                else if (confirmation1 == 2)
                                {
                                    continue;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter 1 or 2");
                                    continue;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please enter 1 or 2");
                                continue;
                            }
                        }
                        if (end is true)
                            break;
                        // ------------------- date loop voor table door code!!!!
                        // table loop
                        while (go_back_to_table)
                        {

                            switch (guests)
                            {
                                case 1:
                                case 2:
                                    {
                                        Console.WriteLine("What table do you wish to reserve? (due to the number of guests you can only reserve tables 8 to 15) \n(16) to go back to guest selection \n(17) to quit ");
                                        string? tableIn = Console.ReadLine();
                                        Int32.TryParse(tableIn, out table);
                                        if (table <= 0 || table >= 18)
                                        {
                                            Console.WriteLine("Please enter a valid table number");
                                            continue;
                                        }
                                        else if (table == 16)
                                        {
                                            Console.WriteLine("Are you sure you want to go back to guests selection? (1) yes (2) no ");
                                            string? confirmationString = Console.ReadLine();
                                            if (int.TryParse(confirmationString, out int confirmation))
                                            {
                                                if (confirmation == 1)
                                                {
                                                    go_back_to_guests = true;
                                                    break;
                                                }
                                                else if (confirmation == 2)
                                                {
                                                    continue;
                                                }
                                            }
                                            Console.WriteLine("Please enter 1 or 2");
                                            continue;
                                        }
                                        else if (table == 17)
                                        {
                                            Console.WriteLine("Are you sure you want to go quit? (1) yes (2) no ");
                                            string? confirmationString = Console.ReadLine();
                                            if (int.TryParse(confirmationString, out int confirmation))
                                            {
                                                if (confirmation == 1)
                                                {
                                                    end = true;
                                                    break;
                                                }
                                                else if (confirmation == 2)
                                                {
                                                    continue;
                                                }
                                                Console.WriteLine("Please enter 1 or 2");
                                                continue;

                                            }
                                            else
                                            {
                                                Console.WriteLine("Please enter 1 or 2");  
                                                continue;
                                            }
                                        }
                                        else if (table < 8)
                                        {
                                            Console.WriteLine("Due to the number of guests this table is not available");
                                            bad_table_and_guests_combo = true;
                                            break;
                                        }
                                        break;
                                    }
                                case 3:
                                case 4:
                                    {
                                        Console.WriteLine("What table do you wish to reserve? (due to the number of guests you can only reserve tables 3 to 7) \n(16) to go back to guest selection \n(17) to quit ");
                                        string? tableIn = Console.ReadLine();
                                        Int32.TryParse(tableIn, out table);
                                        if (table <= 0 || table >= 18)
                                        {
                                            Console.WriteLine("Please enter a valid table number");
                                            continue;
                                        }
                                        else if (table < 3 || table > 7)
                                        {
                                            Console.WriteLine("Due to the number of guests this table is not available");
                                            bad_table_and_guests_combo = true;
                                            break;
                                        }
                                        else if (table == 16)
                                        {
                                            Console.WriteLine("Are you sure you want to go back to guest selection? (1) yes (2) no ");
                                            string? confirmationString = Console.ReadLine();
                                            if (int.TryParse(confirmationString, out int confirmation))
                                            {
                                                if (confirmation == 1)
                                                {
                                                    go_back_to_guests = true;
                                                    break;
                                                }
                                                else if (confirmation == 2)
                                                {
                                                    continue;
                                                }
                                                Console.WriteLine("Please enter 1 or 2");
                                                continue;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Please enter 1 or 2");
                                                continue;
                                            }
                                        }
                                        else if (table == 17)
                                        {
                                            Console.WriteLine("Are you sure you want to go quit? (1) yes (2) no ");
                                            string? confirmationString = Console.ReadLine();
                                            if (int.TryParse(confirmationString, out int confirmation))
                                            {
                                                if (confirmation == 1)
                                                {
                                                    end = true;
                                                    break;
                                                }
                                                else if (confirmation == 2)
                                                {
                                                    break;
                                                }
                                                Console.WriteLine("Please enter 1 or 2");
                                                continue;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Please enter 1 or 2");
                                                continue;
                                            }
                                        }
                                        break;
                                    }
                                case 5:
                                case 6:
                                    {
                                        Console.WriteLine("What table do you wish to reserve? (due to the number of guests you can only reserve tables 1 or 2) \n(16) to go back to guest selection \n(17) to quit");
                                        string? tableIn = Console.ReadLine();
                                        Int32.TryParse(tableIn, out table);
                                        if (table <= 0 || table >= 18)
                                        {
                                            Console.WriteLine("Please enter a valid table number");
                                            continue;
                                        }
                                        if (table == 16)
                                        {
                                            Console.WriteLine("Are you sure you want to go back to guest selection? (1) yes (2) no ");
                                            string? confirmationString = Console.ReadLine();
                                            if (int.TryParse(confirmationString, out int confirmation))
                                            {
                                                if (confirmation == 1)
                                                {
                                                    go_back_to_guests = true;
                                                    break;
                                                }
                                                else if (confirmation == 2)
                                                {
                                                    continue;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Please enter 1 or 2");
                                                continue;
                                            }
                                        }
                                        if (table == 17)
                                        {
                                            Console.WriteLine("Are you sure you want to go quit? (1) yes (2) no ");
                                            string? confirmationString = Console.ReadLine();
                                            if (int.TryParse(confirmationString, out int confirmation))
                                            {
                                                if (confirmation == 1)
                                                {
                                                    end = true;
                                                    break;
                                                }
                                                else if (confirmation == 2)
                                                {
                                                    continue;
                                                }
                                            }
                                        }
                                        if (table > 3)
                                        {
                                            Console.WriteLine("Due to the number of guests this table is not available");
                                            bad_table_and_guests_combo = true;
                                            break;
                                        }
                                        break;
                                    }
                            }
                            if (table == 16)
                            {
                                break;
                            }
                            if (table == 17)
                            {
                                end = true;
                                break;
                            }
                            if (bad_table_and_guests_combo)
                            { 
                                break;
                            }
                            Console.WriteLine($"You chose table {table}, are you sure? (1) yes (2) no ");
                            string? confirmationString1 = Console.ReadLine();
                            if (int.TryParse(confirmationString1, out int confirmation1))
                            {
                                if (confirmation1 == 1)
                                {
                                    go_back_to_table = false;
                                    break;
                                }
                                else if (confirmation1 == 2)
                                {
                                    continue;
                                }
                                else
                                {
                                    Console.WriteLine("Please enter 1 or 2");
                                    continue;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Please enter 1 or 2");
                                continue;
                            }
                        }
                        if (bad_table_and_guests_combo)
                        {
                            go_back_to_guests = true;
                            continue;
                        }
                        if (go_back_to_guests)
                        {
                            continue;
                        }
                        if (end)
                        {
                            break;
                        }
                        
                        // date loop
                        while (go_back_to_date)
                        {
                            while (true)
                            {
                                Console.Write("Which date do you wish to reserve the table? (DD/MM/YYYY) or press Q to quit: ");
                                string? dateIn = Console.ReadLine()!.ToUpper();
                                if (DateOnly.TryParseExact(dateIn, "dd/MM/yyyy", out date))
                                {
                                    if (date >= DateOnly.FromDateTime(DateTime.Now))
                                        break;
                                    Console.WriteLine("Incorrect date, please try again.");
                                }
                                else if (dateIn == "Q") { end = true; go_back_to_date = false; break; }
                            }
                            if (!end)
                            {
                                Console.WriteLine($"You chose {date}, Are you sure? (1) yes (2) no ");
                                string? confirmationString = Console.ReadLine();
                                if (int.TryParse(confirmationString, out int confirmation1))
                                {
                                    if (confirmation1 == 1)
                                    {
                                        go_back_to_date = false;
                                    }
                                    else if (confirmation1 == 2)
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                                Console.WriteLine(date);
                                int timeslot = 0; string? time = "";
                                while (go_back_to_time)
                                {
                                    for (int i = 1; i < Manager.table_list.Count; i++)
                                    {
                                        if (i == table)
                                        {
                                            if (!Manager.table_list[i - 1].TimeSlot_1_reserved.Contains(date)) Console.WriteLine("Timeslot 1: 10:00-11:00");
                                            if (!Manager.table_list[i - 1].TimeSlot_2_reserved.Contains(date)) Console.WriteLine("Timeslot 2: 11:00-12:00");
                                            if (!Manager.table_list[i - 1].TimeSlot_3_reserved.Contains(date)) Console.WriteLine("Timeslot 3: 12:00-13:00");
                                            if (!Manager.table_list[i - 1].TimeSlot_4_reserved.Contains(date)) Console.WriteLine("Timeslot 4: 13:00-14:00");
                                            if (!Manager.table_list[i - 1].TimeSlot_5_reserved.Contains(date)) Console.WriteLine("Timeslot 5: 14:00-15:00");
                                            if (!Manager.table_list[i - 1].TimeSlot_6_reserved.Contains(date)) Console.WriteLine("Timeslot 6: 15:00-16:00");
                                            if (!Manager.table_list[i - 1].TimeSlot_7_reserved.Contains(date)) Console.WriteLine("Timeslot 7: 16:00-17:00");
                                            if (!Manager.table_list[i - 1].TimeSlot_8_reserved.Contains(date)) Console.WriteLine("Timeslot 8: 17:00-18:00");
                                            if (!Manager.table_list[i - 1].TimeSlot_9_reserved.Contains(date)) Console.WriteLine("Timeslot 9: 18:00-20:00");
                                            if (!Manager.table_list[i - 1].TimeSlot_10_reserved.Contains(date)) Console.WriteLine("Timeslot 10: 20:00-22:00");
                                            break;
                                        }
                                    }
                                    Console.WriteLine("Enter the number of the timeslot you wish to reserve (11) quit (12) return to table selection (13) re-enter date and time");
                                    time = Console.ReadLine();
                                    if (Int32.TryParse(time, out timeslot))
                                    {
                                        if (timeslot == 11)
                                        {
                                            end = true;
                                            break;
                                        }
                                        if (timeslot == 12)
                                        {
                                            go_back_to_table = true;
                                            break;
                                        }
                                        if (timeslot == 13)
                                        {
                                            go_back_to_date = true;
                                            continue;
                                        }
                                        if (timeslot >= 14 || timeslot <= 0)
                                        {
                                            Console.WriteLine("Please enter a valid number");
                                        }
                                        bool t1 = timeslot == 1 && Manager.table_list[table - 1].TimeSlot_1_reserved.Contains(date);
                                        bool t2 = timeslot == 2 && Manager.table_list[table - 1].TimeSlot_2_reserved.Contains(date);
                                        bool t3 = timeslot == 3 && Manager.table_list[table - 1].TimeSlot_3_reserved.Contains(date);
                                        bool t4 = timeslot == 4 && Manager.table_list[table - 1].TimeSlot_4_reserved.Contains(date);
                                        bool t5 = timeslot == 5 && Manager.table_list[table - 1].TimeSlot_5_reserved.Contains(date);
                                        bool t6 = timeslot == 6 && Manager.table_list[table - 1].TimeSlot_6_reserved.Contains(date);
                                        bool t7 = timeslot == 7 && Manager.table_list[table - 1].TimeSlot_7_reserved.Contains(date);
                                        bool t8 = timeslot == 8 && Manager.table_list[table - 1].TimeSlot_8_reserved.Contains(date);
                                        bool t9 = timeslot == 9 && Manager.table_list[table - 1].TimeSlot_9_reserved.Contains(date);
                                        bool t0 = timeslot == 10 && Manager.table_list[table - 1].TimeSlot_10_reserved.Contains(date);
                                        if (t1) { Console.WriteLine("This timeslot is taken. "); }
                                        if (t2) { Console.WriteLine("This timeslot is taken. "); }
                                        if (t3) { Console.WriteLine("This timeslot is taken. "); }
                                        if (t4) { Console.WriteLine("This timeslot is taken. "); }
                                        if (t5) { Console.WriteLine("This timeslot is taken. "); }
                                        if (t6) { Console.WriteLine("This timeslot is taken. "); }
                                        if (t7) { Console.WriteLine("This timeslot is taken. "); }
                                        if (t8) { Console.WriteLine("This timeslot is taken. "); }
                                        if (t9) { Console.WriteLine("This timeslot is taken. "); }
                                        if (t0) { Console.WriteLine("This timeslot is taken. "); }
                                        else if(!(t1 || t2 || t3 || t4 || t5 || t6 || t7 || t8 || t9 || t0)){ go_back_to_time = false; break; }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Please enter a valid number");
                                        continue;
                                    }
                                }


                                Console.WriteLine($"Guests: {guests}, Table: {table}, Date: {date}");

                                switch (timeslot)
                                {
                                    case 1:
                                        Manager.table_list[table - 1].TimeSlot_1_reserved.Add(date);
                                        Console.WriteLine("Timeslot 1: 10:00-11:00");
                                        break;
                                    case 2:
                                        Manager.table_list[table - 1].TimeSlot_2_reserved.Add(date);
                                        Console.WriteLine("Timeslot 2: 11:00-12:00");
                                        break;
                                    case 3:
                                        Manager.table_list[table - 1].TimeSlot_3_reserved.Add(date);
                                        Console.WriteLine("Timeslot 3: 12:00-13:00");
                                        break;
                                    case 4:
                                        Manager.table_list[table - 1].TimeSlot_4_reserved.Add(date);
                                        Console.WriteLine("Timeslot 4: 13:00-14:00");
                                        break;
                                    case 5:
                                        Manager.table_list[table - 1].TimeSlot_5_reserved.Add(date);
                                        Console.WriteLine("Timeslot 5: 14:00-15:00");
                                        break;
                                    case 6:
                                        Manager.table_list[table - 1].TimeSlot_6_reserved.Add(date);
                                        Console.WriteLine("Timeslot 6: 15:00-16:00");
                                        break;
                                    case 7:
                                        Manager.table_list[table - 1].TimeSlot_7_reserved.Add(date);
                                        Console.WriteLine("Timeslot 7: 16:00-17:00");
                                        break;
                                    case 8:
                                        Manager.table_list[table - 1].TimeSlot_8_reserved.Add(date);
                                        Console.WriteLine("Timeslot 8: 17:00-18:00");
                                        break;
                                    case 9:
                                        Manager.table_list[table - 1].TimeSlot_9_reserved.Add(date);
                                        Console.WriteLine("Timeslot 9: 18:00-20:00");
                                        break;
                                    case 10:
                                        Console.WriteLine("Timeslot 10: 20:00-22:00");
                                        Manager.table_list[table - 1].TimeSlot_10_reserved.Add(date);
                                        break;
                                }

                                Console.WriteLine("Are you sure you want this guests, table, date and time (1) yes (2) no");
                                string? confirmationString1 = Console.ReadLine();
                                if (int.TryParse(confirmationString1, out int confirmation))
                                {
                                    if (confirmation == 1)
                                    {
                                        customer.Add_Reservation(table, guests, date, time!);
                                        ControllerJson.WriteJson(Manager.table_list, "Tables.json");
                                        go_back_to_date = false;
                                        end = true;
                                        break;
                                    }
                                    else if (confirmation == 2)
                                    {
                                        Console.WriteLine("Do you want to change (1) guests (2) table (3 or higher) date and time");
                                        string? confirmationString2 = Console.ReadLine();
                                        if (int.TryParse(confirmationString2, out int confirmation2))
                                        {
                                            if (confirmation2 == 1)
                                            {
                                                go_back_to_guests = true;
                                                continue;
                                            }
                                            else if (confirmation2 == 2)
                                            {
                                                go_back_to_table = true;
                                                continue;
                                            }
                                            else if (confirmation2 >= 3)
                                            {
                                                go_back_to_date = true;
                                                continue;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Please enter 1 or 2");
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Please enter 1 or 2");
                                    continue;
                                }
                            }
                        }
                        if (end)
                            break;
                        if (go_back_to_guests)
                            continue;
                    }
                    
                }

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
                if (input == "5")
                //gemaakt door sami
                {
                    Review.LeaveReview(customer.ID);
                }
                // logt uit
                if (input == "6")
                {
                    return;
                }
                // laat je het programma uit
                if (input == "7")
                {
                    Console.WriteLine("Goodbye and see you soon!");
                    System.Environment.Exit(0);
                }
            }
        }
    }
}
