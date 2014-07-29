namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses.ControlSetting
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the FilesControlSettings class.
    /// </summary>
    public class FilesControlSettings : ControlSettingsBase
    {
        /// <summary>
        /// Gets or sets the message per server.
        /// </summary>
        /// <value>The message per server.</value>
        [DataType(DataType.MultilineText)]
        public string MessagePerServer { get; set; }

        /// <summary>
        /// Gets or sets the message no files found.
        /// </summary>
        /// <value>The message no files found.</value>
        [DataType(DataType.MultilineText)]
        public string MessageNoFilesFound { get; set; }

        /// <summary>
        /// Gets or sets the message files found.
        /// </summary>
        /// <value>The message files found.</value>
        [DataType(DataType.MultilineText)]
        public string MessageFilesFound { get; set; }

        /// <summary>
        /// Gets or sets the message file.
        /// </summary>
        /// <value>The message file.</value>
        [DataType(DataType.MultilineText)]
        public string MessageFile { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        public void ApplySetting(FilesControlSettings setting)
        {
            base.ApplySetting(setting);

            if (!string.IsNullOrEmpty(setting.MessagePerServer))
                this.MessagePerServer = setting.MessagePerServer;
            if (!string.IsNullOrEmpty(setting.MessageNoFilesFound))
                this.MessageNoFilesFound = setting.MessageNoFilesFound;
            if (!string.IsNullOrEmpty(setting.MessageFilesFound))
                this.MessageFilesFound = setting.MessageFilesFound;
            if (!string.IsNullOrEmpty(setting.MessageFile))
                this.MessageFile = setting.MessageFile;
        }
    }
}