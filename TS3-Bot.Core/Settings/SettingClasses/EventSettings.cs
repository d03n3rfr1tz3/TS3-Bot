namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses
{
    using System.Collections.Generic;
    using EventSetting;

    /// <summary>
    /// Defines the EventSettings class.
    /// </summary>
    public class EventSettings : SettingsBase
    {
        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        /// <value>The events.</value>
        public List<EventDefinition> Events { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(EventSettings setting)
        {
            base.ApplySetting(setting);

            this.Events = setting.Events ?? new List<EventDefinition>();
        }
    }
}