using SunamoExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;

public class JavascriptSerialization : IJsSerializer
{
    static IJsSerializer newtonSoft = null;
    /// <summary>
        /// Properties which could creating instance is useless
        /// single needed instance is creating in ctor
        /// </summary>
    static IJsSerializer systemTextJson = null;
    static IJsSerializer microsoft = null;
    static IJsSerializer danielCrenna = null;
    public static IJsSerializer utf8json = null;
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
                utf8json = JsonUtf8Json.instance;
                //ThrowExceptionsMicrosoftSerializerNotSupported<object>();
                break;
            case SerializationLibrary.Newtonsoft:
                utf8json = JsonUtf8Json.instance;
                break;
            case SerializationLibrary.SystemTextJson:
                utf8json = JsonUtf8Json.instance;
                break;
            case SerializationLibrary.JsonDanielCrenna:
                utf8json = JsonUtf8Json.instance;
                break;
            case SerializationLibrary.Utf8Json:
                utf8json = JsonUtf8Json.instance;
                break;
            default:
                ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), sl);
                break;
        }
    }

    public string Serialize<T>(T o)
    {
        var v = string.Empty;
        int i = 0;
        while (true)
        {
            if (i == 50)
            {
                return "error:More than 50 repetities in JavaScriptSerialization.Serialize";
            }

            try
            {
                v = utf8json.Serialize(o);
                
            }
            catch (Exception)
            {
                i++;
                continue;
            }
            break;
        }
        return v;
        
        //if (sl == SerializationLibrary.JsonDanielCrenna)
        //{
            // must be called as static, otherwise during serialize return empty string
            //return JsonParser.Serialize<T>(o);
        //}
        //else if (sl == SerializationLibrary.Microsoft)
        //{
        //    return systemTextJson.Serialize(o);
        //    return ThrowExceptionsMicrosoftSerializerNotSupported<string>();
        //    //return js.Serialize(o);
        //}
        //else if (sl == SerializationLibrary.Newtonsoft)
        //{
        //    return newtonSoft.Serialize(o);
        //}
        //else if (sl == SerializationLibrary.SystemTextJson)
        //{
        //    return systemTextJson.Serialize(o);
        //}
        //else
        //{
        //    return NotSupportedElseIfClasule<string>("Serialize");
        //}
    }

    //public string Serialize(object o)
    //{
    //    if (sl == SerializationLibrary.JsonDanielCrenna)
    //    {
    //        // must be called as static, otherwise during serialize return empty string
    //        return JsonParser.Serialize(o);
    //    }
    //    else if (sl == SerializationLibrary.Microsoft)
    //    {
    //        return systemTextJson.Serialize(o);
    //        return ThrowExceptionsMicrosoftSerializerNotSupported<string>();
    //        //return js.Serialize(o);
    //    }
    //    else if (sl == SerializationLibrary.Newtonsoft)
    //    {
    //        return newtonSoft.Serialize(o);
    //    }
    //    else if (sl == SerializationLibrary.SystemTextJson)
    //    {
    //        return systemTextJson.Serialize(o);
    //    }
    //    else
    //    {
    //        return NotSupportedElseIfClasule<string>("Serialize");
    //    }
    //}

    private T ThrowExceptionsMicrosoftSerializerNotSupported<T>()
    {
        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "System.Web.Scripting.Serialization.JavaScriptSerializer is not supported in Windows Store Apps.");
        return default(T);
    }

    private T NotSupportedElseIfClasule<T>(string v)
    {
        ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "Else if with enum value " + sl + " in JavascriptSerialization." + v);
        return default(T);
    }

    public object Deserialize(String o, Type targetType)
    {
        if (string.IsNullOrEmpty(o))
        {
            ThrowExceptions.IsNullOrEmpty(Exc.GetStackTrace(), type, Exc.CallingMethod(), "o", o);
        }

        if (sl == SerializationLibrary.Utf8Json)
        {
            return utf8json.Deserialize(o, targetType);
        }
        else if (sl == SerializationLibrary.JsonDanielCrenna)
        {
            return utf8json.Deserialize(o, targetType);
        }
        else if (sl == SerializationLibrary.Microsoft)
        {
            #region MyRegion
            //JsonConvert.DeserializeObject()
            //JsonValue.
            //var serializer = new DataContractJsonSerializer(typeof(T));
            //serializer.P
            //T library = (T)serializer.ReadObject(o);
            //return T;
            //return js.Deserialize<T>(o); 
            #endregion

            return utf8json.Deserialize(o, targetType);
        }
        else if (sl == SerializationLibrary.Newtonsoft)
        {
            return utf8json.Deserialize(o, targetType);
        }
        else if (sl == SerializationLibrary.SystemTextJson)
        {
            return utf8json.Deserialize(o, targetType);
        }
        return NotSupportedElseIfClasule<object>("Serialize(String,Type)");
    }

    public T Deserialize<T>(String o)
    {
        return (T)Deserialize(o, typeof(T));
    }
}