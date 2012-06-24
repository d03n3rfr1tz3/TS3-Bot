namespace DirkSarodnick.TS3_Bot.Core.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entity;
    using Microsoft.Isam.Esent.Collections.Generic;
    using TS3QueryLib.Core.Query.HelperClasses;
    using TS3QueryLib.Core.Query.Responses;

    public class DataContainer : IDisposable
    {
        private bool disposed;
        private readonly string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContainer"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public DataContainer(string name)
        {
            this.name = name;
        }

        #region Specified Data

        internal Dictionary<uint, ClientServerGroupList> ClientServerGroupList = new Dictionary<uint, ClientServerGroupList>();
        internal List<ClientWarningEntity> ClientWarningList = new List<ClientWarningEntity>();

        // Persistent Dictionaries
        private PersistentDictionary<uint, uint> clientLastChannelList;
        private PersistentDictionary<Guid, StickyClientEntity> stickyClientList;
        private PersistentDictionary<Guid, VotedClientEntity> votedClientList;
        private PersistentDictionary<uint, DateTime> clientLastSeen;
        private PersistentDictionary<Guid, ModeratedClientEntity> moderatedClientList;
        private PersistentDictionary<Guid, TimeClientEntity> timeClientList;
        private PersistentDictionary<uint, string> previousServerGroupsList;

        internal PersistentDictionary<uint, uint> ClientLastChannelList
        {
            get { return this.clientLastChannelList ?? (this.clientLastChannelList = new PersistentDictionary<uint, uint>(string.Format(@"Data\{0}\LastChannel", name))); }
        }

        internal PersistentDictionary<Guid, StickyClientEntity> StickyClientList
        {
            get { return this.stickyClientList ?? (this.stickyClientList = new PersistentDictionary<Guid, StickyClientEntity>(string.Format(@"Data\{0}\Sticky", name))); }
        }

        internal PersistentDictionary<Guid, VotedClientEntity> VotedClientList
        {
            get { return this.votedClientList ?? (this.votedClientList = new PersistentDictionary<Guid, VotedClientEntity>(string.Format(@"Data\{0}\Voted", name))); }
        }

        internal PersistentDictionary<uint, DateTime> ClientLastSeen
        {
            get { return this.clientLastSeen ?? (this.clientLastSeen = new PersistentDictionary<uint, DateTime>(string.Format(@"Data\{0}\Seen", name))); }
        }

        internal PersistentDictionary<Guid, ModeratedClientEntity> ModeratedClientList
        {
            get { return this.moderatedClientList ?? (this.moderatedClientList = new PersistentDictionary<Guid, ModeratedClientEntity>(string.Format(@"Data\{0}\Moderated", name))); }
        }

        internal PersistentDictionary<Guid, TimeClientEntity> TimeClientList
        {
            get { return this.timeClientList ?? (this.timeClientList = new PersistentDictionary<Guid, TimeClientEntity>(string.Format(@"Data\{0}\Time", name))); }
        }

        internal PersistentDictionary<uint, string> PreviousServerGroupsList
        {
            get { return this.previousServerGroupsList ?? (this.previousServerGroupsList = new PersistentDictionary<uint, string>(string.Format(@"Data\{0}\PrevServerGroups", name))); }
        }

        #endregion

        #region Basic Data

        internal DateTime Now;

        internal int LastConnectionWaiting = 1;
        internal DateTime LastConnectionError = DateTime.UtcNow;

        internal List<ClientListEntry> ClientList;
        internal Dictionary<uint, ClientInfoResponse> ClientInfoList = new Dictionary<uint, ClientInfoResponse>();
        internal List<ChannelListEntry> ChannelList;
        internal Dictionary<uint, ChannelInfoResponse> ChannelInfoList = new Dictionary<uint, ChannelInfoResponse>();
        internal List<ClientDbEntry> ClientDatabaseList = new List<ClientDbEntry>();
        internal List<FileEntity> FileList = new List<FileEntity>();
        internal List<ComplainListEntry> CompliantList;
        internal List<ServerListItem> ServerList;
        internal Dictionary<Guid, DateTime> LastIntervalList = new Dictionary<Guid, DateTime>();

        #endregion

        #region Locks

        internal readonly object lockNow = new object();
        internal readonly object lockGetClientInfo = new object();
        internal readonly object lockGetClientList = new object();
        internal readonly object lockGetClientsFromDatabase = new object();
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

            lock (lockClientLastChannelList)
            {
                if (clientLastChannelList != null) clientLastChannelList.Flush();
            }

            lock (lockStickyClientList)
            {
                var entities = StickyClientList.Where(m => m.Value.Creation.AddMinutes(m.Value.StickTime) < DateTime.UtcNow);
                foreach (var entity in entities.ToList())
                {
                    StickyClientList.Remove(entity.Key);
                }

                if (stickyClientList != null) stickyClientList.Flush();
            }

            lock (lockVotedClientList)
            {
                var entities = VotedClientList.Where(m => m.Value.Creation.AddHours(1) < DateTime.UtcNow);
                foreach (var entity in entities.ToList())
                {
                    VotedClientList.Remove(entity.Key);
                }

                if (votedClientList != null) votedClientList.Flush();
            }

            lock (lockSeenClientList)
            {
                if (clientLastSeen != null) clientLastSeen.Flush();
            }

            lock (lockModeratedClientList)
            {
                if (moderatedClientList != null) moderatedClientList.Flush();
            }

            lock (lockTimeClientList)
            {
                if (timeClientList != null) timeClientList.Flush();
            }

            lock (lockPreviousServerGroupsList)
            {
                if (previousServerGroupsList != null) previousServerGroupsList.Flush();
            }
        }

        #endregion

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            if (clientLastChannelList != null)
            {
                clientLastChannelList.Flush();
                clientLastChannelList.Dispose();
                this.clientLastChannelList = null;
            }

            if (stickyClientList != null)
            {
                stickyClientList.Flush();
                stickyClientList.Dispose();
                this.stickyClientList = null;
            }

            if (votedClientList != null)
            {
                votedClientList.Flush();
                VotedClientList.Dispose();
                this.votedClientList = null;
            }

            if (clientLastSeen != null)
            {
                clientLastSeen.Flush();
                clientLastSeen.Dispose();
                this.clientLastSeen = null;
            }

            if (moderatedClientList != null)
            {
                moderatedClientList.Flush();
                moderatedClientList.Dispose();
                this.moderatedClientList = null;
            }

            if (timeClientList != null)
            {
                timeClientList.Flush();
                timeClientList.Dispose();
                this.timeClientList = null;
            }

            if (previousServerGroupsList != null)
            {
                previousServerGroupsList.Flush();
                previousServerGroupsList.Dispose();
                this.previousServerGroupsList = null;
            }

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