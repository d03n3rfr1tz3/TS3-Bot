namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses.ControlSetting
{
    using System.ComponentModel.DataAnnotations;
    using TS3QueryLib.Core.CommandHandling;

    /// <summary>
    /// Defines the ControlSettingsBase class.
    /// </summary>
    public class ControlSettingsBase : SettingsBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlSettingsBase"/> class.
        /// </summary>
        public ControlSettingsBase()
        {
            this.Target = MessageTarget.Client;
        }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public string Command { get; set; }

        /// <summary>
        /// Gets or sets the help message.
        /// </summary>
        /// <value>The help message.</value>
        [DataType(DataType.MultilineText)]
        public string HelpMessage { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public MessageTarget Target { get; set; }

        /// <summary>
        /// Gets or sets the target id.
        /// </summary>
        /// <value>
        /// The target id.
        /// </value>
        public uint TargetId { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(ControlSettingsBase setting)
        {
            base.ApplySetting(setting);

            if (!string.IsNullOrEmpty(setting.Command))
                this.Command = setting.Command;
            
            this.Target = setting.Target;
            this.TargetId = setting.TargetId;

            if (!string.IsNullOrEmpty(setting.HelpMessage))
                this.HelpMessage = setting.HelpMessage;
        }
    }
}