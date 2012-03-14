namespace DirkSarodnick.TS3_Bot.Core.Manager.Features
{
    using System.Linq;
    using Base;
    using Entity;
    using Helper;
    using Repository;
    using Settings;
    using TS3QueryLib.Core.CommandHandling;
    using TS3QueryLib.Core.Query.Notification.EventArgs;

    /// <summary>
    /// Defines the CompliantManager class.
    /// </summary>
    public class CompliantManager : DefaultManager
    {
        #region Constructor & Invokation

        /// <summary>
        /// Initializes a new instance of the <see cref="CompliantManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CompliantManager(DataRepository repository) : base(repository)
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
                                     Repository.Settings.Vote
                                 });
        }

        /// <summary>
        /// Invokes this instance.
        /// </summary>
        public override void Invoke()
        {
            PunishVoted();
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.ClientMovedEventArgs"/> instance containing the event data.</param>
        public override void Invoke(ClientMovedEventArgs e)
        {
            PunishVoted(e);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Moves the voted to sticky.
        /// </summary>
        protected void PunishVoted()
        {
            if (!Repository.Settings.Vote.Enabled) return;

            foreach (var compliantList in Repository.Compliant.GetCompliantList()
                .Where(m => m.Created.AddHours(1) < Repository.Static.Now)
                .GroupBy(m => m.TargetClientDatabaseId)
                .Where(m => m.Select(n => n.SourceClientDatabaseId).Distinct().Count() >= Repository.Settings.Vote.NeededCompliants))
            {
                var compliant = compliantList.First();
                var client = Repository.Client.GetClientSimple(compliant.TargetClientDatabaseId);
                if (client != null)
                {
                    if (!PermissionHelper.IsGranted(Repository.Settings.Vote, client.ServerGroups)) continue;
                    PunishClient(client);
                }
            }
        }

        /// <summary>
        /// Punishes the voted.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.ClientMovedEventArgs"/> instance containing the event data.</param>
        protected void PunishVoted(ClientMovedEventArgs e)
        {
            if (!Repository.Settings.Vote.Enabled) return;

            var clientEntry = Repository.Client.GetClientInfo(e.ClientId);
            if(!Repository.Client.IsClientVoted(clientEntry.DatabaseId)) return;

            var clientVoted = Repository.Client.GetClientVoted(clientEntry.DatabaseId);
            if (clientVoted.ChannelId.HasValue && clientVoted.ChannelId == e.TargetChannelId)
            {
                var context = new MessageContext
                                  {
                                      ClientDatabaseId = clientEntry.DatabaseId,
                                      ClientNickname = clientEntry.Nickname
                                  };

                switch (Repository.Settings.Vote.Behavior)
                {
                    case PunishBehavior.KickFromChannel:
                        QueryRunner.KickClient(e.ClientId, KickReason.Channel, Repository.Settings.Vote.KickMessage.ToMessage(context));
                        break;
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Moves the client to sticky.
        /// </summary>
        /// <param name="clientEntry">The client entry.</param>
        private void PunishClient(SimpleClientEntity clientEntry)
        {
            if (Repository.Client.IsClientVoted(clientEntry.ClientDatabaseId)) return;

            var context = new MessageContext
                              {
                                  ClientDatabaseId = clientEntry.ClientDatabaseId,
                                  ClientNickname = clientEntry.Nickname
                              };

            
            QueryRunner.PokeClient(clientEntry.ClientId, Repository.Settings.Vote.KickMessage.ToMessage(context));

            switch (Repository.Settings.Vote.Behavior)
            {
                case PunishBehavior.KickFromChannel:
                    QueryRunner.KickClient(clientEntry.ClientId, KickReason.Channel, Repository.Settings.Vote.KickMessage.ToMessage(context));
                    Repository.Client.AddVotedClients(new VotedClientEntity(clientEntry.ClientDatabaseId) { ChannelId = clientEntry.ChannelId });
                    break;
                case PunishBehavior.KickFromServer:
                    QueryRunner.KickClient(clientEntry.ClientId, KickReason.Server, Repository.Settings.Vote.KickMessage.ToMessage(context));
                    break;
                case PunishBehavior.MoveToSticky:
                    QueryRunner.MoveClient(clientEntry.ClientId, Repository.Settings.Sticky.Channel);
                    Repository.Channel.AddStickyClients(clientEntry.ClientDatabaseId, Repository.Settings.Sticky.Channel, Repository.Settings.Sticky.StickTime);
                    Repository.Client.AddVotedClients(new VotedClientEntity(clientEntry.ClientDatabaseId) { ChannelId = clientEntry.ChannelId });
                    break;
            }

            Log(Repository.Settings.Vote,
                string.Format("Client '{0}'(id:{1}) punished by voting.",
                              clientEntry.Nickname, clientEntry.ClientDatabaseId));
        }

        #endregion
    }
}