namespace DirkSarodnick.TS3_Bot.Core.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Entity;
    using Helper;
    using TS3QueryLib.Core.CommandHandling;
    using TS3QueryLib.Core.Server.Entities;
    using TS3QueryLib.Core.Server.Responses;

    /// <summary>
    /// Defines the ClientData class.
    /// </summary>
    public class ClientData : DefaultData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientData"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public ClientData(DataRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// Gets the client info.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <returns></returns>
        public ClientInfoResponse GetClientInfo(uint clientId)
        {
            lock (Container.lockGetClientInfo)
            {
                if (!Container.ClientInfoList.ContainsKey(clientId))
                    Container.ClientInfoList.Add(clientId, QueryRunner.GetClientInfo(clientId));
            }
            return Container.ClientInfoList[clientId];
        }

        /// <summary>
        /// Gets the client list info.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <returns></returns>
        public SimpleClientEntity GetClientSimple(uint clientDatabaseId)
        {
            var client = GetClientList().FirstOrDefault(m => m.ClientDatabaseId == clientDatabaseId);
            if (client != null)
            {
                return new SimpleClientEntity(client);
            }

            return null;
        }

        /// <summary>
        /// Gets the client list.
        /// </summary>
        /// <returns></returns>
        public List<ClientListEntry> GetClientList()
        {
            lock (Container.lockGetClientList)
            {
                if (Container.ClientList == null)
                {
                    Container.ClientList = QueryRunner.GetClientList(true, true, true, true, false, true, false, true, true)
                        .Where(m => m.ClientType == 0).ToList();
                    Container.ClientList.Where(m => !IsClientGuest(m.ClientDatabaseId, m.ServerGroups))
                        .ForEach(c => SetLastSeen(c.ClientDatabaseId));
                }

                return Container.ClientList.ToList();
            }
        }

        /// <summary>
        /// Gets the clients from database.
        /// </summary>
        /// <returns></returns>
        public List<ClientDbEntry> GetClientsFromDatabase()
        {
            lock (Container.lockGetClientsFromDatabase)
            {
                if (!Container.ClientDatabaseList.Any())
                {
                    var response = QueryRunner.GetClientDatabaseList(0, 100, true);
                    var clientDatabaseList = response.ToList();

                    for (int i = 100; i < (response.TotalClientsInDatabase ?? uint.MaxValue); i = i + 100)
                    {
                        clientDatabaseList.AddRange(QueryRunner.GetClientDatabaseList((uint)i, (uint)(i + 100), false));
                    }

                    Container.ClientDatabaseList = clientDatabaseList.ToList();
                }

                return Container.ClientDatabaseList;
            }
        }

        /// <summary>
        /// Gets the client data base info.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <returns></returns>
        public ClientDbEntry GetClientDataBaseInfo(uint clientDatabaseId)
        {
            return GetClientsFromDatabase().FirstOrDefault(c => c.DatabaseId == clientDatabaseId);
        }

        /// <summary>
        /// Gets the client database id by client id.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <returns></returns>
        public uint? GetClientDatabaseIdByClientId(uint clientId)
        {
            var client = GetClientList().FirstOrDefault(m => m.ClientId == clientId);
            return client != null ? client.ClientDatabaseId : (uint?)null;
        }

        /// <summary>
        /// Gets the client id by client database id.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <returns></returns>
        public uint? GetClientIdByClientDatabaseId(uint clientDatabaseId)
        {
            var client = GetClientList().FirstOrDefault(m => m.ClientDatabaseId == clientDatabaseId);
            return client != null ? client.ClientDatabaseId : (uint?)null;
        }

        /// <summary>
        /// Gets the client server groups.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <returns></returns>
        public List<ServerGroupLight> GetClientServerGroups(uint clientDatabaseId)
        {
            lock (Container.lockGetClientServerGroups)
            {
                if (!Container.ClientServerGroupList.ContainsKey(clientDatabaseId))
                    Container.ClientServerGroupList.Add(clientDatabaseId, new ClientServerGroupList(QueryRunner.GetServerGroupsByClientId(clientDatabaseId).ToList()));

                if (Container.ClientServerGroupList[clientDatabaseId].Creation.AddMinutes(1) < Repository.Static.Now)
                {
                    Container.ClientServerGroupList[clientDatabaseId] = new ClientServerGroupList(QueryRunner.GetServerGroupsByClientId(clientDatabaseId).ToList());
                }

                return Container.ClientServerGroupList[clientDatabaseId].ServerGroups;
            }
        }

        /// <summary>
        /// Adds the client server groups.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <param name="serverGroups">The server groups.</param>
        public void AddClientServerGroups(uint clientDatabaseId, IEnumerable<uint> serverGroups)
        {
            lock (Container.lockGetClientServerGroups)
            {
                serverGroups.ForEach(m => QueryRunner.AddClientToServerGroup(m, clientDatabaseId));
                Container.ClientServerGroupList.Remove(clientDatabaseId);
            }
        }

        /// <summary>
        /// Removes the client server groups.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <param name="serverGroups">The server groups.</param>
        public void RemoveClientServerGroups(uint clientDatabaseId, IEnumerable<uint> serverGroups)
        {
            lock (Container.lockGetClientServerGroups)
            {
                serverGroups.ForEach(m => QueryRunner.DeleteClientFromServerGroup(m, clientDatabaseId));
                Container.ClientServerGroupList.Remove(clientDatabaseId);
            }
        }

        /// <summary>
        /// Determines whether [is client guest] [the specified client database id].
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <returns>
        /// 	<c>true</c> if [is client guest] [the specified client database id]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsClientGuest(uint clientDatabaseId)
        {
            return IsClientGuest(clientDatabaseId, GetClientServerGroups(clientDatabaseId).Select(n => n.Id));
        }

        /// <summary>
        /// Determines whether [is client guest] [the specified client database id].
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <param name="clientServerGroups">The client server groups.</param>
        /// <returns>
        /// 	<c>true</c> if [is client guest] [the specified client database id]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsClientGuest(uint clientDatabaseId, IEnumerable<uint> clientServerGroups)
        {
            return clientServerGroups.Any(n => Repository.Settings.TeamSpeak.GuestGroups.Contains(n));
        }

        /// <summary>
        /// Gets the last seen.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <returns></returns>
        public DateTime GetLastSeen(uint clientDatabaseId)
        {
            lock (Container.lockSeenClientList)
            {
                if (!Container.ClientLastSeen.ContainsKey(clientDatabaseId))
                    return default(DateTime);
                return Container.ClientLastSeen[clientDatabaseId];
            }
        }

        /// <summary>
        /// Sets the last seen.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        public void SetLastSeen(uint clientDatabaseId)
        {
            lock (Container.lockSeenClientList)
            {
                if (Container.ClientLastSeen.ContainsKey(clientDatabaseId))
                    Container.ClientLastSeen[clientDatabaseId] = Repository.Static.Now;
                else
                    Container.ClientLastSeen.Add(clientDatabaseId, Repository.Static.Now);
            }
        }

        /// <summary>
        /// Gets the clients by server group.
        /// </summary>
        /// <param name="serverGroupId">The server group id.</param>
        /// <returns>The clients.</returns>
        public List<uint> GetClientsByServerGroup(uint serverGroupId)
        {
            return QueryRunner.GetServerGroupClientList(serverGroupId).Select(m => m.DatabaseId).ToList();
        }

        #region Last Channel

        /// <summary>
        /// Determines whether [has last channel by client id] [the specified client id].
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <returns>
        /// 	<c>true</c> if [has last channel by client id] [the specified client id]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasLastChannelByClientId(uint clientDatabaseId)
        {
            lock (Container.lockClientLastChannelList)
            {
                return Container.ClientLastChannelList.ContainsKey(clientDatabaseId);
            }
        }

        /// <summary>
        /// Gets the last channel by client id.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <returns></returns>
        public uint GetLastChannelByClientId(uint clientDatabaseId)
        {
            lock (Container.lockClientLastChannelList)
            {
                return Container.ClientLastChannelList[clientDatabaseId];
            }
        }

        /// <summary>
        /// Sets the last channel by client id.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <param name="channelId">The channel id.</param>
        public void SetLastChannelByClientId(uint clientDatabaseId, uint channelId)
        {
            lock (Container.lockClientLastChannelList)
            {
                if (!HasLastChannelByClientId(clientDatabaseId))
                    Container.ClientLastChannelList.Add(clientDatabaseId, channelId);
            }
        }

        /// <summary>
        /// Removes the last channel by client id.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        public void RemoveLastChannelByClientId(uint clientDatabaseId)
        {
            lock (Container.lockClientLastChannelList)
            {
                Container.ClientLastChannelList.Remove(clientDatabaseId);
            }
        }

        #endregion

        #region Client Warning

        /// <summary>
        /// Gets the client to warning list.
        /// </summary>
        /// <returns></returns>
        public List<ClientWarningEntity> GetClientToWarningList()
        {
            lock (Container.lockClientWarningList)
            {
                return Container.ClientWarningList;
            }
        }

        /// <summary>
        /// Determines whether [has client warning of] [the specified client id].
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        /// 	<c>true</c> if [has client warning of] [the specified client id]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasClientWarningOf(uint clientId, WarnType type)
        {
            return GetClientToWarningList().Any(m => m.ClientId == clientId && m.Type == type);
        }

        /// <summary>
        /// Determines whether [is client warned] [the specified client id].
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        /// 	<c>true</c> if [is client warned] [the specified client id]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsClientWarned(uint clientId, WarnType type)
        {
            return GetClientToWarningList().Any(m => m.ClientId == clientId && m.Type == type);
        }

        /// <summary>
        /// Determines whether [is client warning over] [the specified client id].
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="type">The type.</param>
        /// <returns>
        /// 	<c>true</c> if [is client warning over] [the specified client id]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsClientWarningOver(uint clientId, WarnType type)
        {
            return GetClientToWarningList().Any(m => m.ClientId == clientId && m.Type == type
                                              && m.Creation.AddSeconds(30) < Repository.Static.Now);
        }

        /// <summary>
        /// Adds the client to warning list.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="type">The type.</param>
        public void AddClientToWarningList(uint clientId, WarnType type)
        {
            var clientWarning = new ClientWarningEntity(clientId, type);
            lock (Container.lockClientWarningList)
            {
                if (!Container.ClientWarningList.Contains(clientWarning))
                    Container.ClientWarningList.Add(clientWarning);
            }
        }

        #endregion

        #region Voted

        /// <summary>
        /// Adds the voted clients.
        /// </summary>
        /// <param name="votedClient">The voted client.</param>
        public void AddVotedClients(VotedClientEntity votedClient)
        {
            lock (Container.lockVotedClientList)
            {
                Container.VotedClientList.Add(Guid.NewGuid(), votedClient);
            }
        }

        /// <summary>
        /// Removes the voted clients.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        public void RemoveVotedClients(uint clientDatabaseId)
        {
            lock (Container.lockVotedClientList)
            {
                var entities = Container.VotedClientList.Where(m => m.Value.ClientDatabaseId == clientDatabaseId);
                foreach (var entity in entities.ToList())
                {
                    Container.VotedClientList.Remove(entity.Key);
                }
            }
        }

        /// <summary>
        /// Determines whether [is client voted] [the specified client database id].
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <returns>
        /// 	<c>true</c> if [is client voted] [the specified client database id]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsClientVoted(uint clientDatabaseId)
        {
            lock (Container.lockVotedClientList)
            {
                return Container.VotedClientList.Values.Any(m => m.ClientDatabaseId == clientDatabaseId);
            }
        }

        /// <summary>
        /// Gets the client voted.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <returns>voted client.</returns>
        public VotedClientEntity GetClientVoted(uint clientDatabaseId)
        {
            lock (Container.lockVotedClientList)
            {
                return Container.VotedClientList.Values.FirstOrDefault(m => m.ClientDatabaseId == clientDatabaseId);
            }
        }

        #endregion

        #region Moderated

        /// <summary>
        /// Captures all moderations.
        /// </summary>
        public void CaptureModeration()
        {
            var entries = new List<LogEntry>();
            var lastModerated = Container.ModeratedClientList.Count > 0 ? Container.ModeratedClientList.Max(m => m.Value.Moderated) : DateTime.MinValue;

            uint index = 0;
            const ushort length = 100;
            try
            {
                while (true)
                {
                    var logEntries = QueryRunner.GetLogEntries(length, false, index).LogEntries;
                    entries.AddRange(logEntries.Where(m => m.LogLevel == LogLevel.Info && m.LogCategory == "VirtualServer"));

                    if (!logEntries.Any() || logEntries.Any(l => l.TimeStamp < lastModerated)) break;
                    index += length;
                }
            }
            catch(ArgumentException) { }

            CaptureModeration(entries);
        }

        /// <summary>
        /// Captures the moderation.
        /// </summary>
        /// <param name="entries">The entries.</param>
        public void CaptureModeration(IEnumerable<LogEntry> entries)
        {
            entries.ForEach(CaptureModeration);
        }

        /// <summary>
        /// Captures the moderation.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        public void CaptureModeration(LogEntry logEntry)
        {
            var entity = ModeratedClientEntity.Parse(logEntry);
            if (entity.HasValue)
            {
                lock (Container.lockModeratedClientList)
                {
                    if (!Container.ModeratedClientList.Any(m => m.Value.Type == entity.Value.Type && m.Value.User == entity.Value.User && m.Value.ServerGroup == entity.Value.ServerGroup))
                        Container.ModeratedClientList.Add(Guid.NewGuid(), entity.Value);
                }
            }
        }

        /// <summary>
        /// Captures the moderation.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="moderatorDatabaseId">The moderator database id.</param>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <param name="serverGroupId">The server group id.</param>
        public void CaptureModeration(ModerationType type, uint moderatorDatabaseId, uint clientDatabaseId, uint serverGroupId)
        {
            lock (Container.lockModeratedClientList)
            {
                if (!Container.ModeratedClientList.Any(m => m.Value.Type == type && m.Value.User == clientDatabaseId && m.Value.ServerGroup == serverGroupId))
                    Container.ModeratedClientList.Add(Guid.NewGuid(), new ModeratedClientEntity(type, moderatorDatabaseId, clientDatabaseId, serverGroupId));
            }
        }

        /// <summary>
        /// Gets the moderation.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        public IEnumerable<ModeratedClientEntity> GetModeration(ModerationType type, DateTime? fromDate, DateTime? toDate)
        {
            lock (Container.lockModeratedClientList)
            {
                var entities = Container.ModeratedClientList.Where(m => m.Value.Type == type).AsEnumerable();

                if (fromDate.HasValue)
                {
                    entities = entities.Where(m => m.Value.Moderated > fromDate);
                }

                if (toDate.HasValue)
                {
                    entities = entities.Where(m => m.Value.Moderated < toDate);
                }

                return entities.Select(m => m.Value).ToList();
            }
        }

        /// <summary>
        /// Gets the server group joined.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <param name="serverGroupId">The server group id.</param>
        /// <returns></returns>
        public DateTime GetServerGroupJoined(uint clientDatabaseId, uint serverGroupId)
        {
            lock (Container.lockModeratedClientList)
            {
                var joins = Container.ModeratedClientList.Where(m => m.Value.Type == ModerationType.Added && m.Value.User == clientDatabaseId && m.Value.ServerGroup == serverGroupId);
                if (joins.Any()) return joins.Max(m => m.Value.Moderated);
                if (Container.ModeratedClientList.Any()) return Container.ModeratedClientList.Where(m => m.Value.ServerGroup == serverGroupId).Min(m => m.Value.Moderated);
                return DateTime.MinValue;
            }
        }

        #endregion

        #region Time

        /// <summary>
        /// Captures the times for all online clients.
        /// </summary>
        public void CaptureTimes()
        {
            Repository.Client.GetClientList().ForEach(m => this.CaptureTime(m.ClientDatabaseId, m.ClientLastConnected, null));
        }

        /// <summary>
        /// Captures the time for specified client.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <param name="connected">The connected.</param>
        /// <param name="disconnected">The disconnected.</param>
        public void CaptureTime(uint clientDatabaseId, DateTime? connected, DateTime? disconnected)
        {
            var lastConnected = connected ?? DateTime.UtcNow;
            if (!connected.HasValue)
            {
                var entity = Repository.Client.GetClientSimple(clientDatabaseId);
                if (entity != null && entity.Connected.HasValue) lastConnected = entity.Connected.Value;
            }

            lock (Container.lockTimeClientList)
            {
                var times = Container.TimeClientList.Where(m => m.Value.User == clientDatabaseId && m.Value.Joined == lastConnected);
                if (times.Any())
                {
                    var time = times.Single();
                    Container.TimeClientList[time.Key] = new TimeClientEntity(clientDatabaseId, lastConnected, disconnected);
                }
                else
                {
                    Container.TimeClientList.Add(Guid.NewGuid(), new TimeClientEntity(clientDatabaseId, lastConnected, disconnected));
                }
            }
        }

        /// <summary>
        /// Gets the total time spended on TS3 for all users.
        /// </summary>
        /// <param name="fromTime">From time.</param>
        /// <param name="toTime">To time.</param>
        /// <returns></returns>
        public Dictionary<uint, double> GetTime(DateTime? fromTime, DateTime? toTime)
        {
            IEnumerable<KeyValuePair<Guid, TimeClientEntity>> times;

            lock (Container.lockTimeClientList)
            {
                times = Container.TimeClientList.AsEnumerable();

                if (fromTime.HasValue)
                {
                    times = times.Where(m => m.Value.Disconnected > fromTime);
                }

                if (toTime.HasValue)
                {
                    times = times.Where(m => m.Value.Joined < toTime);
                }

                times = times.ToList();
            }

            return times.GroupBy(m => m.Value.User).ToDictionary(g => g.Key, g => g.Sum(m => m.Value.GetTime(fromTime, toTime).TotalMinutes));
        }

        /// <summary>
        /// Gets the total time spended on TS3 for specified clients.
        /// </summary>
        /// <param name="clientDatabaseIds">The client database ids.</param>
        /// <param name="fromTime">From time.</param>
        /// <param name="toTime">To time.</param>
        /// <returns></returns>
        public Dictionary<uint, double> GetTime(IEnumerable<uint> clientDatabaseIds, DateTime? fromTime, DateTime? toTime)
        {
            IEnumerable<KeyValuePair<Guid, TimeClientEntity>> times;

            lock (Container.lockTimeClientList)
            {
                times = Container.TimeClientList.Where(m => clientDatabaseIds.Any(c => c == m.Value.User)).AsEnumerable();

                if (fromTime.HasValue)
                {
                    times = times.Where(m => m.Value.Disconnected > fromTime);
                }

                if (toTime.HasValue)
                {
                    times = times.Where(m => m.Value.Joined < toTime);
                }

                times = times.ToList();
            }

            return times.GroupBy(m => m.Value.User).ToDictionary(g => g.Key, g => g.Sum(m => m.Value.GetTime(fromTime, toTime).TotalMinutes));
        }

        /// <summary>
        /// Gets the total time spended on TS3 for specified client.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <param name="fromTime">From time.</param>
        /// <param name="toTime">To time.</param>
        /// <returns></returns>
        public double GetTime(uint clientDatabaseId, DateTime? fromTime, DateTime? toTime)
        {
            IEnumerable<KeyValuePair<Guid, TimeClientEntity>> times;

            lock (Container.lockTimeClientList)
            {
                times = Container.TimeClientList.Where(m => m.Value.User == clientDatabaseId).AsEnumerable();
                
                if (fromTime.HasValue)
                {
                    times = times.Where(m => m.Value.Disconnected > fromTime);
                }

                if (toTime.HasValue)
                {
                    times = times.Where(m => m.Value.Joined < toTime);
                }

                times = times.ToList();
            }

            return times.Sum(m => m.Value.GetTime(fromTime, toTime).TotalMinutes);
        }

        /// <summary>
        /// Gets the time users.
        /// </summary>
        /// <param name="fromTime">From time.</param>
        /// <param name="toTime">To time.</param>
        /// <returns></returns>
        public List<uint> GetTimeUsers(DateTime? fromTime, DateTime? toTime)
        {
            lock (Container.lockTimeClientList)
            {
                var users = Container.TimeClientList.AsEnumerable();

                if (fromTime.HasValue)
                {
                    users = users.Where(m => m.Value.Disconnected > fromTime);
                }

                if (toTime.HasValue)
                {
                    users = users.Where(m => m.Value.Joined < toTime);
                }

                return users.Select(m => m.Value.User).Distinct().ToList();
            }
        }

        #endregion

        #region ServerGroups

        /// <summary>
        /// Strips the groups.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        public void StripGroups(uint clientDatabaseId)
        {
            // Get the current Server Groups
            var currentGroups = Repository.Client.GetClientServerGroups(clientDatabaseId).Select(m => m.Id).ToList();

            lock (Container.lockPreviousServerGroupsList)
            {
                // Save current Server Groups, that they can restored later
                if (!Container.PreviousServerGroupsList.ContainsKey(clientDatabaseId))
                {
                    Container.PreviousServerGroupsList.Add(clientDatabaseId, string.Join(";", currentGroups));
                }
            }

            Repository.Client.RemoveClientServerGroups(clientDatabaseId, currentGroups);
        }

        /// <summary>
        /// Restores the groups.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        public void RestoreGroups(uint clientDatabaseId)
        {
            lock (Container.lockPreviousServerGroupsList)
            {
                if (Container.PreviousServerGroupsList.ContainsKey(clientDatabaseId))
                {
                    var serverGroups = Container.PreviousServerGroupsList[clientDatabaseId].Split(';').Select(uint.Parse);
                    Repository.Client.AddClientServerGroups(clientDatabaseId, serverGroups);
                    Container.PreviousServerGroupsList.Remove(clientDatabaseId);
                }
            }
        }

        #endregion
    }
}