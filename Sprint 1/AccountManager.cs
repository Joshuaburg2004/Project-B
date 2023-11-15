using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Quic;
using System.Security.Principal;
using System.Text;
// houd alle accounts bij.
public static class AccountManager
{
    public static List<Customer> Customers = new();
    public static List<Admin> Admins = new();
    // houdt de superadmin bij
    public static SuperAdmin superAdmin = new();
    static AccountManager()
    {
        // Vult Customers uit de json
        string? FileCont1 = ControllerJson.ReadJson("Customers.json");
        if (FileCont1 != null)
        {
            Customers = JsonConvert.DeserializeObject<List<Customer>>(FileCont1) ?? new List<Customer> { };
        }
        // Vult Admins uit de json
        string? FileCont2 = ControllerJson.ReadJson("Admins.json");
        if (FileCont2 != null)
        {
            Admins = JsonConvert.DeserializeObject<List<Admin>>(FileCont2) ?? new List<Admin> { };
        }
        // Maakt de ID voor de Accounts groter gebaseerd op de lengte van customers en admins. voorkomt overlap in de json
        Account.nextID += Customers.Count + Admins.Count;
    }
}
