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
            get { return Container.LastConnectionError; }
            set
            {
                Container.LastConnectionError = value;

                Container.LastConnectionWaiting = Container.LastConnectionWaiting*2;
                if (Container.LastConnectionWaiting > 900) Container.LastConnectionWaiting = 900;
            }
        }

        /// <summary>
        /// Gets the next connection time.
        /// </summary>
        /// <value>The next connection time.</value>
        public DateTime NextConnectionTime
        {
            get { return LastConnectionError.AddSeconds(Container.LastConnectionWaiting); }
        }

        /// <summary>
        /// Gets the random.
        /// </summary>
        /// <value>The random.</value>
        public Random Random
        {
            get { return random ?? (random = new Random()); }
        }

        /// <summary>
        /// Variable for the DateTime format
        /// </summary>
        public DateTimeFormatInfo DateTimeFormat
        {
            get { return dateTimeFormat ?? (dateTimeFormat = new CultureInfo(Repository.Settings.Global.Globalization).DateTimeFormat); }
        }

        /// <summary>
        /// Gets the now.
        /// </summary>
        /// <value>The now.</value>
        public DateTime Now
        {
            get
            {
                if (Repository.Container.Now == default(DateTime))
                {
                    Repository.Container.Now = DateTime.UtcNow;
                }

                return Repository.Container.Now;
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