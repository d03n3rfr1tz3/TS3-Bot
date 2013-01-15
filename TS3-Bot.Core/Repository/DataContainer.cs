namespace DirkSarodnick.TS3_Bot.Core.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entity;
    using Helper;
    using TS3QueryLib.Core.Server.Entities;
    using TS3QueryLib.Core.Server.Responses;

    public class DataContainer : IDisposable
    {
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContainer"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public DataContainer(string name)
        {
            DataMigrator.MigrateAll(name);
        }

        #region Specified Data

        private BotDatabaseEntities database;
        internal BotDatabaseEntities Database
        {
            get
            {
                lock (lockDatabase)
                {
                    if (database == null)
                    {
                        database = new BotDatabaseEntities();
                        database.Connection.Open();
                    }

                    return this.database;
                }
            }
        }

        internal Dictionary<uint, ClientServerGroupList> ClientServerGroupList = new Dictionary<uint, ClientServerGroupList>();
        internal List<ClientWarningEntity> ClientWarningList = new List<ClientWarningEntity>();

        #endregion

        #region Basic Data

        internal DateTime Now;

        internal int LastConnectionWaiting = 1;
        internal DateTime LastConnectionError = DateTime.UtcNow;

        internal List<ClientListEntry> ClientList;
        internal Dictionary<uint, ClientInfoResponse> ClientInfoList = new Dictionary<uint, ClientInfoResponse>();
        internal List<ChannelListEntry> ChannelList;
        internal Dictionary<uint, ChannelInfoResponse> ChannelInfoList = new Dictionary<uint, ChannelInfoResponse>();
        internal Dictionary<uint, ClientDbEntry> ClientDatabaseList = new Dictionary<uint, ClientDbEntry>();
        internal List<FileEntity> FileList = new List<FileEntity>();
        internal List<ComplainListEntry> CompliantList;
        internal List<ServerListItem> ServerList;
        internal Dictionary<Guid, DateTime> LastIntervalList = new Dictionary<Guid, DateTime>();

        #endregion

        #region Locks

        internal readonly object lockNow = new object();
        internal readonly object lockDatabase = new object();
        internal readonly object lockGetClientInfo = new object();
        internal readonly object lockGetClientList = new object();
        internal readonly object lockGetClientsFromDatabase = new object();
        internal readonly object lockGetClientFromDatabase = new object();
        internal readonly object lockGetRawClientsFromDatabase = new object();
        internal readonly object lockGetChannelInfo = new object();
        internal readonly object lockGetChannelList = new object();
        internal readonly object lockGetClientServerGroups = new object();
        internal readonly object lockClientWarningList = new object();

        internal readonly object lockClientLastChannelList = new object();
        internal readonly object lockStickyClientList = new object();
        internal readonly object lockVotedClientList = new object();
        internal readonly object lockSeenClientList = new object();
        internal readonly object lockModeratedClientList = new object();
        internal readonly object lockTimeClientList = new object();
        internal readonly object lockPreviousServerGroupsList = new object();

        internal readonly object lockFileList = new object();
        internal readonly object lockGetCompliantList = new object();
        internal readonly object lockGetServerList = new object();
        internal readonly object lockGetServerGroup = new object();
        internal readonly object lockLastEventList = new object();

        #endregion

        #region Public Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Clean()
        {
            lock (lockNow) Now = default(DateTime);
            lock (lockGetClientList) ClientList = null;
            lock (lockGetChannelList) ChannelList = null;
            lock (lockGetCompliantList) CompliantList = null;
            lock (lockGetServerList) ServerList = null;
            lock (lockGetClientInfo) ClientInfoList.Clear();
            lock (lockGetChannelInfo) ChannelInfoList.Clear();

            lock (lockClientWarningList) ClientWarningList.RemoveAll(m => m.Creation.AddMinutes(5) < DateTime.UtcNow);

            lock (lockDatabase)
            {
                Database.Sticky.ToList().Where(m => m.Creation.AddMinutes(m.StickTime) < DateTime.UtcNow).ForEach(m => Database.Sticky.DeleteObject(m));
                Database.Vote.ToList().Where(m => m.Creation.AddHours(1) < DateTime.UtcNow).ForEach(m => Database.Vote.DeleteObject(m));
                Database.SaveChanges();
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void SlowClean()
        {
            lock (lockGetClientsFromDatabase) ClientDatabaseList.Clear();
        }

        #endregion

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            Database.Dispose();
            ClientList = null;
            ClientInfoList = null;
            ChannelList = null;
            ChannelInfoList = null;
            ClientDatabaseList = null;
            FileList = null;
            CompliantList = null;
            ServerList = null;
            LastIntervalList = null;
            ClientServerGroupList = null;
            ClientWarningList = null;

            GC.SuppressFinalize(this);
        }
    }
}