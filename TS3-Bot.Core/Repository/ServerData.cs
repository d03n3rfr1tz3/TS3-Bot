namespace DirkSarodnick.TS3_Bot.Core.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using TS3QueryLib.Core.Query.HelperClasses;

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
                if (Container.ServerList == null)
                    Container.ServerList = QueryRunner.GetServerList().ToList();

                return Container.ServerList.ToList();
            }
        }
    }
}