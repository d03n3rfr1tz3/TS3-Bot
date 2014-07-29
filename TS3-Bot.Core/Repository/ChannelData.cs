namespace DirkSarodnick.TS3_Bot.Core.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using TS3QueryLib.Core.Server.Entities;
    using TS3QueryLib.Core.Server.Responses;
    using DirkSarodnick.TS3_Bot.Core.Entity;
    using DirkSarodnick.TS3_Bot.Core.Helper;
    using System.Data.SQLite;

    /// <summary>
    /// Defines the ChannelData class.
    /// </summary>
    public class ChannelData : DefaultData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelData"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public ChannelData(DataRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// Gets the channel info.
        /// </summary>
        /// <param name="channelId">The channel id.</param>
        /// <returns></returns>
        public ChannelInfoResponse GetChannelInfo(uint channelId)
        {
            lock (Container.lockGetChannelInfo)
            {
                if (!Container.ChannelInfoList.ContainsKey(channelId))
                    Container.ChannelInfoList.Add(channelId, QueryRunner.GetChannelInfo(channelId));
            }
            return Container.ChannelInfoList[channelId];
        }

        /// <summary>
        /// Gets the channel list info.
        /// </summary>
        /// <param name="channelId">The channel id.</param>
        /// <returns></returns>
        public ChannelListEntry GetChannelListInfo(uint channelId)
        {
            return GetChannelList().FirstOrDefault(c => c.ChannelId == channelId);
        }

        /// <summary>
        /// Gets the channel list.
        /// </summary>
        /// <returns></returns>
        public List<ChannelListEntry> GetChannelList()
        {
            lock (Container.lockGetChannelList)
            {
                if (Container.ChannelList == null)
                    Container.ChannelList = QueryRunner.GetChannelList(true, true, false, false, false).Where(m => !m.IsSpacer).ToList();

                return Container.ChannelList.ToList();
            }
        }

        /// <summary>
        /// Gets the channel list.
        /// </summary>
        /// <param name="serverId">The server id.</param>
        /// <returns></returns>
        public List<ChannelListEntry> GetChannelList(uint serverId)
        {
            return DynamicQueryRunner(serverId).GetChannelList(true, true, false, false, false).Where(m => !m.IsSpacer).ToList();
        }

        #region Sticky Channel

        /// <summary>
        /// Gets the sticky clients.
        /// </summary>
        /// <returns></returns>
        public List<StickyClientEntity> GetStickyClients()
        {
            lock (Container.lockStickyClientList)
            {
                lock (Repository.Container.lockDatabase)
                {
                    using (var command = new SQLiteCommand(this.Container.DatabaseConnection))
                    {
                        command.CommandText = "SELECT ClientDatabaseId, ChannelId, StickMinutes, CreationDate FROM Sticky";

                        var result = new List<StickyClientEntity>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.HasRows && reader.Read())
                            {
                                result.Add(new StickyClientEntity
                                {
                                    ChannelId = (uint)reader.GetInt32(1),
                                    ClientDatabaseId = (uint)reader.GetInt32(0),
                                    StickTime = (uint)reader.GetInt32(2),
                                    Creation = reader.GetInt64(3).ToDateTime()
                                });
                            }
                        }
                        return result;
                    }
                }
            }
        }

        /// <summary>
        /// Adds the sticky clients.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <param name="channelId">The channel id.</param>
        /// <param name="stickTime">The stick time.</param>
        public void AddStickyClients(uint clientDatabaseId, uint channelId, uint stickTime)
        {
            lock (Container.lockStickyClientList)
            {
                lock (Repository.Container.lockDatabase)
                {
                    using (var command = new SQLiteCommand(this.Container.DatabaseConnection))
                    {
                        command.CommandText = string.Format("DELETE FROM Sticky WHERE ClientDatabaseId = {0} AND ChannelId = {1}", clientDatabaseId, channelId);
                        command.ExecuteNonQuery();
                    }

                    using (var command = new SQLiteCommand(this.Container.DatabaseConnection))
                    {
                        command.CommandText = string.Format("INSERT INTO Sticky(ClientDatabaseId, ChannelId, StickMinutes, CreationDate) VALUES({0}, {1}, {2}, {3})", clientDatabaseId, channelId, stickTime, Repository.Static.Now.ToTimeStamp());
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        /// <summary>
        /// Removes the sticky clients.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <param name="channelId">The channel id.</param>
        public void RemoveStickyClients(uint clientDatabaseId, uint? channelId = null)
        {
            lock (Container.lockStickyClientList)
            {
                lock (Repository.Container.lockDatabase)
                {
                    if (channelId.HasValue)
                    {
                        using (var command = new SQLiteCommand(this.Container.DatabaseConnection))
                        {
                            command.CommandText = string.Format("DELETE FROM Sticky WHERE ClientDatabaseId = {0} AND ChannelId = {1}", clientDatabaseId, channelId);
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (var command = new SQLiteCommand(this.Container.DatabaseConnection))
                        {
                            command.CommandText = string.Format("DELETE FROM Sticky WHERE ClientDatabaseId = {0}", clientDatabaseId);
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the client sticky.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <returns></returns>
        public uint? GetClientSticky(uint clientDatabaseId)
        {
            lock (Container.lockStickyClientList)
            {
                lock (Repository.Container.lockDatabase)
                {
                    using (var command = new SQLiteCommand(this.Container.DatabaseConnection))
                    {
                        command.CommandText = string.Format("SELECT ChannelId FROM Sticky WHERE ClientDatabaseId = {0} AND ChannelId = {1}", clientDatabaseId, Repository.Settings.Sticky.Channel);
                        var result = command.ExecuteScalar();
                        if (result is Int32 && (Int32)result > 0) return (uint)(Int32)result;
                        if (result is Int64 && (Int64)result > 0) return (uint)(Int64)result;
                    }

                    using (var command = new SQLiteCommand(this.Container.DatabaseConnection))
                    {
                        command.CommandText = string.Format("SELECT ChannelId FROM Sticky WHERE ClientDatabaseId = {0}", clientDatabaseId);
                        var result = command.ExecuteScalar();
                        if (result is Int32 && (Int32)result > 0) return (uint)(Int32)result;
                        if (result is Int64 && (Int64)result > 0) return (uint)(Int64)result;
                    }
                }
            }

            return null;
        }

        #endregion
    }
}