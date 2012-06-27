namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses.ControlSetting
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the HourControlSettings class.
    /// </summary>
    public class HourControlSettings : ControlSettingsBase
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [DataType(DataType.MultilineText)]
        public string TextMessage { get; set; }

        /// <summary>
        /// Gets or sets the message per client.
        /// </summary>
        /// <value>
        /// The message per client.
        /// </value>
        [DataType(DataType.MultilineText)]
        public string MessagePerClient { get; set; }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        /// <value>The limit.</value>
        public int Limit { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(HourControlSettings setting)
        {
            base.ApplySetting(setting);

            if (setting.Limit > 0) this.Limit = setting.Limit;
            if (!string.IsNullOrEmpty(setting.TextMessage))
                this.TextMessage = setting.TextMessage;
            if (!string.IsNullOrEmpty(setting.MessagePerClient))
                this.MessagePerClient = setting.MessagePerClient;
        }
    }
}