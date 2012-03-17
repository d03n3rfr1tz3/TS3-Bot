namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses.EventSetting
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the EventBehaviorBase class.
    /// </summary>
    public class EventBehavior
    {
        /// <summary>
        /// Gets or sets the behavior.
        /// </summary>
        /// <value>The behavior.</value>
        public BehaviorType Behavior { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [DataType(DataType.MultilineText)]
        public string TextMessage { get; set; }
    }
}