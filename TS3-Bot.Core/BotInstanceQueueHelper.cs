namespace DirkSarodnick.TS3_Bot.Core
{
    using System;

    /// <summary>
    /// Defines the BotInstanceQueueHelper class.
    /// </summary>
    public class BotInstanceQueueHelper
    {
        private DateTime lastInitError;

        /// <summary>
        /// Initializes a new instance of the <see cref="BotInstanceQueueHelper"/> class.
        /// </summary>
        /// <param name="file">The file.</param>
        public BotInstanceQueueHelper(string file)
        {
            File = file;
            LastInitError = DateTime.Now;
            LastInitWaiting = 1;
        }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>The file.</value>
        public string File { get; set; }

        /// <summary>
        /// Gets or sets the last init.
        /// </summary>
        /// <value>The last init.</value>
        public DateTime LastInitError
        {
            get { return lastInitError; }

            set
            {
                lastInitError = value;

                LastInitWaiting = LastInitWaiting*2;
                if (LastInitWaiting > 900) LastInitWaiting = 900;
            }
        }

        /// <summary>
        /// Gets or sets the init count.
        /// </summary>
        /// <value>The init count.</value>
        public int LastInitWaiting { get; set; }

        /// <summary>
        /// Gets the next init.
        /// </summary>
        /// <value>The next init.</value>
        public DateTime NextInit
        {
            get
            {
                return LastInitError.AddSeconds(LastInitWaiting);
            }
        }
    }
}