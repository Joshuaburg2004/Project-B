namespace ProjectB.Tests;

[TestClass]
public class AdminTests
{

    [TestMethod]
    //sami
    public void TestAdminCreation()
    {
        
        string name = "Jan";
        string email = "Jan@gmail.com";
        string password = "Jan123!";
        
        Manager.Admins.Clear();
        Admin.CreateAdmin(name,email,password);

        Assert.AreEqual(1,Manager.Admins.Count);
        Assert.AreEqual(name,Manager.Admins[0].Name);
        Assert.AreEqual(email,Manager.Admins[0].Email);
        Assert.AreEqual(password,Manager.Admins[0].Password);


    }
    
}
