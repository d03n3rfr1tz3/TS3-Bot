namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using Settings;

    /// <summary>
    /// Defines the SyncDispatcher class.
    /// </summary>
    public class SyncTcpDispatcher : TS3QueryLib.Core.SyncTcpDispatcher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SyncTcpDispatcher"/> class.
        /// </summary>
        public SyncTcpDispatcher(InstanceSettings settings)
            : base(settings.TeamSpeak.Host, settings.TeamSpeak.QueryPort)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncTcpDispatcher"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        public SyncTcpDispatcher(string host, ushort port)
            : base (host, port)
        {
        }
    }
}