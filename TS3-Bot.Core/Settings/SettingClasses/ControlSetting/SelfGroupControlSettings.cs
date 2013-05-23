namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses.ControlSetting
{
    using System.Xml.Serialization;

    /// <summary>
    /// Defines the SelfGroupControlSettings class.
    /// </summary>
    public class SelfGroupControlSettings : ControlSettingsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelfGroupControlSettings" /> class.
        /// </summary>
        public SelfGroupControlSettings()
        {
            this.AllowedServerGroups = new uint[0];
        }

        /// <summary>
        /// Gets or sets the undo command.
        /// </summary>
        /// <value>
        /// The undo command.
        /// </value>
        public string UndoCommand { get; set; }

        /// <summary>
        /// Gets or sets the server group.
        /// </summary>
        /// <value>
        /// The server group.
        /// </value>
        [XmlArray("AllowedServerGroups"), XmlArrayItem("Group")]
        public uint[] AllowedServerGroups { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(SelfGroupControlSettings setting)
        {
            base.ApplySetting(setting);

            this.AllowedServerGroups = setting.AllowedServerGroups;

            if (!string.IsNullOrEmpty(setting.UndoCommand))
                this.UndoCommand = setting.UndoCommand;
        }
    }
}