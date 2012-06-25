namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses
{
    using System.Xml.Serialization;

    /// <summary>
    /// Defines the TeamSpeakServerSettings class.
    /// </summary>
    public class TeamSpeakServerSettings : ServerSettings
    {
        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public ushort Instance { get; set; }

        /// <summary>
        /// Gets or sets the instance port.
        /// </summary>
        /// <value>
        /// The instance port.
        /// </value>
        public uint InstancePort { get; set; }

        /// <summary>
        /// Gets or sets the query port.
        /// </summary>
        /// <value>The query port.</value>
        public ushort QueryPort { get; set; }

        /// <summary>
        /// Gets or sets the guest groups.
        /// </summary>
        /// <value>The guest groups.</value>
        [XmlArray("GuestGroups"), XmlArrayItem("Group")]
        public uint[] GuestGroups { get; set; }

        /// <summary>
        /// Gets the hash.
        /// </summary>
        /// <value>The hash.</value>
        public string Hash
        {
            get
            {
                return string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
                                     this.Host, this.Instance, this.InstancePort, this.QueryPort, this.Username, this.Password);
            }
        }
    }
}