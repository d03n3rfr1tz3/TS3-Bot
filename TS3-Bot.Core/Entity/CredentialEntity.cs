namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using TS3QueryLib.Core.Server;
    using TS3QueryLib.Core.Server.Responses;

    /// <summary>
    /// Defines the CredentialEntity class.
    /// </summary>
    public class CredentialEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialEntity"/> class.
        /// </summary>
        public CredentialEntity(BotInstance botInstance)
        {
            BotInstance = botInstance;
        }

        /// <summary>
        /// Gets or sets the bot instance.
        /// </summary>
        /// <value>The bot instance.</value>
        public BotInstance BotInstance { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether [notification fault].
        /// </summary>
        /// <value><c>true</c> if [notification fault]; otherwise, <c>false</c>.</value>
        public bool NotificationFault { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="CredentialEntity"/> is fault.
        /// </summary>
        /// <value><c>true</c> if fault; otherwise, <c>false</c>.</value>
        public virtual bool Fault { get { return false; } }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CredentialEntity"/> is initialized.
        /// </summary>
        /// <value><c>true</c> if initialized; otherwise, <c>false</c>.</value>
        public bool Initialized { get; set; }

        /// <summary>
        /// Gets or sets the self.
        /// </summary>
        /// <value>The self.</value>
        public WhoAmIResponse Self { get; set; }

        /// <summary>
        /// Gets or sets the self worker.
        /// </summary>
        /// <value>The self worker.</value>
        public WhoAmIResponse SelfWorker { get; set; }

        /// <summary>
        /// Gets or sets the query runner.
        /// </summary>
        /// <value>The query runner.</value>
        public QueryRunner QueryRunner { get; set; }

        /// <summary>
        /// Gets or sets the TCP dispatcher.
        /// </summary>
        /// <value>The TCP dispatcher.</value>
        public SyncTcpDispatcher TcpDispatcher { get; set; }

        /// <summary>
        /// Gets or sets the worker query runner.
        /// </summary>
        /// <value>The worker query runner.</value>
        public QueryRunner WorkerQueryRunner { get; set; }

        /// <summary>
        /// Gets or sets the worker TCP dispatcher.
        /// </summary>
        /// <value>The worker TCP dispatcher.</value>
        public SyncTcpDispatcher WorkerTcpDispatcher { get; set; }

        /// <summary>
        /// Gets or sets the notification query runner.
        /// </summary>
        /// <value>The notification query runner.</value>
        public QueryRunner NotificationQueryRunner { get; set; }

        /// <summary>
        /// Gets or sets the notification TCP dispatcher.
        /// </summary>
        /// <value>The notification TCP dispatcher.</value>
        public AsyncTcpDispatcher NotificationTcpDispatcher { get; set; }
    }
}