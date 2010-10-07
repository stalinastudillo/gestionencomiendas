using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
namespace Gestion
{
    static class Utils
    {
        public static float parseAndRound(String str)
        {
            str=str.Replace(",", ".");
            float res = 0;
            double doubleRes = 0;
            NumberFormatInfo numberFormat = new NumberFormatInfo();
            numberFormat.NumberDecimalSeparator = ".";
            numberFormat.CurrencyDecimalSeparator = ".";
            res=float.Parse(str, numberFormat);
            doubleRes=Math.Round(res, 2);
            return ((float)doubleRes);
        }
    }
}
