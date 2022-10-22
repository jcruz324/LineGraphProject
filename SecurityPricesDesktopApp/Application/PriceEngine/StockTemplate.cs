
namespace SecurityPricesDesktopApp.Application.PriceEngine
{
    /// <summary>
    /// Template Pattern: This provides the methods needed to override by the stock/security subclasses
    /// </summary>
    public abstract class StockTemplate
    {
        public abstract decimal StockPrice { get; }
        public abstract int LowPrice { get; }
        public abstract int HighPrice { get; }
        public abstract string Name { get; }
        public abstract string Ticker { get; }
        public abstract bool IsSubscribed { get; set; }
        protected StockTemplate()
        {
            
        }

        protected virtual decimal GeneratePrice()
        {
            return StockPrice;
        }


    }
}
