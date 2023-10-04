using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Quic;
using System.Text;
public class AccountManager
{
    public int Id {  get; set; }
    public static List<IAccount> Accounts = new();
    public static List<Customer> Customers = new();
    public AccountManager()
    {
        string? FileCont = ControllerJson.ReadJson("Accounts.json");
        if (FileCont != null)
        {
            Accounts = JsonConvert.DeserializeObject<List<IAccount>>(FileCont) ?? new List<IAccount> { };
        }
    }
}
