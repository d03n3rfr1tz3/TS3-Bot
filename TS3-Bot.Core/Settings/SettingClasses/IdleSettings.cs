namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the IdleSettings class.
    /// </summary>
    public class IdleSettings : SettingsBase
    {
        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        public uint? Channel { get; set; }

        /// <summary>
        /// Gets or sets the idle time.
        /// </summary>
        /// <value>The idle time.</value>
        public uint? IdleTime { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [DataType(DataType.MultilineText)]
        public string TextMessage { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(IdleSettings setting)
        {
            base.ApplySetting(setting);

            this.Channel = setting.Channel;
            this.IdleTime = setting.IdleTime;

            if (!string.IsNullOrEmpty(setting.TextMessage))
                this.TextMessage = setting.TextMessage;
        }
    }
}