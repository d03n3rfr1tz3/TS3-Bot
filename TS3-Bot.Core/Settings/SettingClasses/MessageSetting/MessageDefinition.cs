namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses.MessageSetting
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    /// <summary>
    /// Defines the EventDefinition class.
    /// </summary>
    public class MessageDefinition : SettingsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageDefinition"/> class.
        /// </summary>
        public MessageDefinition()
        {
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [XmlIgnore]
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>The interval.</value>
        public int Interval { get; set; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        public uint Channel { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [DataType(DataType.MultilineText)]
        public string TextMessage { get; set; }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>The type of the message.</value>
        public MessageType MessageType { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(MessageDefinition setting)
        {
            base.ApplySetting(setting);

            this.MessageType = setting.MessageType;
            this.Interval = setting.Interval;
        }
    }
}