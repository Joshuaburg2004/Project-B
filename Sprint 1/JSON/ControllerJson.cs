using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Quic;
using System.Text;
public class ControllerJson
{
    // Schrijft een object naar de json
    public static bool WriteJson<T>(T ObjectToJson, string FileName)
    {
        try
        {
            string json = JsonConvert.SerializeObject(ObjectToJson);
            JArray Object = JArray.Parse(json);
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
    // Leest een json uit
    public static List<T>? ReadJson<T>(string FileName)
    {
        try
        {
            string FileContent = File.ReadAllText(@FileName);
            List<T>? output = JsonConvert.DeserializeObject<List<T>>(FileContent);
            return output;
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
