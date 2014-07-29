namespace DirkSarodnick.TS3_Bot.Core.Manager.Features
{
    using System;
    using System.Linq;
    using System.Net;
    using Base;
    using Entity;
    using Helper;
    using Repository;
    using Service;
    using Settings;
    using Settings.SettingClasses.EventSetting;
    using TS3QueryLib.Core.CommandHandling;

    public class EventManager : DefaultManager
    {
        #region Constructor & Invokation

        /// <summary>
        /// Initializes a new instance of the <see cref="EventManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public EventManager(DataRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// Determines whether this instance can invoke.
        /// </summary>
        /// <returns>True or False</returns>
        public override bool CanSlowInvoke()
        {
            return CanInvoke(new ISettings[]
                                 {
                                     Repository.Settings.Event
                                 });
        }

        /// <summary>
        /// Invokes this instance.
        /// </summary>
        public override void SlowInvoke()
        {
            RandomEvent();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Randoms the poke event.
        /// </summary>
        protected void RandomEvent()
        {
            if(!Repository.Settings.Event.Enabled) return;

            Repository.Settings.Event.Events.Where(e => e.Enabled).ForEach(RandomEvent);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Randoms the poke event.
        /// </summary>
        /// <param name="setting">The setting.</param>
        private void RandomEvent(EventDefinition setting)
        {
            if (!setting.Enabled) return;

            var databaseIds = Repository.Client.GetClientsFromDatabase().Keys.ToList();
            var databaseId = Repository.Static.Random.Next((int)databaseIds.Min(), (int)databaseIds.Max());
            var client = Repository.Client.GetClientSimple((uint)databaseId);

            if (Repository.Static.GetLastIntervalById(setting.Id).AddMinutes(setting.Interval) < Repository.Static.Now)
            {
                Repository.Static.SetLastIntervalById(setting.Id, Repository.Static.Now);
                if (client != null)
                {
                    setting.EventBehaviors.ForEach(b => InvokeEvent(setting, b, client));

                    Log(setting,
                        string.Format("Event '{0}' successfully triggert for client '{1}'(id:{2}).",
                                      setting.Name, client.Nickname, client.ClientDatabaseId));
                }
            }
        }

        /// <summary>
        /// Invokes the event.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <param name="behavior">The behavior.</param>
        /// <param name="client">The client.</param>
        private void InvokeEvent(EventDefinition setting, EventBehavior behavior, SimpleClientEntity client)
        {
            var message = behavior.TextMessage.ToMessage(new MessageContext
                                                         {
                                                             ClientDatabaseId = client.ClientDatabaseId,
                                                             ClientNickname = client.Nickname,
                                                             EventName = setting.Name
                                                         });

            switch (behavior.Behavior)
            {
                case BehaviorType.Poke:
                    QueryRunner.PokeClient(client.ClientId, message);
                    break;
                case BehaviorType.Message:
                    QueryRunner.SendTextMessage(MessageTarget.Client, client.ClientId, message);
                    break;
                case BehaviorType.WebRequest:
                    SendWebRequest(message);
                    break;
            }
        }

        /// <summary>
        /// Sends the web request.
        /// </summary>
        /// <param name="requestUri">The request URI.</param>
        private static void SendWebRequest(string requestUri)
        {
            try
            {
                WebRequest.Create(requestUri).GetResponse();
            }
            catch (UriFormatException ex)
            {
                LogService.Warning(ex.Message);
            }
            catch (WebException ex)
            {
                LogService.Warning(ex.Message);
            }
            catch (Exception ex)
            {
                LogService.Error(ex);
            }
        }

        #endregion
    }
}