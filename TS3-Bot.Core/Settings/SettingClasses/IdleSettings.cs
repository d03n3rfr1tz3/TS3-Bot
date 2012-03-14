namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses
{
    /// <summary>
    /// Defines the IdleSettings class.
    /// </summary>
    public class IdleSettings : SettingsBase
    {
        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        public uint Channel { get; set; }

        /// <summary>
        /// Gets or sets the idle time.
        /// </summary>
        /// <value>The idle time.</value>
        public uint IdleTime { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(IdleSettings setting)
        {
            base.ApplySetting(setting);

            this.Channel = setting.Channel;
            this.IdleTime = setting.IdleTime;
        }
    }
}