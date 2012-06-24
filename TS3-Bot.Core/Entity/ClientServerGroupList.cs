namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using System;
    using System.Collections.Generic;
    using TS3QueryLib.Core.Query.HelperClasses;

    /// <summary>
    /// Defines the ClientServerGroupList class.
    /// </summary>
    public class ClientServerGroupList
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientServerGroupList"/> class.
        /// </summary>
        /// <param name="serverGroups">The server groups.</param>
        public ClientServerGroupList(List<ServerGroupLight> serverGroups)
        {
            Creation = DateTime.UtcNow;
            ServerGroups = serverGroups;
        }

        /// <summary>
        /// Gets or sets the creation.
        /// </summary>
        /// <value>The creation.</value>
        public DateTime Creation { get; set; }

        /// <summary>
        /// Gets or sets the server groups.
        /// </summary>
        /// <value>The server groups.</value>
        public List<ServerGroupLight> ServerGroups { get; set; }
	}
}