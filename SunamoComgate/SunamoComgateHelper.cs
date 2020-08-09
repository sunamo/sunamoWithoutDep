using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using THsoftware.ComGate.Core.Domain.Enums;
using THsoftware.ComGate.Core.Domain.Models;
using THsoftware.ComGate.PaymentAPI.Interfaces.Factories;
using THsoftware.ComGate.PaymentAPI.Services;

public class SunamoComgateHelper : ISunamoPaymentGateway<BaseComGatePayment, PaymentResponse, long, SessionStateComgate>
{
	//public object CreatePayment(string orderId, BaseComGatePayment payment)
	//{
	//	return CreatePaymentWorker(orderId, payment).Result;
	//}

	public object ParseMessage(string m)
    {
		return null;
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

		ApiResponse<PaymentResponse> response =  comGateAPI.CreatePayment(payment, customer, CmConsts.api);

        if (response.Response == null)
        {
			var m = response.Message;

			if (m.StartsWith( ComgateNotTranslateAble.afulStart))
            {
				var uriManage = ComgateNotTranslateAble.uriManage;
				var ip = SH.ReplaceAll(m, string.Empty, ComgateNotTranslateAble.afulStart, ComgateNotTranslateAble.afulEnd);
				ClipboardHelper.SetText(ip);
				PH.Start(uriManage);

				DebugLogger.Instance.WriteLine("Insert IP address to " + uriManage);
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

	public SessionStateComgate IsPayed(long paymentSessionId)
    {
        throw new NotImplementedException();
    }

    public PaymentResponse Status(long paymentSessionId)
    {
        throw new NotImplementedException();
    }

}