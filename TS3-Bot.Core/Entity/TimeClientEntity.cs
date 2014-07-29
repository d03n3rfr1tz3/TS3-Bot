namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using System;

    /// <summary>
    /// Defines the TimeClientEntity struct.
    /// </summary>
    [Serializable]
    public class TimeClientEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeClientEntity"/> struct.
        /// </summary>
        public TimeClientEntity()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeClientEntity"/> struct.
        /// </summary>
        /// <param name="userDatabaseId">The user database id.</param>
        /// <param name="joined">The joined.</param>
        /// <param name="disconnected">The disconnected.</param>
        public TimeClientEntity(uint userDatabaseId, DateTime joined, DateTime? disconnected = null)
        {
            User = userDatabaseId;
            Joined = joined;
            Disconnected = disconnected ?? DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public uint User;

        /// <summary>
        /// Gets or sets the joined.
        /// </summary>
        /// <value>
        /// The joined.
        /// </value>
        public DateTime Joined;

        /// <summary>
        /// Gets or sets the disconnected.
        /// </summary>
        /// <value>
        /// The disconnected.
        /// </value>
        public DateTime Disconnected;

        /// <summary>
        /// Gets or sets the total minutes.
        /// </summary>
        /// <value>
        /// The total minutes.
        /// </value>
        public int TotalMinutes;

        public double GetTime(DateTime? fromTime, DateTime? toTime)
        {
            if (Joined.Date == Disconnected.Date || (!fromTime.HasValue && !toTime.HasValue)) return TotalMinutes;

            var from = fromTime ?? DateTime.MinValue;
            var to = toTime ?? DateTime.MaxValue;
            var joined = Joined < from ? from : Joined;
            var disconnected = Disconnected > to ? to : Disconnected;
            return (disconnected - joined).TotalMinutes;
        }
    }
}