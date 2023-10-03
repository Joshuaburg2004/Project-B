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
    public string Email { get; set; }
    public Account(string email, string name, string password)
    {
        id = Interlocked.Increment(ref nextID);
        Email = email;
        Name = name;
        Password = password;
    }
    public static Account CreateAccount(string email, string name, string password)
    {
        Account account = new Account(email, name, password);
        AccountManager.Accounts.Add(account);
        string json = JsonConvert.SerializeObject(AccountManager.Accounts, Formatting.Indented);
        JArray Object = JArray.Parse(json);
        ControllerJson.WriteJson(Object, "Accounts.json");
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
    public static string? ChangePassword(string email, string name, string password)
    {
        foreach (Account account in AccountManager.Accounts)
        {
            if(account.Email == email && account.Name == name && account.Password == password)
            {
                Console.Write("Please enter your new password: ");
                account.Password = Console.ReadLine() ?? password;
                string json = JsonConvert.SerializeObject(AccountManager.Accounts, Formatting.Indented);
                JArray Object = JArray.Parse(json);
                ControllerJson.WriteJson(Object, "Accounts.json");
                return account.Password;
            }
        }
        return null;
    }
}
