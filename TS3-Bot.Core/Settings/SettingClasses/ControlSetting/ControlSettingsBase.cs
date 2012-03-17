namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses.ControlSetting
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the ControlSettingsBase class.
    /// </summary>
    public class ControlSettingsBase : SettingsBase
    {
        /// <summary>
        /// Gets or sets the help message.
        /// </summary>
        /// <value>The help message.</value>
        [DataType(DataType.MultilineText)]
        public string HelpMessage { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(ControlSettingsBase setting)
        {
            base.ApplySetting(setting);

            if (!string.IsNullOrEmpty(setting.HelpMessage))
                this.HelpMessage = setting.HelpMessage;
        }
    }
}