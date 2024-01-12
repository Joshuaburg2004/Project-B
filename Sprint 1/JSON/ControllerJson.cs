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
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
            serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

            using (StreamWriter sw = new StreamWriter(FileName))
            using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
            {
                serializer.Serialize(writer, ObjectToJson, typeof(List<T>));
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
            List<T>? obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(FileName), new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            });
            return obj;
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
