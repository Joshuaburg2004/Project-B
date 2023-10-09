using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Quic;
using System.Text;
public class Admin : Account
{
    
    private Admin(string name, string email, string password, string role = "Admin") : base(name, email, password, role) { }
    public static Admin CreateAdmin(string name, string email, string password)
    {
        Admin admin = new Admin(name, email, password);
        AccountManager.Admins.Add(admin);
        string json = JsonConvert.SerializeObject(AccountManager.Admins, Formatting.Indented);
        JArray Object = JArray.Parse(json);
        ControllerJson.WriteJson(Object, "Admins.json");
        return admin;
    }   
}
