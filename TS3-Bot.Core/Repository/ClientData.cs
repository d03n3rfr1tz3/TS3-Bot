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
        public Dictionary<uint, ClientDbEntry> GetClientsFromDatabase()
        {
            lock (Container.lockGetClientsFromDatabase)
            {
                if (!Container.ClientDatabaseList.Any())
                {
                    var response = QueryRunner.GetClientDatabaseList(0, 100, true);
                    response.ToList().ForEach(m =>
                    {
                        if (!Container.ClientDatabaseList.ContainsKey(m.DatabaseId))
                        {
                            Container.ClientDatabaseList.Add(m.DatabaseId, m);
                        }
                    });

                    for (int i = 100; i < (response.TotalClientsInDatabase ?? uint.MaxValue); i = i + 100)
                    {
                        var moreResponse = QueryRunner.GetClientDatabaseList((uint)i, (uint)(i + 100), false);
                        if (!moreResponse.Any()) break;
                        moreResponse.ToList().ForEach(m =>
                        {
                            if (!Container.ClientDatabaseList.ContainsKey(m.DatabaseId))
                            {
                                Container.ClientDatabaseList.Add(m.DatabaseId, m);
                            }
                        });
                    }
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
            lock (Container.lockGetClientFromDatabase)
            {
                var clientDatabaseList = GetClientsFromDatabase();
                return clientDatabaseList.ContainsKey(clientDatabaseId) ? clientDatabaseList[clientDatabaseId] : null;
            }
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
                using (var database = new BotDatabaseEntities())
                {
                    var entity = database.Seen.FirstOrDefault(c => c.ClientDatabaseId == clientDatabaseId);
                    return entity == null ? default(DateTime) : entity.LastSeen;
                }
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
                using (var database = new BotDatabaseEntities())
                {
                    var entity = database.Seen.FirstOrDefault(c => c.ClientDatabaseId == clientDatabaseId);
                    if (entity != null)
                    {
                        entity.LastSeen = Repository.Static.Now;
                    }
                    else
                    {
                        database.Seen.AddObject(new Seen
                        {
                            Id = Guid.NewGuid(),
                            ClientDatabaseId = (int)clientDatabaseId,
                            LastSeen = Repository.Static.Now
                        });
                    }

                    database.SaveChanges();
                }
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
                using (var database = new BotDatabaseEntities())
                {
                    return database.Away.Any(m => m.ClientDatabaseId == clientDatabaseId);
                }
            }
        }

        /// <summary>
        /// Gets the last channel by client id.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <returns></returns>
        public Away GetLastChannelByClientId(uint clientDatabaseId)
        {
            lock (Container.lockClientLastChannelList)
            {
                using (var database = new BotDatabaseEntities())
                {
                    return database.Away.FirstOrDefault(m => m.ClientDatabaseId == clientDatabaseId);
                }
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
                using (var database = new BotDatabaseEntities())
                {
                    if (!database.Away.Any(m => m.ClientDatabaseId == clientDatabaseId))
                    {
                        database.Away.AddObject(new Away
                        {
                            Id = Guid.NewGuid(),
                            ClientDatabaseId = (int)clientDatabaseId,
                            LastChannelId = (int)channelId,
                            Creation = Repository.Static.Now
                        });
                        database.SaveChanges();
                    }
                }
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
                using (var database = new BotDatabaseEntities())
                {
                    database.Away.Where(m => m.ClientDatabaseId == clientDatabaseId).ToList().ForEach(m => database.Away.DeleteObject(m));
                    database.SaveChanges();
                }
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
                using (var database = new BotDatabaseEntities())
                {
                    database.Vote.AddObject(new Vote
                    {
                        Id = Guid.NewGuid(),
                        ClientDatabaseId = (int)votedClient.ClientDatabaseId,
                        ChannelId = (int?)votedClient.ChannelId,
                        Creation = Repository.Static.Now
                    });
                    database.SaveChanges();
                }
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
                using (var database = new BotDatabaseEntities())
                {
                    database.Vote.Where(m => m.ClientDatabaseId == clientDatabaseId).ToList().ForEach(m => database.Vote.DeleteObject(m));
                    database.SaveChanges();
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
                using (var database = new BotDatabaseEntities())
                {
                    return database.Vote.Any(m => m.ClientDatabaseId == clientDatabaseId);
                }
            }
        }

        /// <summary>
        /// Gets the client voted.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <returns>voted client.</returns>
        public Vote GetClientVoted(uint clientDatabaseId)
        {
            lock (Container.lockVotedClientList)
            {
                using (var database = new BotDatabaseEntities())
                {
                    return database.Vote.FirstOrDefault(m => m.ClientDatabaseId == clientDatabaseId);
                }
            }
        }

        #endregion

        #region Moderated

        /// <summary>
        /// Captures all moderations.
        /// </summary>
        public void CaptureModeration()
        {
            using (var database = new BotDatabaseEntities())
            {
                var entries = new List<LogEntry>();
                var lastModerated = database.Moderate.Any() ? database.Moderate.Max(m => m.Creation) : DateTime.MinValue;

                uint index = 0;
                ushort length = 10;
                try
                {
                    while (true)
                    {
                        var logEntries = QueryRunner.GetLogEntries(length, false, index).LogEntries;
                        entries.AddRange(logEntries.Where(m => m.LogLevel == LogLevel.Info && m.LogCategory == "VirtualServer"));

                        if (!logEntries.Any() || logEntries.Any(l => l.TimeStamp < lastModerated)) break;
                        index += length;
                        length = 50;
                    }
                }
                catch (ArgumentException) { }

                foreach (var logEntry in entries)
                {
                    var entity = ModeratedClientEntity.Parse(logEntry);
                    if (entity.HasValue)
                    {
                        lock (Container.lockModeratedClientList)
                        {
                            database.Moderate.AddObject(new Moderate
                            {
                                Id = Guid.NewGuid(),
                                ClientDatabaseId = (int)entity.Value.User,
                                ModeratorDatabaseId = (int)entity.Value.Moderator,
                                Creation = entity.Value.Moderated,
                                ServerGroup = (int)entity.Value.ServerGroup,
                                Type = (byte)entity.Value.Type
                            });
                        }
                    }
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
                using (var database = new BotDatabaseEntities())
                {
                    database.Moderate.AddObject(new Moderate
                    {
                        Id = Guid.NewGuid(),
                        ClientDatabaseId = (int)clientDatabaseId,
                        ModeratorDatabaseId = (int)moderatorDatabaseId,
                        Creation = DateTime.UtcNow,
                        ServerGroup = (int)serverGroupId,
                        Type = (byte)type
                    });
                }
            }
        }

        /// <summary>
        /// Gets the moderation.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        public IEnumerable<Moderate> GetModeration(ModerationType type, DateTime? fromDate, DateTime? toDate)
        {
            lock (Container.lockModeratedClientList)
            {
                using (var database = new BotDatabaseEntities())
                {
                    var moderationType = (byte)type;
                    var entities = database.Moderate.Where(m => m.Type == moderationType);

                    if (fromDate.HasValue)
                    {
                        entities = entities.Where(m => m.Creation > fromDate);
                    }

                    if (toDate.HasValue)
                    {
                        entities = entities.Where(m => m.Creation < toDate);
                    }

                    return entities.ToList();
                }
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
                using (var database = new BotDatabaseEntities())
                {
                    const byte moderationType = (byte)ModerationType.Added;
                    var joins = database.Moderate.Where(m => m.Type == moderationType && m.ServerGroup == serverGroupId && m.ClientDatabaseId == clientDatabaseId);
                    if (joins.Any()) return joins.Max(m => m.Creation);
                    if (database.Moderate.Any()) return database.Moderate.Where(m => m.Type == moderationType && m.ServerGroup == serverGroupId).Min(m => m.Creation);
                    return DateTime.MinValue;
                }
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
                using (var database = new BotDatabaseEntities())
                {
                    var entity = database.Time.FirstOrDefault(c => c.ClientDatabaseId == clientDatabaseId && c.Joined == lastConnected);
                    if (entity != null)
                    {
                        entity.Disconnected = disconnected ?? Repository.Static.Now;
                    }
                    else
                    {
                        database.Time.AddObject(new Time
                        {
                            Id = Guid.NewGuid(),
                            ClientDatabaseId = (int)clientDatabaseId,
                            Joined = lastConnected,
                            Disconnected = disconnected ?? Repository.Static.Now,
                            TotalMinutes = ((disconnected ?? Repository.Static.Now) - lastConnected).TotalMinutes
                        });
                    }

                    database.SaveChanges();
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
            lock (Container.lockTimeClientList)
            {
                using (var database = new BotDatabaseEntities())
                {
                    var times = database.Time.AsQueryable();

                    if (fromTime.HasValue)
                    {
                        times = times.Where(c => c.Disconnected > fromTime);
                    }

                    if (toTime.HasValue)
                    {
                        times = times.Where(m => m.Joined < toTime);
                    }

                    return times.GroupBy(m => m.ClientDatabaseId).ToList().ToDictionary(g => (uint)g.Key, g => g.Sum(m => m.GetTime(fromTime, toTime)));
                }
            }
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
            lock (Container.lockTimeClientList)
            {
                using (var database = new BotDatabaseEntities())
                {
                    var times = database.Time.AsQueryable();

                    if (fromTime.HasValue)
                    {
                        times = times.Where(c => c.Disconnected > fromTime);
                    }

                    if (toTime.HasValue)
                    {
                        times = times.Where(m => m.Joined < toTime);
                    }

                    return times.GroupBy(m => m.ClientDatabaseId).ToList().Where(m => clientDatabaseIds.Contains((uint)m.Key)).ToDictionary(g => (uint)g.Key, g => g.Sum(m => m.GetTime(fromTime, toTime)));
                }
            }
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
            lock (Container.lockTimeClientList)
            {
                using (var database = new BotDatabaseEntities())
                {
                    var times = database.Time.Where(c => c.ClientDatabaseId == clientDatabaseId);

                    if (fromTime.HasValue)
                    {
                        times = times.Where(c => c.Disconnected > fromTime);
                    }

                    if (toTime.HasValue)
                    {
                        times = times.Where(m => m.Joined < toTime);
                    }

                    return times.ToList().Sum(m => m.GetTime(fromTime, toTime));
                }
            }
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
                using (var database = new BotDatabaseEntities())
                {
                    var users = database.Time.AsQueryable();

                    if (fromTime.HasValue)
                    {
                        users = users.Where(c => c.Disconnected > fromTime);
                    }

                    if (toTime.HasValue)
                    {
                        users = users.Where(m => m.Joined < toTime);
                    }

                    return users.Select(m => m.ClientDatabaseId).Distinct().ToList().Cast<uint>().ToList();
                }
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
                using (var database = new BotDatabaseEntities())
                {
                    foreach (var currentGroup in currentGroups)
                    {
                        database.PreviousServerGroup.AddObject(new PreviousServerGroup
                        {
                            Id = Guid.NewGuid(),
                            ClientDatabaseId = (int)clientDatabaseId,
                            ServerGroup = (int)currentGroup,
                            Creation = Repository.Static.Now
                        });
                    }

                    database.SaveChanges();
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
                using (var database = new BotDatabaseEntities())
                {
                    var serverGroups = database.PreviousServerGroup.Where(m => m.ClientDatabaseId == clientDatabaseId).ToList().Select(m => (uint)m.ServerGroup).ToList();
                    Repository.Client.AddClientServerGroups(clientDatabaseId, serverGroups);
                    database.PreviousServerGroup.Where(m => m.ClientDatabaseId == clientDatabaseId).ToList().ForEach(m => database.PreviousServerGroup.DeleteObject(m));
                }
            }
        }

        #endregion
    }
}