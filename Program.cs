public class Program
{
    public static void Main()
    {
        Customer p1 = new Customer("new", "e@mail.com","test");

        Account.CreateAccount(p1.Name, p1.Email, p1.Password);


    }
}