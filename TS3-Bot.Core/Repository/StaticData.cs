namespace DirkSarodnick.TS3_Bot.Core.Repository
{
    using System;
    using System.Globalization;
    using Base;

    /// <summary>
    /// Defines the StaticData class.
    /// </summary>
    public class StaticData : DefaultData
    {
        private Random random;
        private DateTimeFormatInfo dateTimeFormat;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticData"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public StaticData(DataRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// Gets or sets the last connection error.
        /// </summary>
        /// <value>The last connection error.</value>
        public DateTime LastConnectionError
        {
            get { return Container.lastConnectionError; }
            set
            {
                Container.lastConnectionError = value;

                Container.lastConnectionWaiting = Container.lastConnectionWaiting*2;
                if (Container.lastConnectionWaiting > 900) Container.lastConnectionWaiting = 900;
            }
        }

        /// <summary>
        /// Gets the next connection time.
        /// </summary>
        /// <value>The next connection time.</value>
        public DateTime NextConnectionTime
        {
            get
            {
                return LastConnectionError.AddSeconds(Container.lastConnectionWaiting);
            }
        }

        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <value>The random.</value>
        public Random Random
        {
            get
            {
                if (random == null)
                {
                    random = new Random();
                }

                return random;
            }
        }

        /// <summary>
        /// Variable for the DateTime format
        /// </summary>
        public DateTimeFormatInfo DateTimeFormat
        {
            get
            {
                if (dateTimeFormat == null)
                {
                    dateTimeFormat = new CultureInfo(Repository.Settings.Global.Globalization).DateTimeFormat;
                }

                return dateTimeFormat;
            }
        }

        /// <summary>
        /// Gets the now.
        /// </summary>
        /// <value>The now.</value>
        public DateTime Now
        {
            get
            {
                if (Repository.Container.now == default(DateTime))
                {
                    Repository.Container.now = DateTime.Now;
                }

                return Repository.Container.now;
            }
        }

        /// <summary>
        /// Gets the last interval by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public DateTime GetLastIntervalById(Guid id)
        {
            lock (Repository.Container.lockLastEventList)
            {
                if (!Repository.Container.LastIntervalList.ContainsKey(id))
                {
                    Repository.Container.LastIntervalList.Add(id, Repository.Static.Now);
                }

                return Repository.Container.LastIntervalList[id];
            }
        }

        /// <summary>
        /// Sets the last interval by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="dateTime">The date time.</param>
        public void SetLastIntervalById(Guid id, DateTime dateTime)
        {
            lock (Repository.Container.lockLastEventList)
            {
                if (Repository.Container.LastIntervalList.ContainsKey(id))
                {
                    Repository.Container.LastIntervalList[id] = dateTime;
                }
                else
                {
                    Repository.Container.LastIntervalList.Add(id, dateTime);
                }
            }
        }
    }
}