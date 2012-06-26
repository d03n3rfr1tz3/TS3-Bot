namespace DirkSarodnick.TS3_Bot.Core.Entity.Messages
{
    using System;
    using System.Collections.Generic;
    using TS3QueryLib.Core.Server.Notification.EventArgs;

    /// <summary>
    /// Defines SeenModeratorMessage class.
    /// </summary>
    public class SeenModeratorMessage : SeenGroupMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeenModeratorMessage"/> class.
        /// </summary>
        public SeenModeratorMessage()
        {
            ClientDatabaseIds = new List<uint>();
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>The command.</value>
        protected override string Command { get { return "!seenmods"; } }

        /// <summary>
        /// Validates the message
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public override bool Validate(string[] parameters)
        {
            return parameters.Length > 0 &&
                   parameters[0].StartsWith(Command, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Initializes the message
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        /// <param name="parameters">The parameters.</param>
        public override void Initialize(MessageReceivedEventArgs e, string[] parameters)
        {
            SenderClientId = e.InvokerClientId;
            ServerGroup = Repository.Settings.Control.SeenModerator.ServerGroup;
            ClientDatabaseIds = Repository.Client.GetClientsByServerGroup(Repository.Settings.Control.SeenModerator.ServerGroup);
        }
    }
}