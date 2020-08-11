using SunamoExceptions;
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
        value = value.ToUpper();

        if (value == PaymentState.PENDING.ToString())
        {
            return SessionState.CREATED;
        }

        //SessionState has CANCELED (1 L), PaymentState has CANCELLED
        if (value == PaymentState.CANCELLED.ToString())
        {
            value = SessionState.CANCELED.ToString();
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