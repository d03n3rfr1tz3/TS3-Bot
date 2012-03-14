namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses.EventSetting
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Defines the EventDefinition class.
    /// </summary>
    public class EventDefinition : SettingsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventDefinition"/> class.
        /// </summary>
        public EventDefinition()
        {
            this.Id = Guid.NewGuid();
            this.EventBehaviors = new List<EventBehavior>();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [XmlIgnore]
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>The interval.</value>
        public int Interval { get; set; }

        /// <summary>
        /// Gets or sets the behavior.
        /// </summary>
        /// <value>The behavior.</value>
        public List<EventBehavior> EventBehaviors { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(EventDefinition setting)
        {
            base.ApplySetting(setting);

            this.Interval = setting.Interval;
            if (!string.IsNullOrEmpty(setting.Name))
                this.Name = setting.Name;
            this.EventBehaviors = setting.EventBehaviors ?? new List<EventBehavior>();
        }
    }
}