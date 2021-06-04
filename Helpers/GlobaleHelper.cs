using System;
using System.Collections.Generic;
using System.Text;

namespace ANTapPayment.Helpers
{
    internal static class GlobaleHelper
    {
        public static T StringToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }


        public static double ToTickFormat(this DateTime dateTime)
        {
            var date = dateTime.ToUniversalTime();
            var ticks = date.Ticks - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).Ticks;
            var ts = ticks / TimeSpan.TicksPerMillisecond;
            return ts;
        }

        public static double ToTickFormat(this DateTime? dateTime)
        {
            if (!dateTime.HasValue)
                return 0;
            return dateTime.Value.ToTickFormat();
        }

        public static int IntValue(this Enum argEnum)
        {
            return Convert.ToInt32(argEnum);
        }

        public static bool IsEmpty(this string argEnum)
        {
            return String.IsNullOrEmpty(argEnum);
        }
    }
}
