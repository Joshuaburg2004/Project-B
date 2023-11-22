using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Quic;
using System.Security.Principal;
using System.Text;
public interface IAccount
{
    public int ID { get; }
    public string Name { get; }
    public string Password { get; }
    public string Email { get; }
    public string Role { get; }
}
