namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses
{
    /// <summary>
    /// Defines the VoteSettings class.
    /// </summary>
    public class VoteSettings : SettingsBase
    {
        /// <summary>
        /// Gets or sets the needed compliants.
        /// </summary>
        /// <value>The needed compliants.</value>
        public uint NeededCompliants { get; set; }

        /// <summary>
        /// Gets or sets the behavior.
        /// </summary>
        /// <value>The behavior.</value>
        public PunishBehavior Behavior { get; set; }

        /// <summary>
        /// Gets or sets the kick message.
        /// </summary>
        /// <value>The kick message.</value>
        public string KickMessage { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(VoteSettings setting)
        {
            base.ApplySetting(setting);

            this.Behavior = setting.Behavior;
            this.KickMessage = setting.KickMessage;
            this.NeededCompliants = setting.NeededCompliants;
        }
    }
}