using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Quic;
using System.Text;
public class SuperAdmin : Account
{
    public SuperAdmin() : base("Gert-Jan den heijer", "gjdenheijer@gmail.com", "123", "SuperAdmin") { }
    public Admin AddAdmin(string Name, string Email, string Password)
    {
        Admin admin = Admin.CreateAdmin(Name, Email, Password);
        return admin;
    }
}
