
namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses
{
    using System;

    /// <summary>
    /// Defines the GlobalSettings class.
    /// </summary>
    [Serializable]
    public class GlobalSettings
    {
        /// <summary>
        /// Gets or sets the globalization.
        /// </summary>
        /// <value>The globalization.</value>
        public string Globalization { get; set; }

        /// <summary>
        /// Gets or sets the bot nickname.
        /// </summary>
        /// <value>The bot nickname.</value>
        public string BotNickname { get; set; }
    }
}