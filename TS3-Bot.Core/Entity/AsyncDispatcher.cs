namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using Settings;

    /// <summary>
    /// Defines the AsyncTcpDispatcher class.
    /// </summary>
    public class AsyncTcpDispatcher : TS3QueryLib.Core.AsyncTcpDispatcher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncTcpDispatcher"/> class.
        /// </summary>
        public AsyncTcpDispatcher(InstanceSettings settings)
            : base(settings.TeamSpeak.Host, settings.TeamSpeak.QueryPort)
        {
        }
    }
}