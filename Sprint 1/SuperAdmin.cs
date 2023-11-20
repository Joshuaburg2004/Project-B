using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Quic;
using System.Text;
public class SuperAdmin : IAccount
{
    public int Id { get; } = 1;
    public string Name { get; } = "Gert-Jan den heijer";
    public string Password { get; } = "123";
    public string Email { get; } = "gjdenheijer@gmail.com";
    public string Role { get; } = "SuperAdmin";
    public SuperAdmin(){ }
    public Admin AddAdmin(string Name, string Email, string Password)
    {
        Admin admin = Admin.CreateAdmin(Name, Email, Password);
        return admin;
    }
}
