using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Quic;
using System.Text;
public class Admin : Account
{
    
    public Admin(string name, string email, string password, string role = "Admin") : base(name, email, password, role) { }
    public static Admin CreateAdmin(string name, string email, string password)
    {
        Admin admin = new Admin(name, email, password);
        AccountManager.Admins.Add(admin);
        string json = JsonConvert.SerializeObject(AccountManager.Admins, Formatting.Indented);
        JArray Object = JArray.Parse(json);
        ControllerJson.WriteJson(Object, "Admins.json");
        return admin;
    }
    public static Admin? GetAdmin(int id)
    {
        foreach (Admin admin in AccountManager.Admins)
        {
            if (admin.Id == id)
            {
                return admin;
            }
        }
        return null;
    }
    public static string? ChangePassword(string name, string password)
    {
        foreach (Admin admin in AccountManager.Admins)
        {
            if (admin.Name == name && admin.Password == password)
            {
                Console.Write("Please enter your new password: ");
                admin.Password = Console.ReadLine() ?? password;
                string json = JsonConvert.SerializeObject(AccountManager.Admins, Formatting.Indented);
                JArray Object = JArray.Parse(json);
                ControllerJson.WriteJson(Object, "Admins.json");
                return admin.Password;
            }
        }
        return null;
    }
    public static Admin? Log_in(string name, string email, string password)
    {
        foreach (Admin admin in AccountManager.Admins)
        {
            if (admin.Name == name && admin.Password == password && admin.Email == email)
            {
                return admin;
            }
        }
        return null;
    }
}
