namespace DirkSarodnick.TS3_Bot.Core
{
    using System;
    using Manager.Connection;
    using Manager.Features.Base;
    using Repository;
    using Settings;
    using TS3QueryLib.Core.CommandHandling;
    using TS3QueryLib.Core.Query.HelperClasses;

    /// <summary>
    /// Defines the BotInstanceBase class.
    /// </summary>
    public partial class BotInstance : IDisposable
    {
        private bool disposed;

        protected int SlowTickCounter;
        protected int KeepTickCounter;

        /// <summary>
        /// Initializes a new instance of the <see cref="BotInstance"/> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public BotInstance(InstanceSettings settings)
        {
            Settings = settings;
            Connection = new TeamSpeakConnection(this);
            Repository = new DataRepository(Connection, Settings);
            ManagerFactory = new ManagerFactory(Repository);
        }

        ~BotInstance()
        {
            Dispose();
        }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public InstanceSettings Settings { get; private set; }

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public TeamSpeakConnection Connection { get; private set; }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>The repository.</value>
        public DataRepository Repository { get; private set; }

        /// <summary>
        /// Gets or sets the manager factory.
        /// </summary>
        /// <value>The manager factory.</value>
        protected ManagerFactory ManagerFactory { get; private set; }

        /// <summary>
        /// Changes the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void ChangeSettings(InstanceSettings settings)
        {
            Repository.Connection.CredentialEntity.WorkerQueryRunner.AddLogEntry(new LogEntryLight(LogLevel.Info, string.Format("TS3-Bot '{0}' Configuration refreshing.", Repository.Settings.Global.BotNickname)));

            var resetConnection = Settings.TeamSpeak.Hash != settings.TeamSpeak.Hash;
            Settings.ApplySettings(settings);

            if (resetConnection)
            {
                Repository.Connection.CredentialEntity.WorkerQueryRunner.AddLogEntry(new LogEntryLight(LogLevel.Info, "TS3-Bot with new Connection recognized."));
                Repository.Connection.CredentialEntity.Dispose();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            Connection.Dispose();
            Repository.Dispose();
            ManagerFactory.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}