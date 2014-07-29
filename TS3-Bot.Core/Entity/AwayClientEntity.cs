namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using System;

    /// <summary>
    /// Defines the AwayClientEntity struct.
    /// </summary>
    [Serializable]
    public class AwayClientEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AwayClientEntity"/> class.
        /// </summary>
        public AwayClientEntity()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwayClientEntity"/> class.
        /// </summary>
        /// <param name="channelId">The channel id.</param>
        public AwayClientEntity(uint channelId)
        {
            Creation = DateTime.UtcNow;
            ClientDatabaseId = 0;
            LastChannelId = channelId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwayClientEntity"/> class.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <param name="channelId">The channel id.</param>
        public AwayClientEntity(uint clientDatabaseId, uint channelId)
        {
            Creation = DateTime.UtcNow;
            ClientDatabaseId = clientDatabaseId;
            LastChannelId = channelId;
        }

        /// <summary>
        /// Gets or sets the creation.
        /// </summary>
        /// <value>The creation.</value>
        public DateTime Creation;

        /// <summary>
        /// Gets or sets the client database id.
        /// </summary>
        /// <value>The channel id.</value>
        public uint ClientDatabaseId;

        /// <summary>
        /// Gets or sets the channel id.
        /// </summary>
        /// <value>The channel id.</value>
        public uint LastChannelId;
    }
}