using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecurityPricesDesktopApp.Application.PriceEngine.Stocks;
using System;
using SecurityPricesDesktopApp.Application.PriceEngine;
using SecurityPricesDesktopApp.Application.PriceEngine.Extensions;
using MicrosoftModel = SecurityPricesDesktopApp.Application.PriceEngine.Stocks.Microsoft;


namespace SecurityPricesDesktopApp.Test
{
    [TestClass]
    public class PricingEngineTestsGitHub
    {
        [TestMethod]
        public void MicrosoftPricingTestGitHub()
        {
            Apple apple = new Apple();
            decimal applePrice = apple.StockPrice;
            //Assert.IsNotNull(whatPrice);

            MicrosoftModel microsoft = new MicrosoftModel();
            //PriceEngine.Stocks.Microsoft microsoft = new PriceEngine.Stocks.Microsoft();
            decimal microsoftPrice = microsoft.StockPrice;
            Assert.IsNotNull(microsoftPrice);

        }

        [TestMethod]
        public void TestGeneratingDecimalsGitHub()
        {
            Decimal randomDecimal = new Decimal(15547, 0, 0, false, 2);
            Decimal randomDecimalAgain = new Decimal(53.5);


            Random random = new Random();
            var generateDecimal = random.NextDecimalByRange(150, 160);

        }

        [TestMethod]
        public void TestStockTemplatingGitHub()
        {
            Amazon amazon = new Amazon();

            decimal stockPrice = amazon.StockPrice;
            //amazon.SubscribeToStockPrice(false);

        }
    }
}
