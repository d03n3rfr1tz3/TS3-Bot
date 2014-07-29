namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses.ControlSetting
{
    /// <summary>
    /// Defines the PunishControlSettings class.
    /// </summary>
    public class PunishControlSettings : ControlSettingsBase
    {
        /// <summary>
        /// Gets or sets the undo command.
        /// </summary>
        /// <value>
        /// The undo command.
        /// </value>
        public string UndoCommand { get; set; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        public uint Channel { get; set; }

        /// <summary>
        /// Gets or sets the server group.
        /// </summary>
        /// <value>
        /// The server group.
        /// </value>
        public uint ServerGroup { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(PunishControlSettings setting)
        {
            base.ApplySetting(setting);

            this.Channel = setting.Channel;
            this.ServerGroup = setting.ServerGroup;

            if (!string.IsNullOrEmpty(setting.UndoCommand))
                this.UndoCommand = setting.UndoCommand;
        }
    }
}