public interface ISunamoPaymentGateway<BasePayment, SessionState>
{
    /// <summary>
    /// Return string error or Payment if success
    /// </summary>
    /// <param name="payment"></param>
    /// <returns></returns>
    object CreatePayment(string orderId, BasePayment payment);
    /// <summary>
    /// Must be string, not PaymentSessionId, due to in gp is long and in cg string
    /// </summary>
    /// <param name="paymentSessionId"></param>
    /// <returns></returns>
    SessionState Status(string paymentSessionId);
    //Payment PaymentObject(PaymentSessionId paymentSessionId);
}