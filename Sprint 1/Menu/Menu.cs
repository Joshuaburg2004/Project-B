// De klant de gerechten per onderdeel in kan zien.
// De Super-admin kan aanpassingen brengen aan de gerechten.

// category (fish/meat/vegan/vegetarian) 
//Gemaakt door Alperen en Berkan
using System.Text.Unicode;
public class Menu
{
    /*public static List<Menu> Menu_item = new() { new Menu("Lahmacun", "Meat", 6.99), new Menu("Pizza pepperoni", "Meat", 12.5) };*/
    public double Price;
    public string Category;
    public string Name;
    // items binnen het menu. wordt niet gebruikt, gebruik Menu_List.Add_item()
    public Menu(string name, string category, double price)
    {
        this.Price = price;
        this.Category = category;
        this.Name = name;

    }
    /*public static List<Menu> view_menu()
    {
        Menu item1 = new Menu("Lahmacun", "Meat", 6.99);
        Menu item2 = new Menu("Pizza pepperoni", "Fish", 12.5);
        Menu_item.Add(item1);
        Menu_item.Add(item2);
        return Menu_item;
    }*/
    // Gemaakt door Alperen, verbeterd door Aymane

    public static void view()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("(1) Everything\n(2) Choose by Category");
        string? ans = Console.ReadLine();
        if (ans is not null)
        {
            if (ans == "1")
            {
                IEnumerable<Menu[]> menu = Menu_List.Menu_item.Chunk(14);
                int Index = 0;
                while (true)
                {
                    foreach (Menu item in menu.ElementAt(Index))
                    {
                        Console.WriteLine($"|Name: {item.Name}|Category: {item.Category}|Price: \u20AC{item.Price},-|");
                    }
                    Console.WriteLine("Enter a page, or (Q) to exit");
                    string Page = Console.ReadLine()!.ToUpper();
                    bool Convert = int.TryParse(Page, out int page);
                    if (Page == "Q") { break; }
                    if (Convert)
                    {
                        if (page > menu.Count()) { Console.WriteLine("This page does not exist"); }
                        else { Index = page - 1; }
                    }
                }
            }
            else if (ans == "2")
            {
                Console.WriteLine("(Fish/Meat/Vegan/Vegetarian)");
                string? ans1 = Console.ReadLine()!.ToUpper();
                IEnumerable<Menu[]> menu = Menu_List.Menu_item.Where(x => x.Category.ToUpper() == ans1).ToList().Chunk(14);
                int Index = 0;
                while (true)
                {
                    foreach (Menu item in menu.ElementAt(Index))
                    {
                        Console.WriteLine($"|Name: {item.Name}|Category: {item.Category}|Price: \u20AC{item.Price},-|");
                    }
                    Console.WriteLine("Enter a page, or (Q) to exit");
                    string Page = Console.ReadLine()!.ToUpper();
                    bool Convert = int.TryParse(Page, out int page);
                    if (Page == "Q") { break; }
                    if (Convert)
                    {
                        if (page > menu.Count()) { Console.WriteLine("This page does not exist"); }
                        else { Index = page - 1; }
                    }
                }
            }
        }
    }
}
