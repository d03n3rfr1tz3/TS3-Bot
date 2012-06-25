namespace DirkSarodnick.TS3_Bot.Core.Entity.Messages
{
    using Repository;
    using TS3QueryLib.Core.Server.Notification.EventArgs;

    public class Message
    {
        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>The command.</value>
        protected virtual string Command { get { return string.Empty; } }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>The repository.</value>
        public DataRepository Repository { get; set; }

        /// <summary>
        /// Gets or sets the sender client id.
        /// </summary>
        /// <value>The sender client id.</value>
        public uint SenderClientId { get; set; }

        /// <summary>
        /// Validates the message
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>True or False</returns>
        public virtual bool Validate(string[] parameters)
        {
            return parameters.Length > 0 &&
                   parameters[0].StartsWith(Command);
        }

        /// <summary>
        /// Initializes the message
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        /// <param name="parameters">The parameters.</param>
        public virtual void Initialize(MessageReceivedEventArgs e, string[] parameters)
        {
            SenderClientId = e.InvokerClientId;
        }
    }
}