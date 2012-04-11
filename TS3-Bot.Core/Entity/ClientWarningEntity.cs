namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using System;

    /// <summary>
    /// Defines the ClientWarningEntity class.
    /// </summary>
    public class ClientWarningEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientWarningEntity"/> class.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="type">The type.</param>
        public ClientWarningEntity(uint clientId, WarnType type)
        {
            Creation = DateTime.Now;
            ClientId = clientId;
            Type = type;
        }

        /// <summary>
        /// Gets or sets the creation.
        /// </summary>
        /// <value>The creation.</value>
        public DateTime Creation { get; set; }

        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        /// <value>The client id.</value>
        public uint ClientId { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public WarnType Type{ get; set;}
    }
}