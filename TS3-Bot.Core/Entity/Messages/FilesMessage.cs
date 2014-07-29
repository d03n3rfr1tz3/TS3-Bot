namespace DirkSarodnick.TS3_Bot.Core.Entity.Messages
{
    /// <summary>
    /// Defines the FilesMessage class.
    /// </summary>
    public class FilesMessage : Message
    {
        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>The command.</value>
        protected override string Command { get { return this.Repository.Settings.Control.Files.Command; } }
    }
}