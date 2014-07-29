namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using System;

    /// <summary>
    /// Defines the StickyClientEntity class.
    /// </summary>
    [Serializable]
    public struct VotedClientEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VotedClientEntity"/> class.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <param name="channelId">The channel id.</param>
        public VotedClientEntity(uint clientDatabaseId, uint? channelId)
        {
            Creation = DateTime.UtcNow;
            ClientDatabaseId = clientDatabaseId;
            ChannelId = channelId;
        }

        /// <summary>
        /// Gets or sets the creation.
        /// </summary>
        /// <value>The creation.</value>
        public DateTime Creation;

        /// <summary>
        /// Gets or sets the client database id.
        /// </summary>
        /// <value>The client database id.</value>
        public uint ClientDatabaseId;

        /// <summary>
        /// Gets or sets the channel id.
        /// </summary>
        /// <value>The channel id.</value>
        public uint? ChannelId;
    }
}