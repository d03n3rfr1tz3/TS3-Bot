namespace DirkSarodnick.TS3_Bot.Core.Service
{
    using System;
    using System.IO;
    using Helper;
    using Settings;

    /// <summary>
    /// Defines the SettingsService class.
    /// </summary>
    public class SettingsService : IDisposable
    {
        private bool disposed;
        private FileStream fileStream;
        private StreamReader streamReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsService"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public SettingsService(string filePath)
        {
            this.fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            this.streamReader = new StreamReader(fileStream, true);

            this.Settings = SettingsSerializer.ReadSettings(BasicHelper.DefaultConfiguration);
            this.Settings.ApplySettings(SettingsSerializer.ReadSettings(streamReader.BaseStream));
            this.Settings.Id = Path.GetFileName(filePath);
            this.Settings.FilePath = filePath;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="SettingsService"/> is reclaimed by garbage collection.
        /// </summary>
        ~SettingsService()
        {
            Dispose();
        }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        protected InstanceSettings Settings { get; private set; }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>The settings.</returns>
        public InstanceSettings Get()
        {
            return this.Settings;
        }

        /// <summary>
        /// Sets the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Set(InstanceSettings settings)
        {
            this.Settings = settings;
            SettingsSerializer.WriteSettings(this.Settings, fileStream);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            this.streamReader.Close();
            this.fileStream.Close();

            this.streamReader.Dispose();
            this.fileStream.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}