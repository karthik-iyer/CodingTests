using System;
using System.Globalization;

namespace InternetBankingRESTfulService.Api
{
    public static class UTCDateTimeExtension
    {
        public static string ToMyCulture(this DateTime dt, CultureInfo info)
        {
            return $"{dt.ToString("yyyy.MM.dd", CultureInfo.InvariantCulture)}.1.0"
            ;
        }
    }
}