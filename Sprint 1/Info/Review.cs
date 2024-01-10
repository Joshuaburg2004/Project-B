using System.Configuration.Assemblies;
using Newtonsoft.Json;
public class Review
{
    //gemaakt door sami
    public int ReviewID { get; }
    public int CustomerID { get; }
    public string Text { get; }
    public int Stars { get; }
    public DateTime Date { get; }

    public Review(int reviewID, int customerID, string text, int stars, DateTime date)
    {
        ReviewID = reviewID;
        CustomerID = customerID;
        Text = text;
        Stars = stars;
        Date = date;
    }

    public static List<Review> AllReviews { get; }
    static Review()
    {
        AllReviews = ControllerJson.ReadJson<Review>("Reviews.json") ?? new List<Review> { };
    }

    public static double Get_Total_Stars()
    {
        int totalreviews = Review.AllReviews.Count();
        double stars_temp = 0;

        foreach (var review in AllReviews)
        {
            stars_temp +=review.Stars;

        }
        double stars = stars_temp / totalreviews;
        double stars_rounded = Math.Round(stars,1);
        return stars_rounded;
    }
}
