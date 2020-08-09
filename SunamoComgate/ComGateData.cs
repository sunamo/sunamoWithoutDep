using System;
using System.Collections.Generic;
using System.Text;

public class ComGateData
{
    public readonly string merchantId = null;
    public readonly string secret = null;
    public readonly string api = null;

    public ComGateData(string merchantId, string secret, string api)
    {
        this.merchantId = merchantId;
        this.secret = secret;
        this.api = api;
    }
}