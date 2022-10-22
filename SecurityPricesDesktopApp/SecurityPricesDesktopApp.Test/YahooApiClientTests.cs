using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RestSharp;

namespace SecurityPricesDesktopApp.Test
{
    [TestClass]
    public class YahooApiClientTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var client = new RestClient("https://yahoo-finance97.p.rapidapi.com/price");
            var request = new RestRequest("https://yahoo-finance97.p.rapidapi.com/price", Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("X-RapidAPI-Key", "46211ad742msh896b54e8e29f443p11d583jsn79617695415a");
            request.AddHeader("X-RapidAPI-Host", "yahoo-finance97.p.rapidapi.com");
            request.AddParameter("application/x-www-form-urlencoded", "symbol=AAPL&period=1d", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            var response = client.Execute(request);
        }
    }
}
