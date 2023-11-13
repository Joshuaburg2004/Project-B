using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Quic;
using System.Security.Principal;
using System.Text;
// abstract class, kan niet worden geïnitialiseerd. Wordt gebruikt voor Customer, Admin en SuperAdmin
public abstract class Account
{
    static int nextID;
    public int Id { get; }
    public string Name;
    public string Password;
    public string Email;
    public string Role;

    protected Account(string name, string email, string password, string role)
    {
        Id = Interlocked.Increment(ref nextID);
        Name = name;
        Password = password;
        Email = email;
        Role = role;
    }
}
