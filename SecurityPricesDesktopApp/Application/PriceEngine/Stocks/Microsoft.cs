using System;
using SecurityPricesDesktopApp.Application.PriceEngine.Extensions;

namespace SecurityPricesDesktopApp.Application.PriceEngine.Stocks
{
    public class Microsoft : StockTemplate
    {
        public override decimal StockPrice { get => GeneratePrice(); }
        public override int LowPrice { get; } = 250;
        public override int HighPrice { get; } = 290;
        public override string Name { get; } = "Microsoft";
        public override string Ticker { get; } = "MSFT";
        public override bool IsSubscribed { get; set; }

        public Microsoft()
        {
 
        }

        protected override decimal GeneratePrice()
        {

            Random rnd = new Random();
            decimal num = rnd.NextDecimalByRange(LowPrice, HighPrice);

            // A bug happens some times and the decimal returned is much lower than the low price range.
            if (num < LowPrice || num > HighPrice)
            {
                do
                {
                    num = rnd.NextDecimalByRange(LowPrice, HighPrice);
                } while (num < LowPrice || num > HighPrice);
            }


            return num;

        }

        // todo do we want to keep this as a function?
        //public override bool SubscribeToStockPrice(bool subscription = true)
        //{
        //    IsSubscribed = subscription;
        //    return IsSubscribed;
        //}

    }
}
