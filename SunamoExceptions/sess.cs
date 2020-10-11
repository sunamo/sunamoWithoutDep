using System;
using System.Collections.Generic;
using System.Text;

namespace SunamoExceptions
{
    public class sess
    {
        static Type type = typeof(sess);

        public static string i18n(string k)
        {
            switch (k)
            {
                case "isNotInWindowsPathFormat":
                    return "is not in Windows Path format";
                default:
                    ThrowExceptions.NotImplementedCase(Exc.GetStackTrace(), type, Exc.CallingMethod(), k);
                    break;
            }

            return null;
        }
    }
}