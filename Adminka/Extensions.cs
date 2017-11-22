using System;
using System.Configuration;

namespace Adminka
{
    public static class Extensions
    {
        public static string ToConfigLocalTime(this DateTime utcDT)
        {
            var istTZ = TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["Timezone"]);
            return String.Format("{0}", TimeZoneInfo.ConvertTimeFromUtc(utcDT, istTZ).ToShortDateString());
        }
    }
}
