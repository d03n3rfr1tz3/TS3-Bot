namespace DirkSarodnick.TS3_Bot.Core.Manager.Connection
{
    using System;
    using Entity;
    using TS3QueryLib.Core.CommandHandling;
    using TS3QueryLib.Core.Server;
    using TS3QueryLib.Core.Server.Entities;

    /// <summary>
    /// Defines the CredentialManager class.
    /// </summary>
    public class CredentialManager : CredentialEntity, IDisposable
    {
        private bool disposed;

        #region Constructor & Deconstructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialManager"/> class.
        /// </summary>
        public CredentialManager(BotInstance botInstance) : base(botInstance)
        {
            InitCredential();
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="CredentialEntity"/> is reclaimed by garbage collection.
        /// </summary>
        ~CredentialManager()
        {
            Dispose();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this <see cref="CredentialEntity"/> is fault.
        /// </summary>
        /// <value><c>true</c> if fault; otherwise, <c>false</c>.</value>
        public override bool Fault
        {
            get
            {
                return (Initialized && (!TcpDispatcher.IsConnected || !WorkerTcpDispatcher.IsConnected)) ||
                       (TcpDispatcher.IsDisposed || WorkerTcpDispatcher.IsDisposed) ||
                       NotificationFault || disposed;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            if (WorkerQueryRunner != null)
                WorkerQueryRunner.AddLogEntry(new LogEntryLight(LogLevel.Info, string.Format("TS3-Bot '{0}' disconnected.", BotInstance.Settings.Global.BotNickname)));

            if (TcpDispatcher != null && TcpDispatcher.IsConnected && !TcpDispatcher.IsDisposed && QueryRunner != null)
            {
                QueryRunner.Logout();
                QueryRunner.Quit();
                TcpDispatcher.Disconnect();
            }
            if (WorkerTcpDispatcher != null && WorkerTcpDispatcher.IsConnected && !WorkerTcpDispatcher.IsDisposed && WorkerQueryRunner != null)
            {
                WorkerQueryRunner.Logout();
                WorkerQueryRunner.Quit();
                WorkerTcpDispatcher.Disconnect();
            }
            if (NotificationTcpDispatcher != null && NotificationTcpDispatcher.IsConnected && !NotificationTcpDispatcher.IsDisposed && NotificationQueryRunner != null)
            {
                NotificationQueryRunner.Logout();
                NotificationQueryRunner.Quit();
                NotificationTcpDispatcher.Disconnect();
            }

            GC.SuppressFinalize(this);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Inits the credential.
        /// </summary>
        private void InitCredential()
        {
            TcpDispatcher = new SyncTcpDispatcher(BotInstance.Settings);
            QueryRunner = new QueryRunner(TcpDispatcher);
            QueryRunner.Login(BotInstance.Settings.TeamSpeak.Username, BotInstance.Settings.TeamSpeak.Password);
            if (BotInstance.Settings.TeamSpeak.InstancePort.GetValueOrDefault() > 0)
                QueryRunner.SelectVirtualServerByPort(BotInstance.Settings.TeamSpeak.InstancePort.GetValueOrDefault());
            else
                QueryRunner.SelectVirtualServerById(BotInstance.Settings.TeamSpeak.Instance.GetValueOrDefault());
            Self = QueryRunner.SendWhoAmI();

            WorkerTcpDispatcher = new SyncTcpDispatcher(BotInstance.Settings);
            WorkerQueryRunner = new QueryRunner(WorkerTcpDispatcher);
            WorkerQueryRunner.Login(BotInstance.Settings.TeamSpeak.Username, BotInstance.Settings.TeamSpeak.Password);

            if (BotInstance.Settings.TeamSpeak.InstancePort.GetValueOrDefault() > 0)
                WorkerQueryRunner.SelectVirtualServerByPort(BotInstance.Settings.TeamSpeak.InstancePort.GetValueOrDefault());
            else
                WorkerQueryRunner.SelectVirtualServerById(BotInstance.Settings.TeamSpeak.Instance.GetValueOrDefault());
            
            WorkerQueryRunner.UpdateCurrentQueryClient(new ClientModification { Nickname = BotInstance.Settings.Global.BotNickname });
            SelfWorker = WorkerQueryRunner.SendWhoAmI();
            WorkerQueryRunner.AddLogEntry(new LogEntryLight(LogLevel.Info, string.Format("TS3-Bot '{0}' connected.", BotInstance.Settings.Global.BotNickname)));

            NotificationTcpDispatcher = new AsyncTcpDispatcher(BotInstance.Settings);
            NotificationQueryRunner = new QueryRunner(NotificationTcpDispatcher);

            NotificationTcpDispatcher.ServerClosedConnection += notificationDispatcher_ServerClosedConnection;
            NotificationTcpDispatcher.ReadyForSendingCommands += notificationDispatcher_ReadyForSendingCommands;
            NotificationQueryRunner.Notifications.ClientJoined += BotInstance.Notifications_ClientJoined;
            NotificationQueryRunner.Notifications.ClientMoved += BotInstance.Notifications_ClientMoved;
            NotificationQueryRunner.Notifications.ClientMoveForced += BotInstance.Notifications_ClientMoveForced;
            NotificationQueryRunner.Notifications.ClientDisconnect += BotInstance.Notifications_ClientDisconnect;
            NotificationQueryRunner.Notifications.ClientConnectionLost += BotInstance.Notifications_ClientConnectionLost;
            NotificationQueryRunner.Notifications.ServerMessageReceived += BotInstance.Notifications_MessageReceived;
            NotificationQueryRunner.Notifications.ChannelMessageReceived += BotInstance.Notifications_MessageReceived;
            NotificationQueryRunner.Notifications.ClientMessageReceived += BotInstance.Notifications_MessageReceived;

            TcpDispatcher.Connect();
            WorkerTcpDispatcher.Connect();
            NotificationTcpDispatcher.Connect();
            Initialized = true;
        }

        /// <summary>
        /// Inits the notification.
        /// </summary>
        private void InitNotification()
        {
            NotificationQueryRunner.Login(BotInstance.Settings.TeamSpeak.Username, BotInstance.Settings.TeamSpeak.Password);
            if (BotInstance.Settings.TeamSpeak.InstancePort > 0)
                NotificationQueryRunner.SelectVirtualServerByPort(BotInstance.Settings.TeamSpeak.InstancePort.GetValueOrDefault());
            else
                NotificationQueryRunner.SelectVirtualServerById(BotInstance.Settings.TeamSpeak.Instance.GetValueOrDefault());

            NotificationQueryRunner.RegisterForNotifications(ServerNotifyRegisterEvent.Server);
            NotificationQueryRunner.RegisterForNotifications(ServerNotifyRegisterEvent.Channel, BotInstance.Settings.Sticky.Channel);
            NotificationQueryRunner.RegisterForNotifications(ServerNotifyRegisterEvent.TextChannel);
            NotificationQueryRunner.RegisterForNotifications(ServerNotifyRegisterEvent.TextPrivate);
            NotificationQueryRunner.RegisterForNotifications(ServerNotifyRegisterEvent.TextServer);
        }

        /// <summary>
        /// Handles the ReadyForSendingCommands event of the Notification connection.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void notificationDispatcher_ReadyForSendingCommands(object sender, EventArgs e)
        {
            InitNotification();
        }

        /// <summary>
        /// Handles the ServerClosedConnection event of the Notification connection.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void notificationDispatcher_ServerClosedConnection(object sender, EventArgs e)
        {
            NotificationFault = true;
        }

        #endregion
    }
}