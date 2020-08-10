using SunamoPayments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class SessionStateGopayConverter
{
    /// <summary>
    /// ConvertToGopaySessionState
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static SessionState? ConvertTo(SessionState value)
    {
        return (SessionState)Enum.Parse(typeof(SessionState), value.ToString());
    }

    /// <summary>
    /// ConvertFromGopaySessionState
    /// </summary>
    /// <param name="g"></param>
    /// <returns></returns>
    public static SessionState ConvertFrom(SessionState g)
    {
        return (SessionState)Enum.Parse(typeof(SessionState), g.ToString());
    }
}