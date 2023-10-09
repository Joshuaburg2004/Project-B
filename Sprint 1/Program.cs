using System.Runtime.CompilerServices;

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

        var Current_user = 0;
/*--------------------------------------------------------------------------------------------------------------------------------*/
        Console.ForegroundColor = ConsoleColor.Green;
        RestaurantLayout.ViewLayout();

        while (true)
        {
            Logo();
            Console.WriteLine("""
            press 1 to log in or to create an Account
            press 2 to look at the menu
            press 3 to read restaurant information
            press 4 to close app
            """);
            int option = Convert.ToInt32(Console.ReadLine());
            if (option == 1)
            {

            }
            else if (option == 2)
            {

            } 
            else if (option == 3)
            {

            }
            else if (option == 4)
            {
                Environment.Exit(0);
            }
        }
 
    }
    public static void Logo()
    {
        Console.WriteLine("""










            """);
    }

}
