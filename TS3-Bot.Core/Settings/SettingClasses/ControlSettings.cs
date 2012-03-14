namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses
{
    using ControlSetting;

    /// <summary>
    /// Defines the ControlSettings class.
    /// </summary>
    public class ControlSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlSettings"/> class.
        /// </summary>
        public ControlSettings()
        {
            Help = new HelpControlSettings();
            Seen = new SeenControlSettings();
            Files = new FilesControlSettings();
        }

        /// <summary>
        /// Gets or sets the help.
        /// </summary>
        /// <value>The help.</value>
        public HelpControlSettings Help { get; set; }

        /// <summary>
        /// Gets or sets the seen.
        /// </summary>
        /// <value>The seen.</value>
        public SeenControlSettings Seen { get; set; }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>The files.</value>
        public FilesControlSettings Files { get; set; }

        /// <summary>
        /// Gets or sets the stick.
        /// </summary>
        /// <value>
        /// The stick.
        /// </value>
        public StickControlSettings Stick { get; set; }

        /// <summary>
        /// Gets or sets the help message.
        /// </summary>
        /// <value>The help message.</value>
        public string HelpMessage { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(ControlSettings setting)
        {
            if (!string.IsNullOrEmpty(setting.HelpMessage))
                this.HelpMessage = setting.HelpMessage;

            Help.ApplySetting(setting.Help);
            Seen.ApplySetting(setting.Seen);
            Files.ApplySetting(setting.Files);
            Stick.ApplySetting(setting.Stick);
        }
    }
}