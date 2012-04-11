namespace DirkSarodnick.TS3_Bot.Core.Manager.Connection
{
    using System;
    using Entity;
    using TS3QueryLib.Core.Query;

    /// <summary>
    /// Defines the TeamSpeakConnection class.
    /// </summary>
    public class TeamSpeakConnection : IDisposable
    {
        private readonly object lockCredentialEntity = new object();
        private bool disposed;
        private CredentialManager credentialEntity;
        private QueryRunner dynamicQueryRunner;
        private SyncTcpDispatcher dynamicTcpDispatcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamSpeakConnection"/> class.
        /// </summary>
        /// <param name="instanceBase">The instance base.</param>
        public TeamSpeakConnection(BotInstance instanceBase)
        {
            BotInstance = instanceBase;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="TeamSpeakConnection"/> is reclaimed by garbage collection.
        /// </summary>
        ~TeamSpeakConnection()
        {
            Dispose();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="TeamSpeakConnection"/> is busy.
        /// </summary>
        /// <value><c>true</c> if busy; otherwise, <c>false</c>.</value>
        public bool Busy { get; set; }

        /// <summary>
        /// Gets or sets the bot instance.
        /// </summary>
        /// <value>The bot instance.</value>
        public BotInstance BotInstance { get; private set; }

        /// <summary>
        /// Gets the credential entity.
        /// </summary>
        /// <value>The credential entity.</value>
        public CredentialManager CredentialEntity
        {
            get
            {
                lock (lockCredentialEntity)
                {
                    if (credentialEntity == null)
                    {
                        credentialEntity = new CredentialManager(BotInstance);
                        return credentialEntity;
                    }

                    if (credentialEntity.Fault)
                    {
                        credentialEntity.Dispose();
                        credentialEntity = new CredentialManager(BotInstance);
                        return credentialEntity;
                    }

                    return credentialEntity;
                }
            }
        }

        /// <summary>
        /// Gets the dynamic query runner.
        /// </summary>
        /// <value>The dynamic query runner.</value>
        public QueryRunner DynamicQueryRunner
        {
            get
            {
                if (dynamicQueryRunner == null || dynamicQueryRunner.IsDisposed)
                {
                    dynamicQueryRunner = new QueryRunner(DynamicTcpDispatcher);
                    dynamicQueryRunner.Login(BotInstance.Settings.TeamSpeak.Username, BotInstance.Settings.TeamSpeak.Password);
                }

                return dynamicQueryRunner;
            }

            set
            {
                dynamicQueryRunner = value;
            }
        }

        /// <summary>
        /// Gets or sets the dynamic TCP dispatcher.
        /// </summary>
        /// <value>The dynamic TCP dispatcher.</value>
        public SyncTcpDispatcher DynamicTcpDispatcher
        {
            get
            {
                if (dynamicTcpDispatcher == null || dynamicTcpDispatcher.IsDisposed)
                {
                    dynamicTcpDispatcher = new SyncTcpDispatcher(BotInstance.Settings);
                    dynamicTcpDispatcher.Connect();
                }

                return dynamicTcpDispatcher;
            }

            set
            {
                dynamicTcpDispatcher = value;
            }
        }

        /// <summary>
        /// Returns the dynamic query runner
        /// </summary>
        /// <param name="serverId">The server id.</param>
        /// <returns>The dynamic query runner</returns>
        public QueryRunner GetDynamicQueryRunner(uint serverId)
        {
            DynamicQueryRunner.SelectVirtualServerById(serverId);
            return DynamicQueryRunner;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            CredentialEntity.Dispose();
            if (DynamicTcpDispatcher.IsConnected && !DynamicTcpDispatcher.IsDisposed)
            {
                DynamicQueryRunner.Logout();
                DynamicQueryRunner.Quit();
                DynamicTcpDispatcher.Disconnect();
            }

            GC.SuppressFinalize(this);
        }
    }
}