namespace DirkSarodnick.TS3_Bot.Core.Manager.Features
{
    using System.Linq;
    using Base;
    using Entity;
    using Helper;
    using Repository;
    using Settings;
    using TS3QueryLib.Core.CommandHandling;
    using TS3QueryLib.Core.Server.Notification.EventArgs;

    /// <summary>
    /// Defines the NickManager class.
    /// </summary>
    public class NickManager : DefaultManager
    {
        #region Constructor & Invokation

        /// <summary>
        /// Initializes a new instance of the <see cref="NickManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public NickManager(DataRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// Determines whether this instance can invoke.
        /// </summary>
        /// <returns>True or False</returns>
        public override bool CanInvoke()
        {
            return CanInvoke(new ISettings[]
                                 {
                                     Repository.Settings.BadNickname
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
                                     Repository.Settings.BadNickname
                                 });
        }

        /// <summary>
        /// Invokes this instance.
        /// </summary>
        public override void Invoke()
        {
            KickClientsWithBadNickname();
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientJoinedEventArgs"/> instance containing the event data.</param>
        public override void Invoke(ClientJoinedEventArgs e)
        {
            KickClientsWithBadNickname(e);
        }

        #endregion

        #region Bad Nickname

        /// <summary>
        /// Kicks the clients with bad nickname.
        /// </summary>
        protected void KickClientsWithBadNickname()
        {
            if (!Repository.Settings.BadNickname.Enabled) return;

            foreach (var client in Repository.Client.GetClientList()
                .Where(m => IsBadNickname(m.Nickname)))
            {
                if (!PermissionHelper.IsGranted(Repository.Settings.BadNickname, client.ServerGroups)) continue;

                KickClientForBadNickname(new SimpleClientEntity(client));
            }
        }

        /// <summary>
        /// Kicks the clients with bad nickname.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientJoinedEventArgs"/> instance containing the event data.</param>
        protected void KickClientsWithBadNickname(ClientJoinedEventArgs e)
        {
            if (!Repository.Settings.BadNickname.Enabled) return;

            if (IsBadNickname(e.Nickname))
            {
                var client = new SimpleClientEntity(e);
                if (!PermissionHelper.IsGranted(Repository.Settings.BadNickname, client.ServerGroups)) return;

                KickClientForBadNickname(client);
            }
        }

        /// <summary>
        /// Determines whether [is bad nickname] [the specified nickname].
        /// </summary>
        /// <param name="nickname">The nickname.</param>
        /// <returns>
        /// 	<c>true</c> if [is bad nickname] [the specified nickname]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsBadNickname(string nickname)
        {
            return Repository.Settings.BadNickname.BadNicknames.Any(badNickname => nickname.ToLower().Contains(badNickname.ToLower()));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Kicks the client for bad nickname.
        /// </summary>
        /// <param name="clientEntry">The client entry.</param>
        private void KickClientForBadNickname(SimpleClientEntity clientEntry)
        {
            var context = new MessageContext
                              {
                                  ClientDatabaseId = clientEntry.ClientDatabaseId,
                                  ClientNickname = clientEntry.Nickname
                              };

            QueryRunner.AddComplaint(clientEntry.ClientDatabaseId, Repository.Settings.BadNickname.KickMessage.ToMessage(context));
            QueryRunner.KickClient(clientEntry.ClientId, KickReason.Server, Repository.Settings.BadNickname.KickMessage.ToMessage(context));

            Log(Repository.Settings.BadNickname,
                string.Format("Client '{0}'(id:{1}) kicked for bad nickname.",
                              clientEntry.Nickname, clientEntry.ClientDatabaseId));
        }

        #endregion
    }
}