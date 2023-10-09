using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Quic;
using System.Security.Principal;
using System.Text;
public class AccountManager
{
    public int Id { get; set; }
    public static List<Customer> Customers = new();
    public static List<Admin> Admins = new();
    public AccountManager()
    {
        string? FileCont1 = ControllerJson.ReadJson("Customers.json");
        if (FileCont1 != null)
        {
            Customers = JsonConvert.DeserializeObject<List<Customer>>(FileCont1) ?? new List<Customer> { };
        }
        string? FileCont2 = ControllerJson.ReadJson("Admins.json");
        if (FileCont2 != null)
        {
            Admins = JsonConvert.DeserializeObject<List<Admin>>(FileCont2) ?? new List<Admin> { };
        }
    }
}
