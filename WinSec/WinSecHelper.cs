using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class WinSecHelper
    {
        public static string CurrentUserName()
        {
            return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }

    /// <summary>
    /// SczSecureCodes.winAccountUserName, SczSecureCodes.machineName
    /// </summary>
    /// <param name="winAccountUserName"></param>
    /// <param name="machineName"></param>
    /// <returns></returns>
    public static bool IsMyComputer(string winAccountUserName, string machineName)
        {
            var un = CurrentUserName();
            if (un == winAccountUserName && Environment.MachineName == machineName)
            {
                return true;
            }
            return false;
        }
    }
