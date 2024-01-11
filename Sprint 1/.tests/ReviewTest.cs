namespace ProjectB.Tests;

[TestClass]
public class ReviewTests

  {
      [TestMethod]
      public void TestLeaveReview()
      {
          //kijken of correcte data wordt opgeslagen
          int customerID = 1;
          string reviewText = "nice restaurant";
          int stars = 5;

          Review.AllReviews.Clear();
          //om accurate ding te hebben
          Review.LeaveReview(customerID);

          Assert.AreEqual(1, Review.AllReviews.Count);
          Assert.AreEqual(customerID, Review.AllReviews[0].CustomerID);
          Assert.AreEqual(reviewText, Review.AllReviews[0].Text);
          Assert.AreEqual(stars, Review.AllReviews[0].Stars);
      }

      [TestMethod]
      public void TestGetTotalStarsEmptyReviews()
      {
          //even kijken of de list empty is zodat het wel werkt
          Review.AllReviews.Clear();
          double totalStars = Review.Get_Total_Stars();
          Assert.AreEqual(0, totalStars);
      }

      [TestMethod]
      public void TestLeaveReviewInvalidStars()
      {
          //kijken of je niet meer dan 5 sterren kan geven
          Review.AllReviews.Clear();
          Review.LeaveReview(1, "test review bla bla", 6);
          Assert.AreEqual(0, Review.AllReviews.Count);
      }
  }

  

