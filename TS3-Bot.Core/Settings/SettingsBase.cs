namespace DirkSarodnick.TS3_Bot.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    /// <summary>
    /// Defines the SettingsBase class.
    /// </summary>
    [Serializable]
    public class SettingsBase : ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether the based settings is enabled or not.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the logging is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [XmlAttribute]
        public bool LogEnabled { get; set; }

        /// <summary>
        /// Gets or sets the permitted server groups.
        /// </summary>
        /// <value>The permitted server groups.</value>
        [XmlArray("Permitted"), XmlArrayItem("Group")]
        public uint[] PermittedServerGroups { get; set; }

        /// <summary>
        /// Gets or sets the denied server groups.
        /// </summary>
        /// <value>The denied server groups.</value>
        [XmlArray("Denied"), XmlArrayItem("Group")]
        public uint[] DeniedServerGroups { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public virtual void ApplySetting(ISettings setting)
        {
            this.Enabled = setting.Enabled;
            this.LogEnabled = setting.LogEnabled;
            this.PermittedServerGroups = setting.PermittedServerGroups ?? new uint[0];
            this.DeniedServerGroups = setting.DeniedServerGroups ?? new uint[0];
        }
    }
}