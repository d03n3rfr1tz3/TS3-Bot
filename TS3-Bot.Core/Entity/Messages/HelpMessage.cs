namespace DirkSarodnick.TS3_Bot.Core.Entity.Messages
{
    /// <summary>
    /// Defines HelpMessage class.
    /// </summary>
    public class HelpMessage : Message
    {
        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>The command.</value>
        protected override string Command { get { return this.Repository.Settings.Control.Help.Command; } }
    }
}