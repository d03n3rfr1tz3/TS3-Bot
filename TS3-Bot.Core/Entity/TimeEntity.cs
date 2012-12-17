namespace DirkSarodnick.TS3_Bot.Core
{
    using System;

    public partial class Time
    {
        /// <summary>
        /// Gets the time.
        /// </summary>
        /// <param name="fromTime">From time.</param>
        /// <param name="toTime">To time.</param>
        /// <returns></returns>
        public TimeSpan GetTime(DateTime? fromTime, DateTime? toTime)
        {
            var from = fromTime ?? DateTime.MinValue;
            var to = toTime ?? DateTime.MaxValue;
            var joined = Joined < from ? from : Joined;
            var disconnected = Disconnected > to ? to : Disconnected;
            return disconnected - joined;
        }
    }
}
