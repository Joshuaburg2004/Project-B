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
    
    public static List<Table> table_list = new() { 
        table_1,
        table_2,
        table_3,
        table_4,
        table_5,
        table_6,
        table_7,
        table_8,
        table_9,
        table_10,
        table_11,
        table_12,
        table_13,
        table_14,
        table_15,
    };

    public static List<Customer> Customers = new();
    public static List<Admin> Admins = new();
    public static SuperAdmin superAdmin = new();

    static Manager()
    {
        List<Table>? Tables = new();
        Customers = ControllerJson.ReadJson<Customer>("Customers.json") ?? new List<Customer> { };
        Admins = ControllerJson.ReadJson<Admin>("Admins.json") ?? new List<Admin> { };
        Tables = ControllerJson.ReadJson<Table>("Tables.json");
        if(Tables is null || Tables.Count != 15)
        {
            Tables = new List<Table>() { table_1, table_2, table_3, table_4, table_5, table_6, table_7, table_8, table_9, table_10, table_11, table_12, table_13, table_14, table_15 };        {
        }
        else
        {
            table_1 = (Table_6)Tables[0];
            table_2 = (Table_6)Tables[1];
            table_3 = (Table_4)Tables[2];
            table_4 = (Table_4)Tables[3];
            table_5 = (Table_4)Tables[4];
            table_6 = (Table_4)Tables[5];
            table_7 = (Table_4)Tables[6];
            table_8 = (Table_2)Tables[7];
            table_9 = (Table_2)Tables[8];
            table_10 = (Table_2)Tables[9];
            table_11 = (Table_2)Tables[10];
            table_12 = (Table_2)Tables[11];
            table_13 = (Table_2)Tables[12];
            table_14 = (Table_2)Tables[13];
            table_15 = (Table_2)Tables[14];
        }
    }
}
