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
    /// Defines the RecordManager class.
    /// </summary>
    public class RecordManager : DefaultManager
    {
        #region Constructor & Invokation

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public RecordManager(DataRepository repository) : base(repository)
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
                                     Repository.Settings.Record
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
                                     Repository.Settings.Record
                                 });
        }

        /// <summary>
        /// Invokes this instance.
        /// </summary>
        public override void Invoke()
        {
            KickRecordingClients();
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientJoinedEventArgs"/> instance containing the event data.</param>
        public override void Invoke(ClientJoinedEventArgs e)
        {
            KickRecordingClients(e);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Kicks the recording clients.
        /// </summary>
        protected void KickRecordingClients()
        {
            if (!Repository.Settings.Record.Enabled) return;

            foreach (var client in Repository.Client.GetClientList()
                .Where(m => m.IsClientRecording.HasValue && m.IsClientRecording.Value))
            {
                if (!PermissionHelper.IsGranted(Repository.Settings.Record, client.ServerGroups)) continue;

                PunishClientForRecord(new SimpleClientEntity(client));
            }
        }

        /// <summary>
        /// Kicks the recording clients.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientJoinedEventArgs"/> instance containing the event data.</param>
        protected void KickRecordingClients(ClientJoinedEventArgs e)
        {
            if (!Repository.Settings.Record.Enabled) return;

            if (e.ClientType == 0 && e.IsRecording)
            {
                var client = new SimpleClientEntity(e);
                if (!PermissionHelper.IsGranted(Repository.Settings.Record, e.ServerGroups)) return;

                PunishClientForRecord(client);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Punishes the client for record.
        /// </summary>
        /// <param name="clientEntry">The client entry.</param>
        private void PunishClientForRecord(SimpleClientEntity clientEntry)
        {
            var context = new MessageContext
                              {
                                  ClientDatabaseId = clientEntry.ClientDatabaseId,
                                  ClientNickname = clientEntry.Nickname
                              };

            QueryRunner.PokeClient(clientEntry.ClientId, Repository.Settings.Record.KickMessage.ToMessage(context));
            switch(Repository.Settings.Record.Behavior)
            {
                case PunishBehavior.KickFromChannel:
                    QueryRunner.KickClient(clientEntry.ClientId, KickReason.Channel, Repository.Settings.Record.KickMessage.ToMessage(context));
                    break;
                case PunishBehavior.KickFromServer:
                    QueryRunner.KickClient(clientEntry.ClientId, KickReason.Server, Repository.Settings.Record.KickMessage.ToMessage(context));
                    break;
                case PunishBehavior.MoveToSticky:
                    Repository.Channel.AddStickyClients(clientEntry.ClientDatabaseId, Repository.Settings.Sticky.Channel.GetValueOrDefault(), Repository.Settings.Sticky.StickTime.GetValueOrDefault());
                    break;
            }

            Log(Repository.Settings.Record,
                string.Format("Client '{0}'(id:{1}) punished for recording.",
                              clientEntry.Nickname, clientEntry.ClientDatabaseId));
        }

        #endregion
    }
}