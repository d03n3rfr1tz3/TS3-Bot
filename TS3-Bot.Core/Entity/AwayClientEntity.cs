namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using System;

    /// <summary>
    /// Defines the AwayClientEntity struct.
    /// </summary>
    [Serializable]
    public struct AwayClientEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AwayClientEntity"/> class.
        /// </summary>
        /// <param name="channelId">The channel id.</param>
        public AwayClientEntity(uint channelId)
        {
            Creation = DateTime.UtcNow;
            LastChannelId = channelId;
        }

        /// <summary>
        /// Gets or sets the creation.
        /// </summary>
        /// <value>The creation.</value>
        public DateTime Creation;

        /// <summary>
        /// Gets or sets the channel id.
        /// </summary>
        /// <value>The channel id.</value>
        public uint LastChannelId;
    }
}