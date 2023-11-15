namespace ProjectB.Tests;

[TestClass]
public class CustomerTest
{
    [TestMethod]
    public void TestCreation()
    {
        string name = "Joshua van der Burg";
        string email = "1051636@hr.nl";
        string password = "Secure123!";
        Customer customer = Customer.CreateAccount(name, email, password);
        Customer customer1 = new Customer(name, email, password);
        Assert.AreEqual(customer.Name, customer1.Name);
        Assert.AreEqual(customer.Email, customer1.Email);
        Assert.AreEqual(customer.Password, customer1.Password);
    }

    [TestMethod]
    public void TestID()
    {
        Customer customer = Customer.CreateAccount("a", "b", "c");
        Customer customer1 = Customer.CreateAccount("d", "e", "f");
        Customer customer2 = Customer.CreateAccount("g", "h", "i");
        Assert.IsTrue(AccountManager.Customers.Contains(customer));
        Assert.IsTrue(AccountManager.Customers.Contains(customer1));
        Assert.IsTrue(AccountManager.Customers.Contains(customer2));
        Assert.AreEqual(customer, Customer.GetCustomer(customer.Id));
        Assert.AreEqual(customer1, Customer.GetCustomer(customer1.Id));
        Assert.AreEqual(customer2, Customer.GetCustomer(customer2.Id));
    }

}