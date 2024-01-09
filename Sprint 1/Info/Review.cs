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
}
