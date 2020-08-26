using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

public class HttpRequestHelperDebug
{
    public static void PostParams(HttpRequest h)
    {
        string[] keys = h.Form.AllKeys;
        for (int i = 0; i < keys.Length; i++)
        {
            var v = h.Form[keys[i]];

            Debug.WriteLine(keys[i] + ": " + v);
        }
    }
}