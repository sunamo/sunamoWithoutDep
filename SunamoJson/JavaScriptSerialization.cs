using Newtonsoft.Json;
using SunamoExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;

public class JavascriptSerialization : IJsSerializer
{
    static JsonNewtonSoft newtonSoft = null;
        /// <summary>
        /// Properties which could creating instance is useless
        /// single needed instance is creating in ctor
        /// </summary>
    static JsonSystemTextJson systemTextJson = null;
    static Type type = typeof(JavascriptSerialization);
    private SerializationLibrary sl = SerializationLibrary.Newtonsoft;

    /// <summary>
    /// Výchozí pro A1 je Microsoft
    /// </summary>
    /// <param name="sl"></param>
    public JavascriptSerialization(SerializationLibrary sl)
    {
        this.sl = sl;
        switch (sl)
        {
            case SerializationLibrary.Microsoft:
                systemTextJson = JsonSystemTextJson.instance;
                //ThrowExceptionsMicrosoftSerializerNotSupported<object>();
                break;
            case SerializationLibrary.Newtonsoft:
                newtonSoft = JsonNewtonSoft.instance;
                break;
            case SerializationLibrary.SystemTextJson:
                systemTextJson = JsonSystemTextJson.instance;
                break;
            default:
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), sl);
                break;
        }
    }

    public string Serialize(object o)
    {
        if (sl == SerializationLibrary.Microsoft)
        {
            return systemTextJson.Serialize(o);
            return ThrowExceptionsMicrosoftSerializerNotSupported<string>();
            //return js.Serialize(o);
        }
        else if (sl == SerializationLibrary.Newtonsoft)
        {
            return newtonSoft.Serialize(o);
        }
        else if (sl == SerializationLibrary.SystemTextJson)
        {
            return systemTextJson.Serialize(o);
        }
        else
        {
            return NotSupportedElseIfClasule<string>("Serialize");
        }
    }

    private T ThrowExceptionsMicrosoftSerializerNotSupported<T>()
    {
        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "System.Web.Scripting.Serialization.JavaScriptSerializer is not supported in Windows Store Apps" + ".");
        return default(T);
    }

    private T NotSupportedElseIfClasule<T>(string v)
    {
        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "Else if with enum value" + " " + sl + " " + "in JavascriptSerialization" + "." + v);
        return default(T);
    }

    public object Deserialize(String o, Type targetType)
    {
        if (sl == SerializationLibrary.Microsoft)
        {
            //JsonConvert.DeserializeObject()
            //JsonValue.
            //var serializer = new DataContractJsonSerializer(typeof(T));
            //serializer.P
            //T library = (T)serializer.ReadObject(o);
            //return T;
            //return js.Deserialize<T>(o);

            return systemTextJson.Deserialize(o, targetType);
        }
        else if (sl == SerializationLibrary.Newtonsoft)
        {
            return newtonSoft.Deserialize(o, targetType);
        }
        else if (sl == SerializationLibrary.SystemTextJson)
        {
            return systemTextJson.Deserialize(o, targetType);
        }
        return NotSupportedElseIfClasule<object>("Serialize(String,Type)");
    }

    public T Deserialize<T>(String o)
    {
        return (T)Deserialize(o, typeof(T));
    }
}