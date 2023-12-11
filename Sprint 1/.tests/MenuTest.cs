// Gemaakt door Alperen
namespace ProjectB.Tests
{
    [TestClass]
    public class MenuTest
    {
        [TestMethod]
        public void TestAddItem()
        {
            Menu_List.Menu_item = new();
            // Arrange
            string itemName = "TestItem";
            string category = "TestCategory";
            string price = "10.99";

            // Act
            bool added = Menu_List.Add_item(itemName, category, price);

            // Assert
            Assert.IsTrue(added);
            // Add more assertions if needed
        }

        [TestMethod]
        public void TestRemoveItem()
        {
            Menu_List.Menu_item = new();
            // Arrange
            string itemName = "TestItem";
            string category = "TestCategory";
            string price = "20.99";
            Menu_List.Add_item(itemName, category, price);

            // Act
            bool removed = Menu_List.Remove_item(itemName, category, price);

            // Assert
            Assert.IsTrue(removed);
            // Add more assertions if needed
        }

        // Add more test methods for other Menu related functionalities...
    }
}
