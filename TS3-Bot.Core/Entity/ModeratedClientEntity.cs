namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using System;

    /// <summary>
    /// Defines the ModeratedClientEntity struct.
    /// </summary>
    [Serializable]
    public struct ModeratedClientEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModeratedClientEntity"/> struct.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="moderatorDatabaseId">The moderator database id.</param>
        /// <param name="userDatabaseId">The user database id.</param>
        /// <param name="serverGroup">The server group.</param>
        public ModeratedClientEntity(ModerationType type, uint moderatorDatabaseId, uint userDatabaseId, uint serverGroup)
        {
            Type = type;
            Moderator = moderatorDatabaseId;
            User = userDatabaseId;
            ServerGroup = serverGroup;
            Moderated = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ModerationType Type;

        /// <summary>
        /// Gets or sets the moderator.
        /// </summary>
        /// <value>
        /// The moderator.
        /// </value>
        public uint Moderator;

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public uint User;

        /// <summary>
        /// Gets or sets the server group.
        /// </summary>
        /// <value>
        /// The server group.
        /// </value>
        public uint ServerGroup;

        /// <summary>
        /// Gets or sets the moderated.
        /// </summary>
        /// <value>
        /// The moderated.
        /// </value>
        public DateTime Moderated;
    }
}