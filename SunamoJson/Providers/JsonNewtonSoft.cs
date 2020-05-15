using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class JsonNewtonSoft : IJsSerializer
{
    public static JsonNewtonSoft instance = new JsonNewtonSoft();

    private JsonNewtonSoft()
    {

    }

    public object Deserialize(string o, Type targetType)
    {
        return JsonConvert.DeserializeObject(o, targetType);
    }

    public string Serialize(object o)
    {
        return JsonConvert.SerializeObject(o);
    }
}