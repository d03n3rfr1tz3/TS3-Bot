namespace DirkSarodnick.TS3_Bot.Core.Settings
{
    using System.IO;
    using Helper;

    /// <summary>
    /// Defines the SettingsManager class.
    /// </summary>
    public class SettingsManager
    {
        /// <summary>
        /// Loads the settings.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The instance settings</returns>
        public static InstanceSettings LoadSettings(string path)
        {
            InstanceSettings result = SettingsSerializer.ReadSettings(BasicHelper.DefaultConfiguration);

            using (var streamReader = new StreamReader(path, true))
            {
                result.ApplySettings(SettingsSerializer.ReadSettings(streamReader.BaseStream));
            }

            result.Id = Path.GetFileName(path);
            result.FilePath = path;
            return result;
        }
    }
}