using GoPay.Model.Payment;
using GoPay.Model.Payments;

public interface ISunamoPaymentGateway<BasePayment, Payment, PaymentSessionId, SessionState>
{
    object CreatePayment(BasePayment payment);
    SessionState IsPayed(PaymentSessionId paymentSessionId);
    Payment Status(PaymentSessionId paymentSessionId);
}