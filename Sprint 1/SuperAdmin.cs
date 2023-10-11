using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Quic;
using System.Text;
public class SuperAdmin
{
    public static string Name = "Gert-Jan den heijer";
    public static string Email = "gjdenheijer@gmail.com";
    public static string Password = "123";
    public Admin AddAdmin(string Name, string Email, string Password)
    {
        Admin admin = Admin.CreateAdmin(Name, Email, Password);
        return admin;
    }
}
