namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using System;

    /// <summary>
    /// Defines the StickyClientEntity class.
    /// </summary>
    public class StickyClientEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StickyClientEntity"/> class.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <param name="channelId">The channel id.</param>
        /// <param name="stickTime">The stick time.</param>
        public StickyClientEntity(uint clientDatabaseId, uint channelId, uint stickTime)
        {
            Creation = DateTime.Now;
            ClientDatabaseId = clientDatabaseId;
            ChannelId = channelId;
            StickTime = stickTime;
        }

        /// <summary>
        /// Gets or sets the creation.
        /// </summary>
        /// <value>The creation.</value>
        public DateTime Creation { get; set; }

        /// <summary>
        /// Gets or sets the client database id.
        /// </summary>
        /// <value>The client database id.</value>
        public uint ClientDatabaseId { get; set; }

        /// <summary>
        /// Gets or sets the channel id.
        /// </summary>
        /// <value>The channel id.</value>
        public uint ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the stick time.
        /// </summary>
        /// <value>
        /// The stick time.
        /// </value>
        public uint StickTime { get; set; }
    }
}