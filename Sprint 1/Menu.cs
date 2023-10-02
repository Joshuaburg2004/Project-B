// De klant de gerechten per onderdeel in kan zien.
// De Super-admin kan aanpassingen brengen aan de gerechten.

// category (fish/meat/vegan/vegetarian) 

public class menu
{
    public static List<menu> Menu_item = new();
    public double Price;
    public string Category;
    public string Name;
    public menu(string name, string category, double price)
    {
        this.Price = price;
        this.Category = category;
        this.Name = name;
    }
    public static List<menu> view_menu()
    {
        menu item1 = new menu("Lahmacun","Meat",6.99);
        menu item2 = new menu("Pizza pepperoni","Meat",12.5);
        Menu_item.Add(item1);
        Menu_item.Add(item2);
        return Menu_item;
    }

    public static void view()
    {
        foreach(menu item in Menu_item)
        {
            Console.WriteLine($"Name: {item.Name} Category: {item.Category} Price: {item.Price}");
        }
    } 
}
public class Program{
    public static void Main()
    {
        menu.view_menu();
        menu.view();
    }
}
