using System;
using System.Collections.Generic;
using System.Text;

public class JsonConvert
{
    JavascriptSerialization js = new JavascriptSerialization(SerializationLibrary.Utf8Json);

    internal static T DeserializeObject<T>(string v)
    {
        return JsonConvert.DeserializeObject<T>(v);
    }
}