using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Quic;
using System.Security.Principal;
using System.Text;
public class Account
{
    static int nextID;
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email;
    public string Role;

    public Account(string name, string email, string password, string role = "Customer")
    {
        Id = Interlocked.Increment(ref nextID);
        Name = name;
        Password = password;
        Email = email;
        Role = role;
    }
    public static Account CreateAccount(string name, string email, string password)
    {

        Account account = new Account(name, email, password);
        AccountManager.Accounts.Add(account);
        string json = JsonConvert.SerializeObject(AccountManager.Accounts, Formatting.Indented);
        JArray Object = JArray.Parse(json);
        ControllerJson.WriteJson(Object, "Accounts.json");
        return account;
    }
    public static Account? GetAccount(int id)
    {
        foreach (Account account in AccountManager.Accounts)
        {
            if (account.Id == id)
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
            if (account.Name == name && account.Password == password)
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
