using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Quic;
using System.Text;
public class ControllerJson
{
    // Gebruik de volgende code (waarbij ObjectToJson je list is) om het naar de Json te sturen:
    // string json = JsonConvert.SerializeObject(ObjectToJson, Formatting.Indented);
    // JArray Object = JArray.Parse(json);
    public static bool WriteJson(JArray Object, string FileName)
    {
        try
        {
            using (StreamWriter file = File.CreateText(@FileName))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                Object.WriteTo(writer);
            }
            return true;
        }
        catch (FileNotFoundException ex)
        {
            Console.Write("Missing JSON file. ");
            Console.WriteLine(ex.Message);
            return false;
        }
        catch (JsonReaderException ex)
        {
            Console.Write("Invalid JSON. ");
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    // gebruik volgende 2 lijnen om te benutten (verander Type naar het type wat je nodig hebt en verander ListName naar wat je wil):
    // string FileCont = ReadJson("file.json");
    // List<Type> ListName = JsonConvert.DeserializeObject<List<Type>>(FileCont);
    public string? ReadJson(string FileName)
    {
        try
        {
            string FileContent = File.ReadAllText(@FileName);
            return FileContent;
        }
        catch (FileNotFoundException ex)
        {
            Console.Write("Missing JSON file. ");
            Console.WriteLine(ex.Message);
            return null;
        }
        catch (JsonReaderException ex)
        {
            Console.Write("Invalid JSON. ");
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}
