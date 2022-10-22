using System;

namespace SecurityPricesDesktopApp.Application.PriceEngine.Extensions
{
    public static class PriceExtensionMethods
    {

        public static decimal NextDecimalByRange(this Random random, int low, int high)
        {
            int decimalPlaceValue = random.Next(0, 99); // Generate Random Decimal Places value

            //The low 32 bits of a 96-bit integer. 
            int lo = random.Next(low, high); // Retrieve a random int range

            int newNumber = int.Parse(lo + decimalPlaceValue.ToString()); // Concatenate the random numbers

            return new Decimal(newNumber, 0, 0, false, 2); ;
        }
    }

}
