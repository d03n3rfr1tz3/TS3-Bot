namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses
{
    using System.ComponentModel.DataAnnotations;

    public class ServerSettings
    {
        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>The host.</value>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}