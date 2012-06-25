namespace DirkSarodnick.TS3_Bot.Core.Service
{
    using System;
    using System.Collections.Generic;
    using Entity;
    using Settings;
    using TS3QueryLib.Core.Server;
    using TS3QueryLib.Core.Server.Entities;

    public class DataService
    {
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataService"/> class.
        /// </summary>
        public DataService(InstanceSettings settings)
        {
            this.Settings = settings;
            this.QueryRunner = new QueryRunner(new SyncTcpDispatcher(settings));
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="DataService"/> is reclaimed by garbage collection.
        /// </summary>
        ~DataService()
        {
            Dispose();
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        protected InstanceSettings Settings { get; private set; }

        /// <summary>
        /// Gets the query runner.
        /// </summary>
        protected QueryRunner QueryRunner { get; private set; }

        /// <summary>
        /// Gets the servers.
        /// </summary>
        /// <returns>The Server list.</returns>
        public IEnumerable<ServerListItem> GetServers()
        {
            return QueryRunner.GetServerList();
        }

        /// <summary>
        /// Gets the channels.
        /// </summary>
        /// <returns>The Channel list.</returns>
        public IEnumerable<ChannelListEntry> GetChannels()
        {
            return QueryRunner.GetChannelList();
        }

        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <returns>The Client list.</returns>
        public IEnumerable<ClientDbEntry> GetClients()
        {
            return QueryRunner.GetClientDatabaseList();
        }

        /// <summary>
        /// Gets the current clients.
        /// </summary>
        /// <returns>The Client list of currently online Clients.</returns>
        public IEnumerable<ClientListEntry> GetCurrentClients()
        {
            return QueryRunner.GetClientList(true);
        }

        /// <summary>
        /// Gets the server groups.
        /// </summary>
        /// <returns>The server group list.</returns>
        public IEnumerable<ServerGroup> GetServerGroups()
        {
            return QueryRunner.GetServerGroupList();
        }

        /// <summary>
        /// Gets the channel groups.
        /// </summary>
        /// <returns>The channel group list.</returns>
        public IEnumerable<ChannelGroup> GetChannelGroups()
        {
            return QueryRunner.GetChannelGroupList();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            GC.SuppressFinalize(this);
        }
    }
}