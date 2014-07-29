namespace DirkSarodnick.TS3_Bot.Core.Manager.Features
{
    using System.Linq;
    using Base;
    using Entity;
    using Helper;
    using Repository;
    using Settings;
    using Settings.SettingClasses.MessageSetting;
    using TS3QueryLib.Core.CommandHandling;
    using TS3QueryLib.Core.Server.Notification.EventArgs;

    /// <summary>
    /// Defines the MessageManager class.
    /// </summary>
    public class MessageManager : DefaultManager
    {
        #region Constructor & Invokation

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public MessageManager(DataRepository repository) : base(repository)
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
                                     Repository.Settings.Message
                                 });
        }

        /// <summary>
        /// Determines whether this instance can invoke the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientJoinedEventArgs"/> instance containing the event data.</param>
        /// <returns>
        ///   <c>true</c> if this instance can invoke the specified e; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanInvoke(ClientJoinedEventArgs e)
        {
            return CanInvoke(new ISettings[]
                                 {
                                     Repository.Settings.Message
                                 });
        }

        /// <summary>
        /// Invokes this instance.
        /// </summary>
        public override void SlowInvoke()
        {
            Repository.Settings.Message.Messages.Where(e => e.Enabled && e.MessageType == MessageType.Advert).ForEach(AdvertMessage);
            Repository.Settings.Message.Messages.Where(e => e.Enabled && e.MessageType == MessageType.Global).ForEach(GlobalAdvertMessage);
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientJoinedEventArgs"/> instance containing the event data.</param>
        public override void Invoke(ClientJoinedEventArgs e)
        {
            Repository.Settings.Message.Messages.Where(m => m.Enabled && m.MessageType == MessageType.Welcome).ForEach(m => WelcomeMessage(e, m));
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Welcomes the message.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientJoinedEventArgs"/> instance containing the event data.</param>
        /// <param name="message">The message.</param>
        protected void WelcomeMessage(ClientJoinedEventArgs e, MessageDefinition message)
        {
            if (!message.Enabled) return;

            if (e.ClientType == 0)
                QueryRunner.SendTextMessage(
                    MessageTarget.Channel, message.Channel,
                    message.TextMessage.ToMessage(new MessageContext
                                                  {
                                                      ClientDatabaseId = e.ClientDatabaseId,
                                                      ClientNickname = e.Nickname
                                                  }));

            Log(message, string.Format("Welcome message successfully sent to client '{0}'(id:{1}).", e.Nickname, e.ClientDatabaseId));
        }

        /// <summary>
        /// Adverts the message.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void AdvertMessage(MessageDefinition message)
        {
            if (!message.Enabled) return;

            if (Repository.Static.GetLastIntervalById(message.Id).AddMinutes(message.Interval) < Repository.Static.Now)
            {
                Repository.Static.SetLastIntervalById(message.Id, Repository.Static.Now);
                QueryRunner.SendTextMessage(
                    MessageTarget.Server, Repository.Connection.CredentialEntity.Self.VirtualServerId,
                    message.TextMessage.ToMessage(new MessageContext
                                                  {
                                                      ServerId = Repository.Connection.CredentialEntity.Self.VirtualServerId
                                                  }));

                Log(message, string.Format("Advert message successfully sent to server '{0}'.", Repository.Connection.CredentialEntity.Self.VirtualServerId));
            }
        }

        /// <summary>
        /// Globals the advert message.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void GlobalAdvertMessage(MessageDefinition message)
        {
            if (!message.Enabled) return;

            if (Repository.Static.GetLastIntervalById(message.Id).AddMinutes(message.Interval) < Repository.Static.Now)
            {
                Repository.Static.SetLastIntervalById(message.Id, Repository.Static.Now);
                QueryRunner.SendGlobalMessage(message.TextMessage.ToMessage());

                Log(message, string.Format("Global advert message successfully sent."));
            }
        }

        #endregion
    }
}