using SunamoPayments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SessionStateComgateConverter
{
    public static SessionState? ConvertTo(string value)
    {
        if (value == PaymentState.PENDING.ToString())
        {
            return SessionState.CREATED;
        }

        return (SessionState)Enum.Parse(typeof(SessionState), value);
    }

    public static PaymentState ConvertFrom(SessionState g)
    {
        switch (g)
        {
            case SessionState.CREATED:
            case SessionState.PAYMENT_METHOD_CHOSEN:
            case SessionState.TIMEOUTED:
            case SessionState.REFUNDED:
            case SessionState.PARTIALLY_REFUNDED:
                return PaymentState.PENDING;
                break;
            default:
                break;
        }

        return (PaymentState)Enum.Parse(typeof(PaymentState), g.ToString());
    }
}