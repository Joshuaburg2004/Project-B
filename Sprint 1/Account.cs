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

    public Account(string name, string email, string password, string role)
    {
        Id = Interlocked.Increment(ref nextID);
        Name = name;
        Password = password;
        Email = email;
        Role = role;
    }
}
