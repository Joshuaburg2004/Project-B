using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net.Quic;
using System.Security.Principal;
using System.Text;
public static class Manager
{
    public static Table_6 table_1 = new Table_6();
    public static Table_6 table_2 = new Table_6();

    public static Table_4 table_3 = new Table_4();
    public static Table_4 table_4 = new Table_4();
    public static Table_4 table_5 = new Table_4();
    public static Table_4 table_6 = new Table_4();
    public static Table_4 table_7 = new Table_4();

    public static Table_2 table_8 = new Table_2();
    public static Table_2 table_9 = new Table_2();
    public static Table_2 table_10 = new Table_2();
    public static Table_2 table_11 = new Table_2();

    public static Table_2 table_12 = new Table_2();
    public static Table_2 table_13 = new Table_2();
    public static Table_2 table_14 = new Table_2();
    public static Table_2 table_15 = new Table_2();

    public static List<Customer> Customers = new();
    public static List<Admin> Admins = new();
    public static SuperAdmin superAdmin = new();
    static Manager()
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
