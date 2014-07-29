namespace DirkSarodnick.TS3_Bot.Core.Repository.Base
{
    using System;
    using Manager.Connection;
    using TS3QueryLib.Core.Server;
    using TS3QueryLib.Core.Server.Responses;

    /// <summary>
    /// Defines the DefaultData class.
    /// </summary>
    public class DefaultData : IDisposable
    {
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultData"/> class.
        /// </summary>
        public DefaultData(DataRepository repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="DefaultData"/> is reclaimed by garbage collection.
        /// </summary>
        ~DefaultData()
        {
            Dispose();
        }

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>The container.</value>
        public DataRepository Repository { get; private set; }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public TeamSpeakConnection Connection
        {
            get
            {
                return Repository.Connection;
            }
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>The container.</value>
        public DataContainer Container
        {
            get
            {
                return Repository.Container;
            }
        }

        /// <summary>
        /// Gets the query runner.
        /// </summary>
        /// <value>The query runner.</value>
        protected QueryRunner QueryRunner
        {
            get { return Connection.CredentialEntity.QueryRunner; }
        }

        /// <summary>
        /// Returns the dynamic query runner
        /// </summary>
        /// <param name="serverId">The server id.</param>
        /// <returns>The dynamic query runner</returns>
        protected QueryRunner DynamicQueryRunner(uint serverId)
        {
            return Connection.GetDynamicQueryRunner(serverId);
        }

        /// <summary>
        /// Gets the self.
        /// </summary>
        /// <value>The self.</value>
        protected WhoAmIResponse Self
        {
            get { return Connection.CredentialEntity.Self; }
        }

        /// <summary>
        /// Gets the self worker.
        /// </summary>
        /// <value>The self worker.</value>
        protected WhoAmIResponse SelfWorker
        {
            get { return Connection.CredentialEntity.SelfWorker; }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if(disposed) return;
            disposed = true;

            Repository.Dispose();
            Connection.Dispose();
            Container.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}