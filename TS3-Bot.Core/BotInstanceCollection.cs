namespace DirkSarodnick.TS3_Bot.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Helper;
    using Settings;

    /// <summary>
    /// Defines the BotInstanceCollection class.
    /// </summary>
    public class BotInstanceCollection
    {
        private readonly List<BotInstance> bots;

        /// <summary>
        /// Initializes a new instance of the <see cref="BotInstanceCollection"/> class.
        /// </summary>
        public BotInstanceCollection()
        {
            bots = new List<BotInstance>();
        }

        /// <summary>
        /// Gets the <see cref="TS3_Bot.Core.BotInstance"/> with the specified id.
        /// </summary>
        /// <value>The bot instance.</value>
        public BotInstance this[string id]
        {
            get
            {
                return bots.FirstOrDefault(bot => bot.Settings.Id == id);
            }
        }

        /// <summary>
        /// Gets the <see cref="TS3_Bot.Core.BotInstance"/> with the specified id.
        /// </summary>
        /// <param name="setting">the settings</param>
        /// <value>The bot instance.</value>
        public BotInstance this[InstanceSettings setting]
        {
            get
            {
                return bots.FirstOrDefault(bot => bot.Settings.Id == setting.Id);
            }
        }

        /// <summary>
        /// Adds the specified bot instance.
        /// </summary>
        /// <param name="botInstance">The bot instance.</param>
        public void Add(BotInstance botInstance)
        {
            if (Exists(botInstance.Settings.Id)) return;

            bots.Add(botInstance);
        }

        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <param name="action">The action.</param>
        public void ForEach(Action<BotInstance> action)
        {
            bots.Where(bot => bot.Settings.Enabled).ForEach(action);
        }

        /// <summary>
        /// Removes the specified id.
        /// </summary>
        /// <param name="id">The bot id.</param>
        public void Remove(string id)
        {
            var bot = this[id];
            if (bot != null)
            {
                bot.Dispose();
                bots.Remove(bot);
            }
        }

        /// <summary>
        /// Existses the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>True or False</returns>
        public bool Exists(string id)
        {
            return bots.Any(bot => bot.Settings.Id == id);
        }
    }
}