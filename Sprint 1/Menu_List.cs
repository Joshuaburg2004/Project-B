public class Menu_List
{
    public List<Menu> Menu_item = new();
    public void view()
    {
        foreach(Menu item in Menu_item)
        {
            Console.WriteLine($"Name: {item.Name} | Category: {item.Category} | Price: {item.Price}");
        }
    }

    public bool Add_item()
    {
        Console.WriteLine("What is the Name of the item ?: ");
        string? name1 = Console.ReadLine();
        Console.WriteLine("What is the Category (fish/meat/vegan/vegetarian) ?:");
        string? category1 = Console.ReadLine();
        Console.WriteLine("What will the price be ?:");
        string? price1 = Console.ReadLine();
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
            return true;
        }
        return false;
    }

    public bool Remove_item()
    {
        //TODO
        return false;
    }
}
