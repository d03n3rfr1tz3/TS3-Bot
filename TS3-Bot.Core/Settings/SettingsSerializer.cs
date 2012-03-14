namespace DirkSarodnick.TS3_Bot.Core.Settings
{
    using System.IO;
    using System.Xml.Serialization;
    using Microsoft.Xml.Serialization.GeneratedAssembly;

    /// <summary>
    /// Defines the SettingsSerializer class.
    /// </summary>
    public static class SettingsSerializer
    {
        private static XmlSerializer serializer;

        /// <summary>
        /// Gets the serializer.
        /// </summary>
        /// <value>The serializer.</value>
        private static XmlSerializer Serializer
        {
            get
            {
                if (serializer == null)
                {
                    serializer = new InstanceSettingsSerializer();
                }

                return serializer;
            }
        }

        /// <summary>
        /// Reads the settings.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public static InstanceSettings ReadSettings(Stream stream)
        {
            lock (Serializer)
            {
                return Serializer.Deserialize(stream) as InstanceSettings;
            }
        }

        /// <summary>
        /// Writes the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        public static Stream WriteSettings(InstanceSettings settings)
        {
            lock (Serializer)
            {
                var result = new MemoryStream();
                Serializer.Serialize(result, settings);
                result.Seek(0, SeekOrigin.Begin);
                return result;
            }
        }

        /// <summary>
        /// Writes the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="path">The path.</param>
        public static void WriteSettings(InstanceSettings settings, Stream stream)
        {
            lock (Serializer)
            {
                var result = new StreamWriter(stream);
                Serializer.Serialize(result, settings);
            }
        }
    }
}