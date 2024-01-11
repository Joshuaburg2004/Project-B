using System.Configuration.Assemblies;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

    public static void LeaveReview(int customerID)
    {
        Console.WriteLine("Enter your review text (type 'Q' to exit):");
        string reviewText = Console.ReadLine()!;

        if (reviewText.ToUpper() == "Q")
        {
            Console.WriteLine("Review submission canceled.");
            return;
        }

        Console.WriteLine("Enter the number of stars (1-5):");
        string starsInput = Console.ReadLine()!;

        if (starsInput.ToUpper() == "Q")
        {
            Console.WriteLine("Review submission canceled.");
            return;
        }

        if (int.TryParse(starsInput, out int stars) && stars >= 1 && stars <= 5)
        {
            DateTime now = DateTime.Now;

            Review review = new Review(AllReviews.Count + 1, customerID, reviewText, stars, now);
            AllReviews.Add(review);

            string jsonReviews = JsonConvert.SerializeObject(AllReviews, Formatting.Indented);
            JArray reviewsObject = JArray.Parse(jsonReviews);
            ControllerJson.WriteJson(reviewsObject, "Reviews.json");

            Console.WriteLine("Review submitted successfully.");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Invalid input for stars. Review not submitted.");
        }
    }

    public static void LeaveReview(int customerID,string reviewText,int stars)
    {
        if (stars >= 1 && stars <= 5 )
        {
            DateTime now = DateTime.Now;
            Review review = new Review(AllReviews.Count + 1,customerID,reviewText,stars, now);

            string JsonReviews = JsonConvert.SerializeObject(AllReviews, Formatting.Indented);
            JArray reviewsObject = JArray.Parse(JsonReviews);
            ControllerJson.WriteJson(reviewsObject,"Reviews.json");
            
            Console.WriteLine("Review submitted succesfully. ");
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Invalid input for stars. Review not submitted.");
        }
    }
}
