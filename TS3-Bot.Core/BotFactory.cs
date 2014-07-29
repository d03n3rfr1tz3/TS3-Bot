
namespace DirkSarodnick.TS3_Bot.Core
{
    using System;
    using System.IO;
    using System.Threading;
    using Helper;
    using Service;
    using Settings;

    /// <summary>
    /// Defines the BotFactory class.
    /// </summary>
    public static class BotFactory
    {
        #region Fields & Properties

        private static readonly object lockBots = new object();
        private static readonly object lockQueue = new object();

        private static BotInstanceCollection bots;
        private static BotInstanceQueue botQueue;
        private static FileSystemWatcher fileWatcher;
        private static readonly SynchronizationContext SyncContext = new SynchronizationContext();

        /// <summary>
        /// Gets or sets the bots.
        /// </summary>
        /// <value>The bots.</value>
        public static BotInstanceCollection Bots
        {
            get
            {
                lock (lockBots)
                {
                    if (bots == null)
                    {
                        bots = new BotInstanceCollection();
                        InitInstances();
                    }

                    return bots;
                }
            }
        }

        /// <summary>
        /// Gets the queue.
        /// </summary>
        /// <value>The queue.</value>
        public static BotInstanceQueue Queue
        {
            get
            {
                lock (lockQueue)
                {
                    return botQueue ?? (botQueue = new BotInstanceQueue());
                }
            }
        }

        /// <summary>
        /// Gets the file watcher.
        /// </summary>
        /// <value>The file watcher.</value>
        public static FileSystemWatcher FileWatcher
        {
            get
            {
                if (fileWatcher == null)
                {
                    fileWatcher = new FileSystemWatcher(BasicHelper.ConfigurationDirectory, "*.xml");
                    FileWatcher.Created += FileWatcher_Created;
                    FileWatcher.Changed += FileWatcher_Changed;
                    FileWatcher.Deleted += FileWatcher_Deleted;
                    FileWatcher.Renamed += FileWatcher_Renamed;
                }

                return fileWatcher;
            }
        }

        #endregion

        /// <summary>
        /// Executes the tick in all Bot instances.
        /// </summary>
        public static void Tick()
        {
            if (SynchronizationContext.Current == null) SynchronizationContext.SetSynchronizationContext(SyncContext);

            ReCheck();
            Bots.ForEach(bot => bot.Tick(SyncContext));
        }

        /// <summary>
        /// Inits the instances.
        /// </summary>
        private static void InitInstances()
        {
            try
            {
                foreach (var file in Directory.GetFiles(BasicHelper.ConfigurationDirectory, "*.xml"))
                {
                    CreateBot(file);
                }

                FileWatcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                LogService.Error(ex.ToString());
            }
        }

        /// <summary>
        /// Res the check.
        /// </summary>
        private static void ReCheck()
        {
            foreach (var file in Queue.GetQueue())
            {
                ChangeBot(file);
            }
        }

        #region File Events

        /// <summary>
        /// Handles the Created event of the FileWatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.IO.FileSystemEventArgs"/> instance containing the event data.</param>
        private static void FileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            CreateBot(e.FullPath);
        }

        /// <summary>
        /// Handles the Changed event of the FileWatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.IO.FileSystemEventArgs"/> instance containing the event data.</param>
        private static void FileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            ChangeBot(e.FullPath);
        }

        /// <summary>
        /// Handles the Deleted event of the FileWatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.IO.FileSystemEventArgs"/> instance containing the event data.</param>
        private static void FileWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            RemoveBot(e.FullPath);
        }

        /// <summary>
        /// Handles the Renamed event of the FileWatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.IO.RenamedEventArgs"/> instance containing the event data.</param>
        private static void FileWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            RenameBot(e.FullPath, e.OldFullPath);
        }

        #endregion

        #region Bot Methods

        /// <summary>
        /// Creates the bot.
        /// </summary>
        /// <param name="file">The file.</param>
        private static void CreateBot(string file)
        {
            try
            {
                Bots.Add(new BotInstance(SettingsManager.LoadSettings(file)));
                Queue.Remove(file);
            }
            catch (InvalidOperationException ex)
            {
                LogService.Warning(ex.InnerException == null
                                       ? string.Format("[{0}] {1}", Path.GetFileName(file), ex.Message)
                                       : string.Format("[{0}] {1}: {2}", Path.GetFileName(file), ex.Message, ex.InnerException.Message));
                Queue.Add(file);
            }
            catch (IOException ex)
            {
                LogService.Warning(string.Format("{0} File will be queued for later reading.", ex.Message));
                Queue.Add(file);
            }
            catch (Exception ex)
            {
                LogService.Warning(ex.ToString());
            }
        }

        /// <summary>
        /// Changes the bot.
        /// </summary>
        /// <param name="file">The file.</param>
        private static void ChangeBot(string file)
        {
            try
            {
                var settings = SettingsManager.LoadSettings(file);
                var bot = Bots[settings];

                if (bot == default(BotInstance))
                {
                    Bots.Add(new BotInstance(settings));
                }
                else
                {
                    bot.ChangeSettings(settings);
                }

                Queue.Remove(file);
            }
            catch (InvalidOperationException ex)
            {
                LogService.Warning(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                Queue.Add(file);
            }
            catch (IOException ex)
            {
                LogService.Warning(string.Format("{0} File will be queued for later reading.", ex.Message));
                Queue.Add(file);
            }
            catch (Exception ex)
            {
                LogService.Warning(ex.ToString());
            }
        }

        /// <summary>
        /// Removes the bot.
        /// </summary>
        /// <param name="file">The file.</param>
        private static void RemoveBot(string file)
        {
            try
            {
                Bots.Remove(Path.GetFileName(file));
            }
            catch (Exception ex)
            {
                LogService.Warning(ex.ToString());
            }
        }

        /// <summary>
        /// Renames the bot.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="oldFile">The old file.</param>
        private static void RenameBot(string file, string oldFile)
        {
            try
            {
                var bot = Bots[Path.GetFileName(oldFile)];
                if (bot != default(BotInstance))
                {
                    bot.Settings.Id = Path.GetFileName(file);
                    bot.Settings.FilePath = file;
                }
            }
            catch (Exception ex)
            {
                LogService.Error(ex.ToString());
            }
        }

        #endregion
    }
}