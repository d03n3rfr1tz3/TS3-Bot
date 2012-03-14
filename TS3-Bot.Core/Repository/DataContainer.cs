namespace DirkSarodnick.TS3_Bot.Core.Repository
{
    using System;
    using System.Collections.Generic;
    using Entity;
    using TS3QueryLib.Core.Query.HelperClasses;
    using TS3QueryLib.Core.Query.Responses;

    public class DataContainer : IDisposable
    {
        private bool disposed;

        #region Specified Data

        public Dictionary<uint, uint> ClientLastChannelList = new Dictionary<uint, uint>();
        public Dictionary<uint, ClientServerGroupList> ClientServerGroupList = new Dictionary<uint, ClientServerGroupList>();
        public List<ClientWarningEntity> ClientWarningList = new List<ClientWarningEntity>();
        public List<StickyClientEntity> StickyClientList = new List<StickyClientEntity>();
        public List<VotedClientEntity> VotedClientList = new List<VotedClientEntity>();
        public Dictionary<uint, DateTime> ClientLastSeen = new Dictionary<uint, DateTime>();

        #endregion

        #region Basic Data

        public DateTime now;

        public int lastConnectionWaiting = 1;
        public DateTime lastConnectionError = DateTime.Now;

        public List<ClientListEntry> ClientList;
        public Dictionary<uint, ClientInfoResponse> ClientInfoList = new Dictionary<uint, ClientInfoResponse>();
        public List<ChannelListEntry> ChannelList;
        public Dictionary<uint, ChannelInfoResponse> ChannelInfoList = new Dictionary<uint, ChannelInfoResponse>();
        public List<ClientDbEntry> ClientDatabaseList = new List<ClientDbEntry>();
        public List<FileEntity> FileList = new List<FileEntity>();
        public List<ComplainListEntry> CompliantList;
        public List<ServerListItem> ServerList;
        public Dictionary<Guid, DateTime> LastIntervalList = new Dictionary<Guid, DateTime>();

        #endregion

        #region Locks

        public readonly object lockNow = new object();
        public readonly object lockGetClientInfo = new object();
        public readonly object lockGetClientList = new object();
        public readonly object lockGetClientsFromDatabase = new object();
        public readonly object lockGetRawClientsFromDatabase = new object();
        public readonly object lockGetChannelInfo = new object();
        public readonly object lockGetChannelList = new object();
        public readonly object lockGetClientServerGroups = new object();
        public readonly object lockClientWarningList = new object();
        public readonly object lockStickyClientList = new object();
        public readonly object lockVotedClientList = new object();
        public readonly object lockFileList = new object();
        public readonly object lockGetCompliantList = new object();
        public readonly object lockGetServerList = new object();
        public readonly object lockLastEventList = new object();

        #endregion

        #region Public Methods

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Clean()
        {
            lock (lockNow) now = default(DateTime);
            lock (lockGetClientList) ClientList = null;
            lock (lockGetChannelList) ChannelList = null;
            lock (lockGetCompliantList) CompliantList = null;
            lock (lockGetServerList) ServerList = null;
            lock (lockGetClientInfo) ClientInfoList.Clear();
            lock (lockGetChannelInfo) ChannelInfoList.Clear();

            lock (lockClientWarningList) ClientWarningList.RemoveAll(m => m.Creation.AddMinutes(5) < DateTime.Now);
            lock (lockStickyClientList) StickyClientList.RemoveAll(m => m.Creation.AddMinutes(m.StickTime) < DateTime.Now);
            lock (lockVotedClientList) VotedClientList.RemoveAll(m => m.Creation.AddHours(1) < DateTime.Now);
        }

        #endregion

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            ClientList = null;
            ClientInfoList = null;
            ChannelList = null;
            ChannelInfoList = null;
            ClientDatabaseList = null;
            FileList = null;
            CompliantList = null;
            ServerList = null;
            LastIntervalList = null;
            ClientLastChannelList = null;
            ClientServerGroupList = null;
            ClientWarningList = null;
            StickyClientList = null;
            VotedClientList = null;
            ClientLastSeen = null;

            GC.SuppressFinalize(this);
        }
    }
}