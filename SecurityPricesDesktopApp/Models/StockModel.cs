using System.Collections.Generic;
using SecurityPricesDesktopApp.Application.PriceEngine;
using SecurityPricesDesktopApp.Application.PriceEngine.Stocks;

using MicrosoftModel = SecurityPricesDesktopApp.Application.PriceEngine.Stocks.Microsoft;

namespace SecurityPricesDesktopApp.Models
{
    public class StockModel
    {
        public Amazon Amazon { get; set; }
        public Apple Apple { get; set; }
        public MicrosoftModel Microsoft { get; set; }

        public StockModel(Amazon amazon, Apple apple, MicrosoftModel microsoft)
        {
            Amazon = amazon;
            Apple = apple;
            Microsoft = microsoft;

        }
        /// <summary>
        /// Inject StockTemplate types
        /// </summary>
        /// <param name="stocks"></param>
        public StockModel(IList<StockTemplate> stocks)
        {

            stocks.Add(new Amazon());
            stocks.Add(new Apple());
            stocks.Add(new MicrosoftModel());
        }
        
    }
}
