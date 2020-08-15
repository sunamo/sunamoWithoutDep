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

	/// <summary>
	/// Return object is object
	/// 
	/// </summary>
	/// <param name="orderId"></param>
	/// <param name="payment"></param>
	/// <returns></returns>
	public object CreatePayment(string orderId, BaseComGatePayment payment)
    {
		Payer customer = new Payer();

		customer.Contact = new Contact()
		{
			Email = CmConsts.Email,
			Name = CmConsts.Email
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

	public SunamoComgateHelper(ComGateData d)
	{
		comGateAPI = ComGateApiConnector.CreateConnector()
		.TestEnviroment()
		.SetLang()
		.SetMerchant(d.merchantId)
		.SetSecret(d.secret);
		comGateAPI.PrepareOnly = true;
	}



	public BaseComGatePayment CreateBasePayment(string orderId)
	{
		BasicPaymentViewModel model = new BasicPaymentViewModel { Price = 10, Name = "Name", Email = CmConsts.Email, Label = "Item1" };
		model.ReferenceId = orderId;//HttpRequestHelper.GetResponseText(AppsHandlersUri.OrderId(Consts.localhost), HttpMethod.Get, new HttpRequestData { });

		var cents = (int)(model.Price * 100);
		BaseComGatePayment payment = PaymentFactory.GetBasePayment(cents, model.ReferenceId, model.Label, PaymentMethods.ALL);

		return payment;
	}

	public SessionState Status(string paymentSessionId)
    {
		var st = comGateAPI.GetPaymentStatus(paymentSessionId, CmConsts.api);
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

		if (!transId.Contains(d))
        {
			transId = transId.Insert(8, d);
			transId = transId.Insert(4, d);
		}

		return transId;
    }
}