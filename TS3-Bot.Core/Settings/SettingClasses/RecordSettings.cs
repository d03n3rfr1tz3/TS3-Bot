namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the RecordSettings class.
    /// </summary>
    public class RecordSettings : SettingsBase
    {
        /// <summary>
        /// Gets or sets the behavior.
        /// </summary>
        /// <value>The behavior.</value>
        public PunishBehavior Behavior { get; set; }

        /// <summary>
        /// Gets or sets the kick message.
        /// </summary>
        /// <value>The kick message.</value>
        [DataType(DataType.MultilineText)]
        public string KickMessage { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(RecordSettings setting)
        {
            base.ApplySetting(setting);

            this.Behavior = setting.Behavior;
            if (!string.IsNullOrEmpty(setting.KickMessage))
                this.KickMessage = setting.KickMessage;
        }
    }
}