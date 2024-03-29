using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Quic;
using System.Text;
public static class Menu_List
{
    // wordt gebruikt om alle menu items bij te houden
    public static List<Menu> Menu_item = new List<Menu> { };
    static Menu_List()
    {
        // leest het menu uit de json
        Menu_item = ControllerJson.ReadJson<Menu>("Menu.json") ?? new List<Menu> { };
    }
    // Print alle items.
    public static void view()
    {
        foreach(Menu item in Menu_item)
        {
            Console.WriteLine($"Name: {item.Name} | Category: {item.Category} | Price: {item.Price}");
        }
    }
    // Voegt een item toe aan de list, returned of het goed ging
    public static bool Add_item(string? name1, string? category1, string? price1)
    {
        foreach (Menu menu in Menu_item)
        {
            if (menu.Name == name1)
            {
                return false;
            }
        }
        if(name1 is not null && category1 is not null && price1 is not null)
        {
            Menu_item.Add(new Menu(name1, category1, Convert.ToDouble(price1)));
            ControllerJson.WriteJson(Menu_item, "Menu.json");
            return true;
        }
        return false;
    }
    // haalt een item uit de list, returned of het goed ging
    public static bool Remove_item(string? name, string? category, string? price)
    {
        if (name is not null && category is not null && price is not null)
        {
            double price1 = Convert.ToDouble(price);
            foreach (Menu menu in Menu_item)
            {
                if(menu.Name == name && menu.Category == category && menu.Price == price1)
                {
                    Menu_item.Remove(menu);
                    ControllerJson.WriteJson(Menu_item, "Menu.json");
                    return true;
                }
            }
        }
        return false;
    }
}
