
namespace DirkSarodnick.TS3_Bot.Core.Settings
{
    /// <summary>
    /// Defines the ISettings interface.
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether the based settings is enabled or not.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the logging is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        bool LogEnabled { get; set; }

        /// <summary>
        /// Gets or sets the permitted server groups.
        /// </summary>
        /// <value>The permitted server groups.</value>
        uint[] PermittedServerGroups { get; set; }

        /// <summary>
        /// Gets or sets the denied server groups.
        /// </summary>
        /// <value>The denied server groups.</value>
        uint[] DeniedServerGroups { get; set; }

        /// <summary>
        /// Applies the setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        void ApplySetting(ISettings setting);
    }
}