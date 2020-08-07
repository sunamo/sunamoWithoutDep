using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NameValueCollectionDebug
{
    public static void Print(NameValueCollection nvc)
    {
        foreach (var item in nvc.AllKeys)
        {
            d(item + ": " + nvc[item]);
        }
    }

    private static void d(string v)
    {
        Debug.WriteLine(v);
    }
}