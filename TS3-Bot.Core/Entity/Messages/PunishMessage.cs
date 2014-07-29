namespace DirkSarodnick.TS3_Bot.Core.Entity.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TS3QueryLib.Core.Server.Notification.EventArgs;

    /// <summary>
    /// Defines PunishMessage class.
    /// </summary>
    public class PunishMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PunishMessage"/> class.
        /// </summary>
        public PunishMessage()
        {
            ClientDatabaseIds = new List<uint>();
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>The command.</value>
        protected override string Command { get { return this.Repository.Settings.Control.Punish.Command; } }

        /// <summary>
        /// Gets or sets the client database ids.
        /// </summary>
        /// <value>The client database ids.</value>
        public List<uint> ClientDatabaseIds { get; set; }

        /// <summary>
        /// Gets or sets the channel id.
        /// </summary>
        /// <value>
        /// The channel id.
        /// </value>
        public uint ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Validates the message
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public override bool Validate(string[] parameters)
        {
            return parameters.Length > 1 &&
                   parameters[0].StartsWith(Command, StringComparison.InvariantCultureIgnoreCase) &&
                   !string.IsNullOrEmpty(parameters[1]);
        }

        /// <summary>
        /// Initializes the message
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        /// <param name="parameters">The parameters.</param>
        public override void Initialize(MessageReceivedEventArgs e, string[] parameters)
        {
            SenderClientId = e.InvokerClientId;
            var nickname = parameters[1];

            uint clientDatabaseId;
            if (uint.TryParse(nickname, out clientDatabaseId))
            {
                ClientDatabaseIds.Add(clientDatabaseId);
            }
            else
            {
                var clientIds = Repository.Client.GetClientsFromDatabase().Values
                    .Where(m => m.NickName.ToLower().Contains(nickname.ToLower()))
                    .Select(m => m.DatabaseId).ToList();
                if (clientIds.Any())
                {
                    ClientDatabaseIds = clientIds;
                }
                else
                {
                    ErrorMessage = string.Format("Could not find nickname or database id '{0}'", nickname);
                }
            }

            uint channelId;
            if (parameters.Length > 2 && uint.TryParse(parameters[2], out channelId))
            {
                ChannelId = channelId;
            }
        }
    }
}
