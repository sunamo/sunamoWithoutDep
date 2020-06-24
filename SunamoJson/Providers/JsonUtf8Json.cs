using System;
using System.Collections.Generic;
using System.Text;
using Utf8Json;

public class JsonUtf8Json : IJsSerializer
{
    public static JsonUtf8Json instance = new JsonUtf8Json();

    private JsonUtf8Json()
    {

    }

    public object Deserialize(string o, Type targetType)
    {
        return JsonSerializer.NonGeneric.Deserialize(targetType, o);
    }

    public string Serialize<T>(T o)
    {
        return JsonSerializer.ToJsonString<T>(o);
    }
}