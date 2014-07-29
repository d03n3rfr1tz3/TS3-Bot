namespace DirkSarodnick.TS3_Bot.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BotInstanceQueue
    {
        private readonly List<BotInstanceQueueHelper> botQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="BotInstanceQueue"/> class.
        /// </summary>
        public BotInstanceQueue()
        {
            botQueue = new List<BotInstanceQueueHelper>();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerable<string> GetQueue()
        {
            var result = botQueue.Where(m => m.NextInit < DateTime.UtcNow).ToList();
            result.ForEach(m => m.LastInitError = DateTime.UtcNow);
            return result.Select(m => m.File);
        }


        /// <summary>
        /// Adds the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        public void Add(string file)
        {
            if (!botQueue.Any(m => m.File == file))
            {
                botQueue.Add(new BotInstanceQueueHelper(file));
            }
        }

        /// <summary>
        /// Removes the specified file.
        /// </summary>
        /// <param name="file">The file.</param>
        public void Remove(string file)
        {
            if (botQueue.Any(m => m.File == file))
            {
                botQueue.RemoveAll(m => m.File == file);
            }
        }
    }
}