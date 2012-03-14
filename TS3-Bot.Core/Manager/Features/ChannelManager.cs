namespace DirkSarodnick.TS3_Bot.Core.Manager.Features
{
    using Base;
    using Helper;
    using Repository;
    using Settings;
    using TS3QueryLib.Core.Query.Notification.EventArgs;

    /// <summary>
    /// Defines the ChannelManager class.
    /// </summary>
    public class ChannelManager : DefaultManager
    {
        #region Constructor & Invokation

        /// <summary>
        /// Initializes a new instance of the <see cref="ChannelManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ChannelManager(DataRepository repository) : base(repository)
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
                                     Repository.Settings.Sticky
                                 });
        }

        /// <summary>
        /// Invokes this instance.
        /// </summary>
        public override void Invoke()
        {
            StickyChannel();
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.ClientMovedByClientEventArgs"/> instance containing the event data.</param>
        public override void Invoke(ClientMovedByClientEventArgs e)
        {
            StickyChannel(e);
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.ClientMovedEventArgs"/> instance containing the event data.</param>
        public override void Invoke(ClientMovedEventArgs e)
        {
            StickyChannel(e);
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.ClientJoinedEventArgs"/> instance containing the event data.</param>
        public override void Invoke(ClientJoinedEventArgs e)
        {
            StickyChannel(e);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Stickies the channel.
        /// </summary>
        protected void StickyChannel()
        {
            if (!Repository.Settings.Control.Stick.Enabled) return;

            var stickyClients = Repository.Channel.GetStickyClients();
            foreach (var stickyClient in stickyClients)
            {
                var client = Repository.Client.GetClientSimple(stickyClient.ClientDatabaseId);
                if (client != null && client.ChannelId != stickyClient.ChannelId)
                {
                    MoveClientToSticky(client.ClientId, stickyClient.ChannelId);
                }
            }
        }

        /// <summary>
        /// Stickies the channel.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.ClientMovedByClientEventArgs"/> instance containing the event data.</param>
        protected void StickyChannel(ClientMovedByClientEventArgs e)
        {
            if (!Repository.Settings.Sticky.Enabled ||
                e.InvokerClientId == Self.ClientId ||
                e.InvokerClientId == SelfWorker.ClientId) return;

            var clientEntry = Repository.Client.GetClientInfo(e.ClientId);
            var invokerEntry = Repository.Client.GetClientInfo(e.InvokerClientId);
            if (!PermissionHelper.IsGranted(Repository.Settings.Sticky, clientEntry.ServerGroups)) return;

            if (Repository.Settings.Sticky.Channel == e.TargetChannelId)
            {
                Repository.Channel.AddStickyClients(clientEntry.DatabaseId, e.TargetChannelId, Repository.Settings.Sticky.StickTime);
                
                Log(Repository.Settings.Sticky,
                    string.Format("Client '{0}'(id:{1}) were added to sticky (id:{4}) by '{2}'(id:{3})",
                                  clientEntry.Nickname, clientEntry.DatabaseId, invokerEntry.Nickname, invokerEntry.DatabaseId, e.TargetChannelId));
            }
            else
            {
                Repository.Channel.RemoveStickyClients(clientEntry.DatabaseId, false);
                
                Log(Repository.Settings.Sticky,
                    string.Format("Client '{0}'(id:{1}) were removed from sticky (id:{4}) by '{2}'(id:{3})",
                                  clientEntry.Nickname, clientEntry.DatabaseId, invokerEntry.Nickname, invokerEntry.DatabaseId, e.TargetChannelId));
            }
        }

        /// <summary>
        /// Stickies the channel.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.ClientMovedEventArgs"/> instance containing the event data.</param>
        protected void StickyChannel(ClientMovedEventArgs e)
        {
            if (!Repository.Settings.Sticky.Enabled) return;

            var clientEntry = Repository.Client.GetClientInfo(e.ClientId);
            if (!PermissionHelper.IsGranted(Repository.Settings.Sticky, clientEntry.ServerGroups)) return;

            if (Repository.Settings.Sticky.Channel == e.TargetChannelId)
            {
                Log(Repository.Settings.Sticky,
                    string.Format("Client '{0}'(id:{1}) added himself to sticky (id:{2})",
                                  clientEntry.Nickname, clientEntry.DatabaseId, e.TargetChannelId));

                Repository.Channel.AddStickyClients(clientEntry.DatabaseId, e.TargetChannelId, Repository.Settings.Sticky.StickTime);
            }
            else if (Repository.Channel.GetClientSticky(clientEntry.DatabaseId).HasValue)
            {
                MoveClientToSticky(e.ClientId, Repository.Channel.GetClientSticky(clientEntry.DatabaseId).Value);
            }
        }

        /// <summary>
        /// Stickies the channel.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.ClientJoinedEventArgs"/> instance containing the event data.</param>
        protected void StickyChannel(ClientJoinedEventArgs e)
        {
            if (!Repository.Settings.Sticky.Enabled ||
                !PermissionHelper.IsGranted(Repository.Settings.Sticky, e.ServerGroups)) return;

            var sticky = Repository.Channel.GetClientSticky(e.ClientDatabaseId);
            if (sticky != null && sticky.Value != e.ChannelId)
            {
                MoveClientToSticky(e.ClientId, sticky.Value);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Moves the client to sticky.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="channelId">The channel id.</param>
        private void MoveClientToSticky(uint clientId, uint channelId)
        {
            QueryRunner.MoveClient(clientId, channelId);
        }

        #endregion
    }
}