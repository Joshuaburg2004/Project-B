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
    public static void view()
    {
        foreach(Menu item in Menu_item)
        {
            Console.WriteLine($"Name: {item.Name} Category: {item.Category} Price: {item.Price}");
        }
    } 
}
