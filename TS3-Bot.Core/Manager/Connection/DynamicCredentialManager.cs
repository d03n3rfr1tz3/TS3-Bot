namespace DirkSarodnick.TS3_Bot.Core.Manager.Connection
{
    using System;
    using Entity;
    using Repository;
    using TS3QueryLib.Core.Query;

    /// <summary>
    /// Defines the DynamicCredentialContainer class.
    /// </summary>
    class DynamicCredentialManager : IDisposable
    {
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicCredentialManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public DynamicCredentialManager(DataRepository repository)
        {
            Repository = repository;
            Repository.Connection.DynamicTcpDispatcher = new SyncTcpDispatcher(Repository.Settings);
            Repository.Connection.DynamicTcpDispatcher.Connect();
            Repository.Connection.DynamicQueryRunner = new QueryRunner(Repository.Connection.DynamicTcpDispatcher);
            Repository.Connection.DynamicQueryRunner.Login(Repository.Settings.TeamSpeak.Username, Repository.Settings.TeamSpeak.Password);
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="DynamicCredentialManager"/> is reclaimed by garbage collection.
        /// </summary>
        ~DynamicCredentialManager()
        {
            Dispose();
        }

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        private DataRepository Repository { get; set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                Repository.Connection.DynamicQueryRunner.Logout();
                Repository.Connection.DynamicQueryRunner.Quit();
                Repository.Connection.DynamicTcpDispatcher.Disconnect();
            }

            GC.SuppressFinalize(this);
        }
    }
}