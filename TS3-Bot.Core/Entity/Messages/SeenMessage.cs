﻿namespace DirkSarodnick.TS3_Bot.Core.Entity.Messages
{
    using System.Collections.Generic;
    using System.Linq;
    using TS3QueryLib.Core.Query.Notification.EventArgs;

    /// <summary>
    /// Defines SeenMessage class.
    /// </summary>
    public class SeenMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeenMessage"/> class.
        /// </summary>
        public SeenMessage()
        {
            ClientDatabaseIds = new List<uint>();
        }

        protected override string Command { get { return "!seen"; } }

        /// <summary>
        /// Gets or sets the client database ids.
        /// </summary>
        /// <value>The client database ids.</value>
        public List<uint> ClientDatabaseIds { get; set; }

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
                   parameters[0].StartsWith(Command) &&
                   !string.IsNullOrEmpty(parameters[1]);
        }

        /// <summary>
        /// Initializes the message
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        /// <param name="parameters">The parameters.</param>
        public override void Initialize(MessageReceivedEventArgs e, string[] parameters)
        {
            SenderClientId = e.InvokerClientId;
            var nickname = string.Join(string.Empty, parameters, 1, parameters.Length - 1);

            uint clientDatabaseId;
            if (uint.TryParse(nickname, out clientDatabaseId))
            {
                ClientDatabaseIds.Add(clientDatabaseId);
            }
            else
            {
                var clientIds = Repository.Client.GetClientsFromDatabase()
                    .Where(m => m.NickName.ToLower().Contains(nickname.ToLower()))
                    .Select(m => m.DatabaseId);
                if (clientIds.Any())
                {
                    ClientDatabaseIds = clientIds.ToList();
                }
                else
                {
                    ErrorMessage = string.Format("Could not find nickname or database id '{0}'", nickname);
                }
            }
        }
    }
}