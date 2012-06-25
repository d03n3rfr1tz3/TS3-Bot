namespace DirkSarodnick.TS3_Bot.Core.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Entity;
    using TS3QueryLib.Core.Server.Entities;
    using TS3QueryLib.Core.Server.Responses;
    using System;

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
            return Container.StickyClientList.Select(m => m.Value).ToList();
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
                if (Container.StickyClientList.Any(c => c.Value.ClientDatabaseId == clientDatabaseId && c.Value.ChannelId == channelId))
                {
                    var entity = Container.StickyClientList.SingleOrDefault(c => c.Value.ClientDatabaseId == clientDatabaseId && c.Value.ChannelId == channelId);
                    Container.StickyClientList.Remove(entity.Key);
                }

                Container.StickyClientList.Add(Guid.NewGuid(), new StickyClientEntity(clientDatabaseId, channelId, stickTime));
            }
        }

        /// <summary>
        /// Removes the sticky clients.
        /// </summary>
        /// <param name="clientDatabaseId">The client database id.</param>
        /// <param name="tempOnly">if set to <c>true</c> [temp only].</param>
        public void RemoveStickyClients(uint clientDatabaseId, bool tempOnly)
        {
            lock (Container.lockStickyClientList)
            {
                var entities = tempOnly ? Container.StickyClientList.Where(m => m.Value.ClientDatabaseId == clientDatabaseId && m.Value.ChannelId != Repository.Settings.Sticky.Channel) : Container.StickyClientList.Where(m => m.Value.ClientDatabaseId == clientDatabaseId && m.Value.ChannelId == Repository.Settings.Sticky.Channel);
                foreach (var entity in entities.ToList())
                {
                    Container.StickyClientList.Remove(entity.Key);
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
            if (Container.StickyClientList.Any(m => m.Value.ClientDatabaseId == clientDatabaseId && m.Value.ChannelId == Repository.Settings.Sticky.Channel))
            {
                return Repository.Settings.Sticky.Channel;
            }

            lock (Container.lockStickyClientList)
            {
                if (Container.StickyClientList.Any(m => m.Value.ClientDatabaseId == clientDatabaseId))
                {
                    return Container.StickyClientList.First(m => m.Value.ClientDatabaseId == clientDatabaseId).Value.ChannelId;
                }
            }

            return null;
        }

        #endregion
    }
}