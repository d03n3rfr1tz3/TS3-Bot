namespace DirkSarodnick.TS3_Bot.Core.Helper
{
    using System;

    public static class DateTimeHelper
    {
        private static DateTime BaseDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static int ToTimeStamp(this DateTime dateTime)
        {
            return (int)(dateTime.ToUniversalTime() - BaseDate).TotalSeconds;
        }

        public static DateTime ToDateTime(this Int32 timestamp)
        {
            return BaseDate.AddSeconds(timestamp);
        }

        public static DateTime ToDateTime(this Int64 timestamp)
        {
            return BaseDate.AddSeconds(timestamp);
        }
    }
}
