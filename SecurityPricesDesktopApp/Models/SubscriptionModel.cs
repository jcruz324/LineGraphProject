using System.Collections.Generic;

using SecurityPricesDesktopApp.Application.PriceEngine;
using SecurityPricesDesktopApp.Application.PriceEngine.Stocks;

using MicrosoftModel = SecurityPricesDesktopApp.Application.PriceEngine.Stocks.Microsoft;

namespace SecurityPricesDesktopApp.Models
{
    public class SubscriptionModel //: INotifyPropertyChanged
    {
        private StockTemplate _stockTemplate;

        public StockTemplate StockTemplate
        {
            get => _stockTemplate;
            set {  _stockTemplate = value; }
        }
       

        public SubscriptionModel(StockTemplate stockTemplate)
        {
            StockTemplate = stockTemplate;
        }
        /// <summary>
        /// Inject StockTemplate types
        /// </summary>
        /// <param name="stocks"></param>
        public SubscriptionModel(IList<StockTemplate> stocks)
        {

            stocks.Add(new Amazon());
            stocks.Add(new Apple());
            stocks.Add(new MicrosoftModel());
        }
    }
}
