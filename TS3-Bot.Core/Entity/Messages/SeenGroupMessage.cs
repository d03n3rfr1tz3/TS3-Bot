namespace DirkSarodnick.TS3_Bot.Core.Entity.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TS3QueryLib.Core.Server.Notification.EventArgs;

    /// <summary>
    /// Defines SeenGroupMessage class.
    /// </summary>
    public class SeenGroupMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeenGroupMessage"/> class.
        /// </summary>
        public SeenGroupMessage()
        {
            ClientDatabaseIds = new List<uint>();
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>The command.</value>
        protected override string Command { get { return this.Repository.Settings.Control.SeenGroup.Command; } }

        /// <summary>
        /// Gets or sets the server group.
        /// </summary>
        /// <value>
        /// The server group.
        /// </value>
        public uint ServerGroup { get; set; }

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
                   parameters[0].Equals(Command, StringComparison.InvariantCultureIgnoreCase) &&
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

            uint serverGroup;
            if (uint.TryParse(parameters[1], out serverGroup))
            {
                ServerGroup = serverGroup;
                ClientDatabaseIds = Repository.Client.GetClientsByServerGroup(serverGroup).ToList();
                if (!ClientDatabaseIds.Any())
                {
                    ErrorMessage = string.Format("Could not find server group id '{0}'", serverGroup);
                }
            }
            else
            {
                ErrorMessage = string.Format("Could not find server group id '{0}'", parameters[1]);
            }
        }
    }
}