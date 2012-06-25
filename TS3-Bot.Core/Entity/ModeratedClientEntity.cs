namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using System;
    using System.Text.RegularExpressions;
    using TS3QueryLib.Core.Server.Entities;

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

        /// <summary>
        /// Defines the Regex for Log Entries, that will be used to parse ModeratedClientEntity of it.
        /// </summary>
        private static readonly Regex LogRegex = new Regex(@"client \(id:(?<clientId>.*)\) was (?<type>.*) (?<typeAdd>.*) servergroup '(?<groupName>.*)'\(id:(?<groupId>.*)\) by client '(?<moderatorName>.*)'\(id:(?<moderatorId>.*)\)", RegexOptions.IgnoreCase);

        /// <summary>
        /// Parses the specified log entry.
        /// </summary>
        /// <param name="logEntry">The log entry.</param>
        /// <returns>Returns parsed ModeratedClientEntity.</returns>
        public static ModeratedClientEntity? Parse(LogEntry logEntry)
        {
            var match = LogRegex.Match(logEntry.Message);

            try
            {
                ModerationType type;
                if (Enum.TryParse(match.Groups["type"].Value, true, out type))
                {
                    return new ModeratedClientEntity(type,
                                                     uint.Parse(match.Groups["moderatorId"].Value),
                                                     uint.Parse(match.Groups["clientId"].Value),
                                                     uint.Parse(match.Groups["groupId"].Value));
                }
            }
            catch { }

            return null;
        }
    }
}