using SunamoExceptions;
using SunamoPayments;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using THsoftware.ComGate.Core.Domain.Enums;
using THsoftware.ComGate.Core.Domain.Models;
using THsoftware.ComGate.PaymentAPI.Interfaces.Factories;
using THsoftware.ComGate.PaymentAPI.Services;

public class SunamoComgateHelper : ISunamoPaymentGateway<BaseComGatePayment, SessionState>
{
	public object ParseMessage(string m)
    {
		return null;
    }

	public static SunamoComgateHelper Instance = new SunamoComgateHelper();

    private SunamoComgateHelper()
    {

    }

	static Type type = typeof(SunamoComgateHelper);

	/// <summary>
	/// Return object is object
	/// 
	/// </summary>
	/// <param name="orderId"></param>
	/// <param name="payment"></param>
	/// <returns></returns>
	public object CreatePayment(string orderId, BaseComGatePayment payment, params object[] args)
    {
		Payer customer = new Payer();

		string buyerMail = null;

		if (args.Length== 0)
        {
            try
            {
				ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "args was empty. Is used " + CmConsts.Email);
			}
            catch (Exception)
            {
            }
			buyerMail = CmConsts.Email;
        }
        else
        {
			buyerMail = args[0].ToString();
		}
		
		customer.Contact = new Contact()
		{
			Email = buyerMail,
			Name = buyerMail
		};
		
		//var payment = CreateBasePayment(orderId);
		payment.PrepareOnly = true;

		// CreatePaymentAsync,CreatePayment - working in Cmd
		// CreatePayment - working in APp, CreatePaymentAsync - not working in APp

		ApiResponse<PaymentResponse> response = comGateAPI.CreatePayment(payment, customer, CmConsts.api);

        if (response.Response == null)
        {
			var m = response.Message;

			if (m.StartsWith( ComgateNotTranslateAble.afulStart))
            {
				var uriManage = ComgateNotTranslateAble.uriManage;
				var ip = m.Replace(ComgateNotTranslateAble.afulStart, string.Empty);
				ip = ip.Replace(ComgateNotTranslateAble.afulEnd, string.Empty);
				//Clipboard.SetText(ip);
				Process.Start(uriManage);

				Debug.WriteLine("Insert " +ip+" to " + uriManage);
				//Access from unauthorized location [37. 188.150.241]!		
			}
		}
		var pc = new CreatePaymentResult { pc = response.Response, error = response.Message };
        // cant here check for null, coz there is not ErrorCapture (webforms)

		return pc;
	}

	public ComGateApiConnector comGateAPI = null;

	public SunamoComgateHelper(ComGateData d, bool isMyPc)
	{
		comGateAPI = ComGateApiConnector.CreateConnector(isMyPc)
		//.TestEnviroment()
		.SetLang()
		.SetMerchant(d.merchantId)
		.SetSecret(d.secret);
		comGateAPI.PrepareOnly = true;
	}
	public BaseComGatePayment CreateBasePayment(string orderId, string label, decimal price)
	{
		//model.Price
		var cents = (int)(price * 100);

		//BasicPaymentViewModel model = new BasicPaymentViewModel { Price = 10, Name = "Name", Email = CmConsts.Email, Label = "Item1" };
		//model.ReferenceId = orderId;//HttpClientHelper.GetResponseText(AppsHandlersUri.OrderId(Consts.localhost), HttpMethod.Get, new HttpRequestData { });

		
		BaseComGatePayment payment = PaymentFactory.GetBasePayment(cents, orderId, label, PaymentMethods.ALL);
		

		return payment;
	}

	public SessionState Status(string paymentSessionId)
    {
		var st = comGateAPI.GetPaymentStatus(paymentSessionId, CmConsts.api);

		if (st.Response == null)
		{
			// TODO: Remove completely from table - musím dát pozor zda nehledám production objednávku na testingu a opačně

			return SessionState.TIMEOUTED;
		}

		var r = SessionStateComgateConverter.ConvertTo( st.Response.Status.ToString());
        if (r.HasValue)
        {
			return r.Value;
        }
		return SessionState.CREATED;
    }

    public  string InsertDashes(string transId)
    {
		string d = AllStrings.dash;

        if (transId.Length < 8)
        {
			ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "transId has less than 8 letters: " + transId);

		}

		if (!transId.Contains(d))
        {
			transId = transId.Insert(8, d);
			transId = transId.Insert(4, d);
		}

		return transId;
    }
}