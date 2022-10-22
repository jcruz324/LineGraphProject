using System;

using SecurityPricesDesktopApp.Application.PriceEngine.Extensions;

namespace SecurityPricesDesktopApp.Application.PriceEngine.Stocks
{
    public class Apple : StockTemplate
    {
        public override decimal StockPrice { get => GeneratePrice(); }
        public override int LowPrice { get; } = 150;
        public override int HighPrice { get; } = 165;
        public override string Name { get; } = "Apple";
        public override string Ticker { get; } = "AAPL";
        public override bool IsSubscribed { get; set; }

        protected override decimal GeneratePrice()
        {
            //Random rnd = new Random();

            //return rnd.NextDecimalByRange(LowPrice, HighPrice);
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
    }
}
