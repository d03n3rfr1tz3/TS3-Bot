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

        internal Dictionary<uint, uint> ClientLastChannelList = new Dictionary<uint, uint>();
        internal Dictionary<uint, ClientServerGroupList> ClientServerGroupList = new Dictionary<uint, ClientServerGroupList>();
        internal List<ClientWarningEntity> ClientWarningList = new List<ClientWarningEntity>();
        internal List<StickyClientEntity> StickyClientList = new List<StickyClientEntity>();
        internal List<VotedClientEntity> VotedClientList = new List<VotedClientEntity>();
        internal Dictionary<uint, DateTime> ClientLastSeen = new Dictionary<uint, DateTime>();

        #endregion

        #region Basic Data

        internal DateTime Now;

        internal int LastConnectionWaiting = 1;
        internal DateTime LastConnectionError = DateTime.Now;

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
        internal readonly object lockStickyClientList = new object();
        internal readonly object lockVotedClientList = new object();
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