using System;

public class RestaurantInfo
{
    public string Adres { get;  set; } = "Wijnhaven 107";
    public string Bereikbaarheid { get;  set; } = "Beurs, metrolijnen: A B C D E";
    public int Telefoonnummer { get;  set; } = 064787943;
    public string Email { get;  set; } = "Gert_jan@hr.nl";
    public string Geschiedenis { get;  set; } = "Gert Jan was voorheen een docent, maar koos voor een nieuwe carrière als ondernemer. Hij houdt van lekker eten en heeft daarom dit restaurant geopend om zijn passie te delen.";
    // print de info over het restaurant
    public void Info_Restaurant()
    {
        Console.WriteLine($"Adres: {Adres}");
        Console.WriteLine($"Bereikbaarheid: {Bereikbaarheid}");
        Console.WriteLine($"Telefoonnummer: {Telefoonnummer}");
        Console.WriteLine($"E-mail: {Email}");
        Console.WriteLine($"Geschiedenis: {Geschiedenis}");
        Console.WriteLine();
        double average_stars = Review.Get_Total_Stars();
        Console.WriteLine($"Restaurant stars: {average_stars}");
    }
}
