namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses
{
    using System.ComponentModel.DataAnnotations;
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
            Stick = new StickControlSettings();
            SeenGroup = new SeenGroupControlSettings();
            SeenModerator = new SeenModeratorControlSettings();
            Moderator = new ModeratorControlSettings();
            Hours = new HourControlSettings();
            Punish = new PunishControlSettings();
            SelfGroup = new SelfGroupControlSettings();
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
        /// <value>The stick.</value>
        public StickControlSettings Stick { get; set; }

        /// <summary>
        /// Gets or sets the seen group.
        /// </summary>
        /// <value>The seen group.</value>
        public SeenGroupControlSettings SeenGroup { get; set; }

        /// <summary>
        /// Gets or sets the seen moderator.
        /// </summary>
        /// <value>The seen moderator.</value>
        public SeenModeratorControlSettings SeenModerator { get; set; }

        /// <summary>
        /// Gets or sets the moderator.
        /// </summary>
        /// <value>The moderator.</value>
        public ModeratorControlSettings Moderator { get; set; }

        /// <summary>
        /// Gets or sets the hour.
        /// </summary>
        /// <value>The hour.</value>
        public HourControlSettings Hours { get; set; }

        /// <summary>
        /// Gets or sets the punish.
        /// </summary>
        /// <value>The punish.</value>
        public PunishControlSettings Punish { get; set; }

        /// <summary>
        /// Gets or sets the self group.
        /// </summary>
        /// <value>
        /// The self group.
        /// </value>
        public SelfGroupControlSettings SelfGroup { get; set; }

        /// <summary>
        /// Gets or sets the help message.
        /// </summary>
        /// <value>The help message.</value>
        [DataType(DataType.MultilineText)]
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
            SeenGroup.ApplySetting(setting.SeenGroup);
            SeenModerator.ApplySetting(setting.SeenModerator);
            Moderator.ApplySetting(setting.Moderator);
            Hours.ApplySetting(setting.Hours);
            Punish.ApplySetting(setting.Punish);
            SelfGroup.ApplySetting(setting.SelfGroup);
        }
    }
}