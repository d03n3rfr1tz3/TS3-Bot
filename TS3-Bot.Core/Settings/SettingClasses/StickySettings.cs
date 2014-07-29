namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses
{
    /// <summary>
    /// Defines the StickySettings class.
    /// </summary>
    public class StickySettings : SettingsBase
    {
        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        public uint? Channel { get; set; }

        /// <summary>
        /// Gets or sets the stick time.
        /// </summary>
        /// <value>
        /// The stick time.
        /// </value>
        public uint? StickTime { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(StickySettings setting)
        {
            base.ApplySetting(setting);

            this.Channel = setting.Channel;
            if (setting.StickTime > 0)
            {
                this.StickTime = setting.StickTime;
            }
        }
    }
}