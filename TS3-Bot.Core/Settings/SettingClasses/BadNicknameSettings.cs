namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    /// <summary>
    /// Defines the BadNicknameSettings class.
    /// </summary>
    public class BadNicknameSettings : SettingsBase
    {
        /// <summary>
        /// Gets or sets the bad nicknames.
        /// </summary>
        /// <value>The bad nicknames.</value>
        [XmlArray("Blacklist"), XmlArrayItem("Entry")]
        public string[] BadNicknames { get; set; }

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
        public void ApplySetting(BadNicknameSettings setting)
        {
            base.ApplySetting(setting);

            this.BadNicknames = setting.BadNicknames ?? new string[0];
            if (!string.IsNullOrEmpty(setting.KickMessage))
                this.KickMessage = setting.KickMessage;
        }
    }
}