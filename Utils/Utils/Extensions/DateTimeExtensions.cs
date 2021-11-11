using System;

namespace Utils.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime SpecifyDateAsUtc(this DateTime dateTime)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
        }

        public static DateTime GetMaxBetween(DateTime first, DateTime second)
        {
            return first > second
                ? first
                : second;
        }

        public static string TimeStamp(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMddHHmmssff");
        }

        public static bool IsToday(this DateTime? date)
        {
            return date != null
                   && IsToday(date.Value);
        }

        public static bool IsToday(this DateTime date)
        {
            var today = DateTime.Today;

            return date.Month == today.Month
                   && date.Day == today.Day
                   && date.Year == today.Year;
        }
    }
}