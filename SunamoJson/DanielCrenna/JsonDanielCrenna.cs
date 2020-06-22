using System;
using System.Collections.Generic;
using System.Text;
using Json;

public class JsonDanielCrenna : IJsSerializer
{
    public static JsonDanielCrenna instance = new JsonDanielCrenna();

    private JsonDanielCrenna()
    {

    }

    public object Deserialize(string o, Type targetType)
    {
        return JsonParser.Deserialize(o, targetType);
    }

    public string Serialize(object o)
    {
        return JsonParser.Serialize(o);
    }
}