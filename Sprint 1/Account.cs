using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Quic;
using System.Text;
public class Account
{
    static int nextID;
    public int id { get; private set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public Account(string name, string password)
    {
        id = Interlocked.Increment(ref nextID);
        Name = name;
        Password = password;
    }
    public static Account CreateAccount(string name, string password)
    {
        Account account = new Account(name, password);
        AccountManager.Accounts.Add(account);
        return account;
    }
    public static Account? GetAccount(int id)
    {
        foreach(Account account in AccountManager.Accounts)
        {
            if (account.id == id)
            {
                return account;
            }
        }
        return null;
    }
    public static string? ChangePassword(string name, string password)
    {
        foreach (Account account in AccountManager.Accounts)
        {
            if(account.Name == name && account.Password == password)
            {
                Console.Write("Please enter your new password: ");
                account.Password = Console.ReadLine() ?? password;
                return account.Password;
            }
        }
        return null;
    }
}
