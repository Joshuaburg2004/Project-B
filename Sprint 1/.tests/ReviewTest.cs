namespace ProjectB.Tests;
//gemaakt door sami

[TestClass]
public class ReviewTests

    {
        [TestMethod]
        public void TestLeaveReview()
        {
            int customerID = 1;
            string reviewText = "nice restaurant";
            int stars = 5;

            Review.AllReviews.Clear();
            Review.LeaveReview(customerID,"nice restaurant",5);

            Assert.AreEqual(1,Review.AllReviews.Count);
            //check of het is toegevoegd
            Assert.AreEqual(customerID,Review.AllReviews[0].CustomerID);
            //id check
            Assert.AreEqual(reviewText,Review.AllReviews[0].Text);
            //review text check
            Assert.AreEqual(stars,Review.AllReviews[0].Stars);
            //star checks
        }

        [TestMethod]
        public void TestGetTotalStarsEmptyReviews()
        //bekijken of het ook echt leeg is
        {
            Review.AllReviews.Clear();
            double totalStars = Review.Get_Total_Stars();
            Assert.AreEqual(0, totalStars);
        }

        [TestMethod]
        public void TestLeaveReviewInvalidStars()
        //ster check fout
        {
            Review.AllReviews.Clear();
            Review.LeaveReview(1, "test review bla bla", 6);
            Assert.AreEqual(0, Review.AllReviews.Count);
        }
    }

    

