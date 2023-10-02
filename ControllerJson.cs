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
    // gebruik List<TypeInList> ListName = JsonConvert.DeserializeObject<List<TypeInList>>(outputVanReadJson); voor de list in C# speak
    public JArray string ReadJson(string FileName)
    {
        try
        {
            JArray o1 = JArray.Parse(File.ReadAllText(@FileName));
            return o1;
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