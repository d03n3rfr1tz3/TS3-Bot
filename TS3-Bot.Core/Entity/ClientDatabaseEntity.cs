namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using System;

    /// <summary>
    /// Defines the ClientDatabaseEntity class.
    /// </summary>
    public class ClientDatabaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientDatabaseEntity"/> class.
        /// </summary>
        public ClientDatabaseEntity()
        {
            Nickname = string.Empty;
            NicknameChange = DateTime.MinValue;
            LastConnected = DateTime.MinValue;
        }

        /// <summary>
        /// Gets or sets the client database id.
        /// </summary>
        /// <value>The client database id.</value>
        public uint ClientDatabaseId { get; set; }

        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>The nickname.</value>
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or sets the nickname change.
        /// </summary>
        /// <value>The nickname change.</value>
        public DateTime NicknameChange { get; set; }

        /// <summary>
        /// Gets or sets the last connected.
        /// </summary>
        /// <value>The last connected.</value>
        public DateTime LastConnected { get; set; }
    }
}