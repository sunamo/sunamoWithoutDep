using System;
using System.Collections.Generic;
using System.Text;

namespace SunamoPayments
{
    /// <summary>
    /// na webu není žádné číslování https://github.com/gopaycommunity/gopay-dotnet-api/blob/master/GoPay.net-sdk/src/Model/Payment/Payment.cs
    /// proto nemusím mít 2 enumy ani SessionState
    /// </summary>
    public enum SessionState
    {
    CREATED = 0,
    PAYMENT_METHOD_CHOSEN = 1,
    PAID = 2,
    AUTHORIZED=4,
    CANCELED=8,
    TIMEOUTED=16,
    REFUNDED=32,
    PARTIALLY_REFUNDED = 64
}
}