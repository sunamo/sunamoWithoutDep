//
//using System.Xml;
//using System.Collections.Generic;
//using System;

//public partial class SunamoJsonHelper
//{
//    /// <summary>
//    /// Musí se to zkonvertovat do xml, protože to je jediná možnost jak to parsovat.
//    /// </summary>
//    /// <param name = "fd"></param>
//    public static string ConvertToXml(string fd)
//    {
//        string deserializedRootElementName = null;
//        //deserializedRootElementName = ThisApp.Name;
//        return JsonConvert.DeserializeXmlNode(fd, deserializedRootElementName, false).OuterXml;
//    }

//    public static string SerializeXmlNode(XmlNode xn)
//    {
//        return JsonConvert.SerializeXmlNode(xn);
//    }

//    /// <summary>
//    /// Nač JSON převádět na JSON? 
//    /// </summary>
//    /// <param name = "fd"></param>
//    public static string ConvertToJson(string fd)
//    {
//        return JsonConvert.DeserializeObject(fd).ToString();
//    }

//    public static string FormatJson(string json)
//    {
//        dynamic parsedJson = JsonConvert.DeserializeObject(json);
//        return JsonConvert.SerializeObject(parsedJson, Newtonsoft.Json.Formatting.Indented);
//    }

//    public static string SerializeXmlString(object p)
//    {
//        return JsonConvert.SerializeObject(p);
//    }

//}