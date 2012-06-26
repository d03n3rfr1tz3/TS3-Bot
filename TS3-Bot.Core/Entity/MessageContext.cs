namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    public class MessageContext
    {
        /// <summary>
        /// Gets or sets the client database id.
        /// </summary>
        /// <value>The client database id.</value>
        public uint? ClientDatabaseId { get; set; }

        /// <summary>
        /// Gets or sets the client nickname.
        /// </summary>
        /// <value>The client nickname.</value>
        public string ClientNickname { get; set; }

        /// <summary>
        /// Gets or sets the client registered.
        /// </summary>
        /// <value>The client registered.</value>
        public string ClientRegistered { get; set; }

        /// <summary>
        /// Gets or sets the client last login.
        /// </summary>
        /// <value>The client last login.</value>
        public string ClientLastLogin { get; set; }

        /// <summary>
        /// Gets or sets the client last seen.
        /// </summary>
        /// <value>The client last seen.</value>
        public string ClientLastSeen { get; set; }

        /// <summary>
        /// Gets or sets the client hours.
        /// </summary>
        /// <value>The client hours.</value>
        public double? ClientHours { get; set; }

        /// <summary>
        /// Gets or sets the channel id.
        /// </summary>
        /// <value>The channel id.</value>
        public uint? ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the name of the channel.
        /// </summary>
        /// <value>The name of the channel.</value>
        public string ChannelName { get; set; }

        /// <summary>
        /// Gets or sets the server id.
        /// </summary>
        /// <value>The server id.</value>
        public uint? ServerId { get; set; }

        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>The name of the server.</value>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        /// <value>The server port.</value>
        public ushort? ServerPort { get; set; }

        /// <summary>
        /// Gets or sets the name of the event.
        /// </summary>
        /// <value>The name of the event.</value>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the file created.
        /// </summary>
        /// <value>The file created.</value>
        public string FileCreated { get; set; }

        /// <summary>
        /// Gets or sets the size of the file.
        /// </summary>
        /// <value>The size of the file.</value>
        public ulong? FileSize { get; set; }

        /// <summary>
        /// Gets or sets the moderator verified.
        /// </summary>
        /// <value>
        /// The moderator verified.
        /// </value>
        public int? ModeratorVerified { get; set; }

        /// <summary>
        /// Gets or sets the server group id.
        /// </summary>
        /// <value>
        /// The server group id.
        /// </value>
        public uint? ServerGroupId { get; set; }

        /// <summary>
        /// Gets or sets the name of the server group.
        /// </summary>
        /// <value>
        /// The name of the server group.
        /// </value>
        public string ServerGroupName { get; set; }
    }
}