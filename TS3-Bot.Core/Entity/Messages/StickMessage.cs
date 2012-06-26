namespace DirkSarodnick.TS3_Bot.Core.Entity.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TS3QueryLib.Core.Server.Notification.EventArgs;

    /// <summary>
    /// Defines StickMessage class.
    /// </summary>
    public class StickMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StickMessage"/> class.
        /// </summary>
        public StickMessage()
        {
            ClientDatabaseIds = new List<uint>();
            StickTime = 60;
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>The command.</value>
        protected override string Command { get { return "!stick"; } }

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
        /// Gets or sets the stick time.
        /// </summary>
        /// <value>
        /// The stick time.
        /// </value>
        public uint StickTime { get; set; }

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
                var clientIds = Repository.Client.GetClientsFromDatabase()
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
            if (uint.TryParse(parameters[2], out channelId))
            {
                ChannelId = channelId;
            }

            uint stickTime;
            if (parameters.Length > 2 && uint.TryParse(parameters[2], out stickTime))
            {
                StickTime = stickTime;
            }
        }
    }
}
