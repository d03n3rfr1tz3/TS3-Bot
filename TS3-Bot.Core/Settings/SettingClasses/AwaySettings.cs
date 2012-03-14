namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses
{
    /// <summary>
    /// Defines the AwaySettings class.
    /// </summary>
    public class AwaySettings : SettingsBase
    {
        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        public uint Channel { get; set; }

        /// <summary>
        /// Gets or sets the mute headphone channel.
        /// </summary>
        /// <value>
        /// The mute headphone channel.
        /// </value>
        public uint MuteHeadphoneChannel { get; set; }

        /// <summary>
        /// Gets or sets the mute microphone channel.
        /// </summary>
        /// <value>
        /// The mute microphone channel.
        /// </value>
        public uint MuteMicrophoneChannel { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(AwaySettings setting)
        {
            base.ApplySetting(setting);

            this.Channel = setting.Channel;
            this.MuteHeadphoneChannel = setting.MuteHeadphoneChannel;
            this.MuteMicrophoneChannel = setting.MuteMicrophoneChannel;
        }
    }
}