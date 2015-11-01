using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Regular
{
    public static class SecureConversion
    {
        public static byte ConvertToByte(this string text, byte defaultValue)
        {
            byte returnValue;
            return byte.TryParse(text, out returnValue) ? returnValue : defaultValue;
        }

        public static short ConvertToShort(this string text, short defaultValue)
        {
            short returnValue;
            return short.TryParse(text, out returnValue) ? returnValue : defaultValue;
        }

        public static int ConvertToInt(this string text, int defaultValue)
        {
            int returnValue;
            return int.TryParse(text, out returnValue) ? returnValue : defaultValue;
        }

        public static long ConvertToLong(this string text, long defaultValue)
        {
            long returnValue;
            return long.TryParse(text, out returnValue) ? returnValue : defaultValue;
        }

        public static double ConvertToDouble(this string text, double defaultValue)
        {
            double returnValue;
            return double.TryParse(text, out returnValue) ? returnValue : defaultValue;
        }

        public static DateTime ConvertToDateTime(this string text, DateTime defaultValue)
        {
            DateTime returnValue;
            return DateTime.TryParse(text, out returnValue) ? returnValue : defaultValue;
        }
    }
}
