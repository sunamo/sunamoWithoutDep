using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SunamoExceptions
{ 
public class SH
{
        /// <summary>
        /// If null, return Consts.nulled
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static string NullToStringOrDefault(object n)
        {
            return NullToStringOrDefault(n, null);
        }

        /// <summary>
        /// If null, return Consts.nulled
        /// </summary>
        /// <param name="n"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string NullToStringOrDefault(object n, string v)
        {
            if (v == null)
            {
                if (n == null)
                {
                    v = Consts.nulled;
                }
                else
                {
                    v = n.ToString();
                }
            }
            if (n != null)
            {
                return AllStrings.space + v;
            }
            return " " + Consts.nulled;
        }

        /// <summary>
        /// Will be delete after final refactoring
        /// Automaticky ořeže poslední znad A1
        /// Pokud máš inty v A2, použij metodu JoinMakeUpTo2NumbersToZero
        /// </summary>
        /// <param name="delimiter"></param>
        /// <param name="parts"></param>
        public static string Join(IEnumerable parts, object delimiter)
    {
        var d = delimiter.ToString();

        StringBuilder sb = new StringBuilder();
        foreach (var item in parts)
        {
            sb.Append(item.ToString() + d);
        }

        var vr = sb.ToString();
        return vr.Substring(0, vr.Length - d.Length);
    }

        public static List<string> Split(string parametry, params object[] deli)
        {
            return Split(StringSplitOptions.RemoveEmptyEntries, parametry, deli);
        }

        private static List<string> Split(StringSplitOptions removeEmptyEntries, string parametry, params object[] deli)
        {
            string[] sep = new string[deli.Length];
            for (int i = 0; i < sep.Length; i++)
            {
                sep[i] = deli[i].ToString();
            }
            return parametry.Split(sep, removeEmptyEntries).ToList() ;
        }

        public static string Format2(string status, params object[] args)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return string.Empty;
            }
            if (status.Contains(AllChars.lcub) && !status.Contains("{0}"))
            {
                return status;
            }
            else
            {
                try
                {
                    return string.Format(status, args);
                }
                catch (Exception ex)
                {
                    return status;
                }
            }
        }

    }
}