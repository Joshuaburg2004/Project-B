public class Menu_List
{
    public List<Menu> Menu_item = new();
    public static void view()
    {
        foreach(Menu item in Menu_List.Menu_item)
        {
            Console.WriteLine($"Name: {item.Name} | Category: {item.Category} | Price: {item.Price}");
        }
    }
    public static bool add_item()
    {
        Console.Write("Would you like to add an item to the Menu Y/N ?: ");
        string choice = Console.ReadLine();

        if(choice.ToUpper() == "Y")
        {
            Console.WriteLine("What is the Name of the item ?: ");
            string name1 = Console.ReadLine();
            Console.WriteLine("What is the Category (fish/meat/vegan/vegetarian) ?:");
            string category1 = Console.ReadLine();
            Console.WriteLine("What will the price be ?:");
            double price1 = Convert.ToDouble(Console.ReadLine());
            foreach(Menu menu in Menu_List.Menu_item){
                if(menu.Name == name1){
                    return false;
                }
            }
            Menu_List.Menu_item.Add(new Menu(name1, category1, price1));
            return true;
        }
        return false;
    }
}