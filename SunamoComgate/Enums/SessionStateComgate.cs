using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Must be the same as SessionStateGoPay!!!
/// </summary>
public enum SessionStateComgate : byte
{
    CREATED = 0,
    PAYMENT_METHOD_CHOSEN = 1,
    PAID = 2,
    AUTHORIZED = 4,
    CANCELED = 8,
    TIMEOUTED = 16,
    REFUNDED = 32,
    PARTIALLY_REFUNDED = 64

    //CREATED = 0,
    //    PAYMENT_METHOD_CHOSEN = 1,
    //    PAID = 2,
    //    AUTHORIZED = 3,
    //    CANCELED = 4,
    //    TIMEOUTED = 5,
    //    REFUNDED = 6,
    //    PARTIALLY_REFUNDED = 7
}