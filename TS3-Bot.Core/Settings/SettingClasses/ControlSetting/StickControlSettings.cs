namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses.ControlSetting
{
    /// <summary>
    /// Defines the StickControlSettings class.
    /// </summary>
    public class StickControlSettings : ControlSettingsBase
    {
        /// <summary>
        /// Gets or sets the undo command.
        /// </summary>
        /// <value>
        /// The undo command.
        /// </value>
        public string UndoCommand { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(StickControlSettings setting)
        {
            base.ApplySetting(setting);

            if (!string.IsNullOrEmpty(setting.UndoCommand))
                this.UndoCommand = setting.UndoCommand;
        }
    }
}