namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using System;

    /// <summary>
    /// Defines the StickyClientEntity class.
    /// </summary>
    public class VotedClientEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VotedClientEntity"/> class.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        public VotedClientEntity(uint clientDatabaseId)
        {
            Creation = DateTime.Now;
            ClientDatabaseId = clientDatabaseId;
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
        public uint? ChannelId { get; set; }
    }
}