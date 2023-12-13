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
        Customer customer2 = Customer.CreateAccount("test", "unittest@hr.nl", "Unittest1");
        Assert.IsTrue(Manager.Customers.Contains(customer));
        Assert.IsTrue(Manager.Customers.Contains(customer1));
        Assert.IsTrue(Manager.Customers.Contains(customer2));
        Assert.AreEqual(customer, Customer.GetCustomerByID(customer.ID));
        Assert.AreEqual(customer1, Customer.GetCustomerByID(customer1.ID));
        Assert.AreEqual(customer2, Customer.GetCustomerByID(customer2.ID));
    }
    [TestMethod]
    public void TestLogIn()
    {
        Customer customer = Customer.CreateAccount("name", "email", "password");
        Customer? customer1 = Customer.Log_in("name", "email", "password");
        Assert.IsTrue(customer1!.Name == customer.Name);
        Assert.IsTrue(customer1!.Email == customer.Email);
        Assert.IsTrue(customer1!.Password == customer.Password);
    }
}
