namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses
{
    using System.Collections.Generic;
    using MessageSetting;

    /// <summary>
    /// Defines the MessageSettings class.
    /// </summary>
    public class MessageSettings : SettingsBase
    {
        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        /// <value>The events.</value>
        public List<MessageDefinition> Messages { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(MessageSettings setting)
        {
            base.ApplySetting(setting);

            this.Messages = setting.Messages ?? new List<MessageDefinition>();
        }
    }
}