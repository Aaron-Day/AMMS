using System;

namespace AMMS.Services
{
    public static class Formatting
    {
        public static string AsMilitaryDate(DateTime date)
        {
            return date.ToLocalTime().ToString("yyyyMMdd");
        }

        public static string AsMilDate(DateTime date)
        {
            return date.ToLocalTime().ToString("yyMMdd");
        }

        public static string AsJDate(DateTime date)
        {
            return date.ToLocalTime().ToString(date.Year % 10 + date.DayOfYear.ToString("000"));
        }
        public static string AsMilitaryDateTime(DateTime date)
        {
            return date.ToLocalTime().ToString("yyyyMMdd HH:mm:ss");
        }

        public static string AsMilDateTime(DateTime date)
        {
            return date.ToLocalTime().ToString("yyMMdd HH:mm:ss");
        }

        public static string AsJDateTime(DateTime date)
        {
            return date.ToLocalTime().ToString(date.Year % 10 + date.DayOfYear.ToString("000") + " HH:mm:ss");
        }

        public static string AsUSDateTime(DateTime date)
        {
            return date.ToLocalTime().ToString("MM/dd/yyyy hh:mm:ss tt");
        }

        public static string AsEuroDateTime(DateTime date)
        {
            return date.ToLocalTime().ToString("dd/MM/yyyy hh:mm:ss tt");
        }

        public static string AsAbbDate(DateTime date)
        {
            return date.ToLocalTime().ToString("dd MMM yy").ToUpper();
        }

        public static string TimeStamp(DateTime date)
        {
            return date.ToLocalTime().ToString("yyMMddHHmmssfff");
        }

        public static string AsMilTime(DateTime time)
        {
            return time.ToLocalTime().ToString("HHmm");
        }
    }
}
