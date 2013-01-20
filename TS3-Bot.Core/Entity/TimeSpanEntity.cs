namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Defines the TimeSpanEntity struct.
    /// </summary>
    public struct TimeSpanEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeSpanEntity"/> struct.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        public TimeSpanEntity(DateTime? fromDate, DateTime? toDate)
        {
            FromDate = fromDate;
            ToDate = toDate;
        }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime? FromDate;

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime? ToDate;

        /// <summary>
        /// Parses the specified timespan string.
        /// </summary>
        /// <param name="timespan">The timespan.</param>
        /// <returns>Returns a parsed TimeSpan Entity.</returns>
        internal static TimeSpanEntity Parse(string timespan)
        {
            timespan = timespan == null ? null : timespan.ToLower();

            var today = DateTime.Today;
            switch (timespan)
            {
                case "today":
                    return new TimeSpanEntity(today, today.AddDays(1));
                case "week":
                    return new TimeSpanEntity(today.AddDays(1 - (int)today.DayOfWeek), today.AddDays(1));
                case "month":
                    return new TimeSpanEntity(new DateTime(today.Year, today.Month, 1, 0, 0, 0, 0), today.AddDays(1));
                default:
                    return new TimeSpanEntity();
            }
        }

        /// <summary>
        /// Tries to parse the specified timespan string.
        /// </summary>
        /// <param name="timespan">The timespan.</param>
        /// <param name="timeSpanEntity">The time span entity.</param>
        /// <returns>Returns a parsed TimeSpan Entity.</returns>
        internal static bool TryParse(string timespan, out TimeSpanEntity timeSpanEntity)
        {
            timespan = timespan == null ? null : timespan.ToLower();

            var today = DateTime.Today;
            switch (timespan)
            {
                case "today":
                    timeSpanEntity = new TimeSpanEntity(today, today.AddDays(1));
                    return true;
                case "week":
                    var dayOfWeek = (int)today.DayOfWeek - (int)CultureInfo.CurrentUICulture.DateTimeFormat.FirstDayOfWeek;
                    if (dayOfWeek < 0) dayOfWeek = dayOfWeek + 7;
                    timeSpanEntity = new TimeSpanEntity(today.AddDays(0 - dayOfWeek), today.AddDays(1));
                    return true;
                case "month":
                    timeSpanEntity = new TimeSpanEntity(new DateTime(today.Year, today.Month, 1, 0, 0, 0, 0), today.AddDays(1));
                    return true;
            }

            timeSpanEntity = new TimeSpanEntity();
            return false;
        }
    }
}