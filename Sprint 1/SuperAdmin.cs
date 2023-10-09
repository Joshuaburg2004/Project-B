using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Quic;
using System.Text;
public class SuperAdmin : Admin
{
    public SuperAdmin(string name = "Gert-Jan den heijer", string email = "gjdenheijer@gmail.com", string password = "123", string role = "SuperAdmin") : base(name, email, password) { }
    public Admin AddAdmin(string Name, string Email, string Password)
    {
        Admin admin = Admin.CreateAdmin(Name, Email, Password);
        return admin;
    }
}
