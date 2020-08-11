using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


public class JsonSystemTextJson //: IJsSerializer
{
    /// <summary>
    /// Instance must bu create due to anytime availability in RH.DumpAsString
    /// </summary>
    public static JsonSystemTextJson instance = new JsonSystemTextJson();

    private JsonSystemTextJson()
    {

    }

    public object Deserialize(string o, Type targetType)
    {
        return JsonSerializer.Deserialize(o, targetType);
    }

    public string Serialize(object o)
    {
        return JsonSerializer.Serialize(o, o.GetType());
    }
}