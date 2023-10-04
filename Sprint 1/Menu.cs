// De klant de gerechten per onderdeel in kan zien.
// De Super-admin kan aanpassingen brengen aan de gerechten.

// category (fish/meat/vegan/vegetarian) 

public class Menu
{
    public static List<Menu> Menu_item = new();
    public double Price;
    public string Category;
    public string Name;
    public Menu(string name, string category, double price)
    {
        this.Price = price;
        this.Category = category;
        this.Name = name;
    }
    public static List<Menu> view_menu()
    {
        Menu item1 = new Menu("Lahmacun","Meat",6.99);
        Menu item2 = new Menu("Pizza pepperoni","Meat",12.5);
        Menu_item.Add(item1);
        Menu_item.Add(item2);
        return Menu_item;
    }

    public static void view()
    {
        foreach(Menu item in Menu_item)
        {
            Console.WriteLine($"Name: {item.Name} Category: {item.Category} Price: {item.Price}");
        }
    } 
}
