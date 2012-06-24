namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using System;
    using System.Collections.Generic;
    using TS3QueryLib.Core.Query.HelperClasses;
    using TS3QueryLib.Core.Query.Notification.EventArgs;
    using TS3QueryLib.Core.Query.Responses;

    /// <summary>
    /// Defines the SimpleClientEntity class.
    /// </summary>
    public class SimpleClientEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleClientEntity"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public SimpleClientEntity(ClientListEntry client)
        {
            this.ClientDatabaseId = client.ClientDatabaseId;
            this.ClientId = client.ClientId;
            this.Nickname = client.Nickname;
            this.ServerGroups = new List<uint>(client.ServerGroups);
            this.ChannelId = client.ChannelId;
            this.Connected = client.ClientLastConnected;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleClientEntity"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="clientId">The client id.</param>
        public SimpleClientEntity(ClientInfoResponse client, uint clientId)
        {
            this.ClientDatabaseId = client.DatabaseId;
            this.ClientId = clientId;
            this.Nickname = client.Nickname;
            this.ServerGroups = new List<uint>(client.ServerGroups);
            this.ChannelId = client.ChannelId;
            this.Connected = client.LastConnected;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleClientEntity"/> class.
        /// </summary>
        /// <param name="client">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.ClientJoinedEventArgs"/> instance containing the event data.</param>
        public SimpleClientEntity(ClientJoinedEventArgs client)
        {
            this.ClientDatabaseId = client.ClientDatabaseId;
            this.ClientId = client.ClientId;
            this.Nickname = client.Nickname;
            this.ServerGroups = new List<uint>(client.ServerGroups);
            this.ChannelId = client.ChannelId;
            this.Connected = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        /// <value>The client id.</value>
        public uint ClientId { get; set; }

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
        /// Gets or sets the server groups.
        /// </summary>
        /// <value>The server groups.</value>
        public List<uint> ServerGroups { get; set; }

        /// <summary>
        /// Gets or sets the channel id.
        /// </summary>
        /// <value>
        /// The channel id.
        /// </value>
        public uint? ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the connected.
        /// </summary>
        /// <value>
        /// The connected.
        /// </value>
        public DateTime? Connected { get; set; }
    }
}