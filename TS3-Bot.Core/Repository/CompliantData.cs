namespace DirkSarodnick.TS3_Bot.Core.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using TS3QueryLib.Core.Server.Entities;

    /// <summary>
    /// Defines the CompliantData class.
    /// </summary>
    public class CompliantData : DefaultData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompliantData"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public CompliantData(DataRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// Gets the compliant list.
        /// </summary>
        /// <returns></returns>
        public List<ComplainListEntry> GetCompliantList()
        {
            lock (Container.lockGetCompliantList)
            {
                if (Container.CompliantList == null)
                    Container.CompliantList = QueryRunner.GetComplainList().ToList();

                return Container.CompliantList.ToList();
            }
        }
    }
}