namespace DirkSarodnick.TS3_Bot.Core.Entity.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TS3QueryLib.Core.Server.Notification.EventArgs;

    /// <summary>
    /// Defines SelfGroupRemoveMessage class.
    /// </summary>
    public class SelfGroupRemoveMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfGroupRemoveMessage"/> class.
        /// </summary>
        public SelfGroupRemoveMessage()
        {
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>The command.</value>
        protected override string Command { get { return this.Repository.Settings.Control.SelfGroup.UndoCommand; } }

        /// <summary>
        /// Gets the not command.
        /// </summary>
        /// <value>
        /// The not command.
        /// </value>
        protected string NotCommand { get { return this.Repository.Settings.Control.SelfGroup.Command; } }

        /// <summary>
        /// Gets or sets the server group ids.
        /// </summary>
        /// <value>
        /// The server group ids.
        /// </value>
        public List<uint> ServerGroupIds { get; set; }

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
                   string.Join(" ", parameters).StartsWith(Command, StringComparison.InvariantCultureIgnoreCase) &&
                   !string.IsNullOrEmpty(parameters.Last());
        }

        /// <summary>
        /// Initializes the message
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        /// <param name="parameters">The parameters.</param>
        public override void Initialize(MessageReceivedEventArgs e, string[] parameters)
        {
            SenderClientId = e.InvokerClientId;
            var serverGroup = parameters.Last();

            uint serverGroupId;
            if (uint.TryParse(serverGroup, out serverGroupId))
            {
                if (this.Repository.Settings.Control.SelfGroup.AllowedServerGroups.Contains(serverGroupId))
                {
                    this.ServerGroupIds.Add(serverGroupId);
                }
                else
                {
                    ErrorMessage = string.Format("The server group '{0}' is not allowed.", serverGroup);
                }
            }
            else
            {
                var serverGroups = Repository.Server.GetServerGroups().Where(m => m.Name.Equals(serverGroup, StringComparison.InvariantCultureIgnoreCase)).Select(m => m.Id).ToList();
                if (serverGroups.Any())
                {
                    if (serverGroups.All(s => this.Repository.Settings.Control.SelfGroup.AllowedServerGroups.Contains(s)))
                    {
                        this.ServerGroupIds = serverGroups;
                    }
                    else
                    {
                        ErrorMessage = string.Format("The server group '{0}' is not allowed.", serverGroup);
                    }
                }
                else
                {
                    ErrorMessage = string.Format("Could not find server group by name or id '{0}'", serverGroup);
                }
            }
        }
    }
}
