using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace SunamoExceptions
{ 
public class SH
{
        public static string WrapWithQm(string commitMessage)
        {
            return SH.WrapWith(commitMessage, AllChars.qm);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string WrapWith(string value, char v, bool _trimWrapping = false)
        {
            // TODO: Make with StringBuilder, because of SH.WordAfter and so
            return WrapWith(value, v.ToString());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string WrapWith(string value, string h, bool _trimWrapping = false)
        {
            return h + SH.Trim(value, h) + h;
        }

        public static string TrimStart(string v, string s)
        {
            while (v.StartsWith(s))
            {
                v = v.Substring(s.Length);
            }
            return v;
        }

        public static string TrimEnd(string name, string ext)
        {
            while (name.EndsWith(ext))
            {
                return name.Substring(0, name.Length - ext.Length);
            }
            return name;
        }

        public static string Trim(string s, string args)
        {
            s = TrimStart(s, args);
            s = TrimEnd(s, args);

            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nazevPP"></param>
        /// <param name="only"></param>
        public static string FirstCharUpper(string nazevPP, bool only = false)
        {
            if (nazevPP != null)
            {
                string sb = nazevPP.Substring(1);
                if (only)
                {
                    sb = sb.ToLower();
                }
                return nazevPP[0].ToString().ToUpper() + sb;
            }
            return null;
        }


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
            if (CA.Count(parts) == 0)
            {
                return string.Empty;
            }

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