using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Quic;
using System.Text;
public static class Menu_List
{
    public static List<Menu> Menu_item = new List<Menu> { };
    static Menu_List()
    {
        string? FileCont = ControllerJson.ReadJson("file.json");
        if (FileCont is not null)
        { 
            Menu_item = JsonConvert.DeserializeObject<List<Menu>>(FileCont) ?? new List<Menu> { }; 
        }
        else
        {
            Menu_item = new List<Menu> { };
        }
    }
    public static void view()
    {
        foreach(Menu item in Menu_item)
        {
            Console.WriteLine($"Name: {item.Name} | Category: {item.Category} | Price: {item.Price}");
        }
    }

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
            string json = JsonConvert.SerializeObject(Menu_item, Formatting.Indented);
            JArray Object = JArray.Parse(json);
            ControllerJson.WriteJson(Object, "Menu.json");
            return true;
        }
        return false;
    }

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
                    string json = JsonConvert.SerializeObject(Menu_item, Formatting.Indented);
                    JArray Object = JArray.Parse(json);
                    ControllerJson.WriteJson(Object, "Menu.json");
                    return true;
                }
            }
        }
        return false;
    }
}
