namespace DirkSarodnick.TS3_Bot.Core.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using TS3QueryLib.Core.Server.Entities;
    using TS3QueryLib.Core.Server.Responses;

    /// <summary>
    /// Defines the ServerData class.
    /// </summary>
    public class ServerData : DefaultData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerData"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public ServerData(DataRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// Gets the server list.
        /// </summary>
        /// <returns></returns>
        public List<ServerListItem> GetServerList()
        {
            lock (Container.lockGetServerList)
            {
                return Container.ServerList ?? (Container.ServerList = QueryRunner.GetServerList().ToList());
            }
        }

        /// <summary>
        /// Gets the current server.
        /// </summary>
        /// <returns></returns>
        public ServerInfoResponse GetCurrentServer()
        {
            return QueryRunner.GetServerInfo() ?? new ServerInfoResponse();
        }

        /// <summary>
        /// Gets the server groups.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ServerGroupLight> GetServerGroups()
        {
            lock (Container.lockGetServerGroup)
            {

                return QueryRunner.GetServerGroupList();
            }
        }

        /// <summary>
        /// Gets the server group.
        /// </summary>
        /// <param name="serverGroupId">The server group.</param>
        /// <returns></returns>
        public ServerGroupLight GetServerGroup(uint serverGroupId)
        {
            lock (Container.lockGetServerGroup)
            {
                return Container.ClientServerGroupList.Select(m => m.Value.ServerGroups.FirstOrDefault(s => s.Id == serverGroupId)).FirstOrDefault() ??
                       QueryRunner.GetServerGroupList().FirstOrDefault(m => m.Id == serverGroupId) ??
                       new ServerGroupLight();
            }
        }
    }
}