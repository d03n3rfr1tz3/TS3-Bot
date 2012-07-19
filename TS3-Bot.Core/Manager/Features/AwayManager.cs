namespace DirkSarodnick.TS3_Bot.Core.Manager.Features
{
    using System.Linq;
    using Base;
    using Entity;
    using Helper;
    using Repository;
    using Settings;
    using TS3QueryLib.Core.CommandHandling;

    /// <summary>
    /// Defines the AwayManager class.
    /// </summary>
    public class AwayManager : DefaultManager
    {
        #region Constructor & Invokation

        /// <summary>
        /// Initializes a new instance of the <see cref="AwayManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AwayManager(DataRepository repository) : base(repository)
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
                                     Repository.Settings.Away,
                                     Repository.Settings.Idle
                                 });
        }

        /// <summary>
        /// Invokes this instance.
        /// </summary>
        public override void Invoke()
        {
            MoveIdleClients();
            MoveAwayClients();
            MoveAwayClientsBack();
            MoveIdleClientsBack();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Moves the away clients.
        /// </summary>
        protected void MoveAwayClients()
        {
            if(!Repository.Settings.Away.Enabled) return;

            var clients = Repository.Client.GetClientList().Where(m => m.IsClientAway.GetValueOrDefault() || m.IsClientInputMuted.GetValueOrDefault() || m.IsClientOutputMuted.GetValueOrDefault());
            /*
            var clientsAway = Repository.Client.GetClientList().Where(m => Repository.Settings.Away.Channel > 0 && m.IsClientAway.HasValue && m.IsClientAway.Value).ToList();
            var clientsMicrophoneMuted = Repository.Client.GetClientList().Where(m => Repository.Settings.Away.MuteMicrophoneChannel > 0 && m.IsClientInputMuted.HasValue && m.IsClientInputMuted.Value).Except(clientsAway).ToList();
            var clientsHeadphoneMuted = Repository.Client.GetClientList().Where(m => Repository.Settings.Away.MuteHeadphoneChannel > 0 && m.IsClientOutputMuted.HasValue && m.IsClientOutputMuted.Value).Except(clientsAway).Except(clientsMicrophoneMuted).ToList();
            */
            foreach (var client in clients)
            {
                if (!PermissionHelper.IsGranted(Repository.Settings.Away, client.ServerGroups)) continue;

                uint? awayChannel = null;
                if (Repository.Settings.Away.MuteHeadphoneChannel > 0 && client.IsClientOutputMuted.GetValueOrDefault()) awayChannel = Repository.Settings.Away.MuteHeadphoneChannel;
                if (Repository.Settings.Away.MuteMicrophoneChannel > 0 && client.IsClientInputMuted.GetValueOrDefault()) awayChannel = Repository.Settings.Away.MuteMicrophoneChannel;
                if (Repository.Settings.Away.Channel > 0 && client.IsClientAway.GetValueOrDefault()) awayChannel = Repository.Settings.Away.Channel;

                if (awayChannel.HasValue && !client.ChannelId.EqualsAny(awayChannel.Value) && !Repository.Channel.GetClientSticky(client.ClientDatabaseId).HasValue)
                {
                    Repository.Client.SetLastChannelByClientId(client.ClientDatabaseId, client.ChannelId);
                    QueryRunner.MoveClient(client.ClientId, awayChannel.Value);

                    Log(Repository.Settings.Away,
                        string.Format("Client '{0}'(id:{1}) successfully moved to Away Channel(id:{2}).",
                                      client.Nickname, client.ClientDatabaseId, awayChannel));
                }
            }
        }

        /// <summary>
        /// Moves the idle clients.
        /// </summary>
        protected void MoveIdleClients()
        {
            if (!Repository.Settings.Idle.Enabled) return;

            foreach (var client in Repository.Client.GetClientList()
                .Where(m => m.ClientIdleDuration.HasValue && m.ClientIdleDuration.Value.TotalMinutes >= Repository.Settings.Idle.IdleTime))
            {
                if (!PermissionHelper.IsGranted(Repository.Settings.Idle, client.ServerGroups)) continue;

                if (!client.ChannelId.EqualsAny(Repository.Settings.Away.Channel, Repository.Settings.Away.MuteMicrophoneChannel, Repository.Settings.Away.MuteHeadphoneChannel, Repository.Settings.Idle.Channel) &&
                    !Repository.Channel.GetClientSticky(client.ClientDatabaseId).HasValue)
                {
                    Repository.Client.SetLastChannelByClientId(client.ClientDatabaseId, client.ChannelId);
                    QueryRunner.MoveClient(client.ClientId, Repository.Settings.Idle.Channel);

                    Log(Repository.Settings.Idle,
                        string.Format("Client '{0}'(id:{1}) successfully moved to Idle Channel.",
                                      client.Nickname, client.ClientDatabaseId));
                }
            }
        }

        /// <summary>
        /// Moves the away clients back.
        /// </summary>
        protected void MoveAwayClientsBack()
        {
            if (!Repository.Settings.Away.Enabled) return;

            foreach (var client in Repository.Client.GetClientList()
                .Where(m => (m.ChannelId == Repository.Settings.Away.Channel || m.ChannelId == Repository.Settings.Away.MuteHeadphoneChannel || m.ChannelId == Repository.Settings.Away.MuteMicrophoneChannel) &&       // Check for Channel
                            (Repository.Settings.Away.Channel == 0 || (Repository.Settings.Away.Channel > 0 && m.IsClientAway.HasValue && !m.IsClientAway.Value)) &&                                                    // Check if not Away
                            (Repository.Settings.Away.MuteMicrophoneChannel == 0 || (Repository.Settings.Away.MuteMicrophoneChannel > 0 && m.IsClientInputMuted.HasValue && !m.IsClientInputMuted.Value)) &&               // Check if not muted Microphone
                            (Repository.Settings.Away.MuteHeadphoneChannel == 0 || (Repository.Settings.Away.MuteHeadphoneChannel > 0 && m.IsClientOutputMuted.HasValue && !m.IsClientOutputMuted.Value)) &&               // Check if not muted Headphones
                            ((!Repository.Settings.Idle.Enabled || Repository.Settings.Idle.IdleTime == 0) || (m.ClientIdleDuration.HasValue && m.ClientIdleDuration.Value.TotalMinutes < Repository.Settings.Idle.IdleTime)) && // Check if not idle (to prevent endless loop)
                            Repository.Client.HasLastChannelByClientId(m.ClientDatabaseId))) // Check for an last-channel entry
            {
                if (!Repository.Channel.GetClientSticky(client.ClientDatabaseId).HasValue)
                {
                    var awayClient = Repository.Client.GetLastChannelByClientId(client.ClientDatabaseId);
                    var channel = Repository.Channel.GetChannelListInfo(awayClient.LastChannelId);
                    QueryRunner.MoveClient(client.ClientId, awayClient.LastChannelId);

                    Log(Repository.Settings.Away,
                        string.Format("Client '{0}'(id:{1}) successfully moved back from Away Channel to '{2}'(id:{3}).",
                                      client.Nickname, client.ClientDatabaseId, channel.Name, awayClient.LastChannelId));

                    if (!string.IsNullOrEmpty(Repository.Settings.Away.TextMessage))
                    {
                        var awayTimespan = Repository.Static.Now - awayClient.Creation;
                        var messageContext = new MessageContext
                        {
                            ClientDatabaseId = client.ClientDatabaseId,
                            ClientNickname = client.Nickname,
                            ClientAwayTime = BasicHelper.GetTimespanString(awayTimespan),
                            ChannelId = awayClient.LastChannelId,
                            ChannelName = channel.Name
                        };
                        QueryRunner.SendTextMessage(MessageTarget.Server, Repository.Connection.CredentialEntity.Self.VirtualServerId, Repository.Settings.Away.TextMessage.ToMessage(messageContext));
                    }
                }
                Repository.Client.RemoveLastChannelByClientId(client.ClientDatabaseId);
            }
        }

        /// <summary>
        /// Moves the idle clients back.
        /// </summary>
        protected void MoveIdleClientsBack()
        {
            if (!Repository.Settings.Idle.Enabled) return;

            foreach (var client in Repository.Client.GetClientList()
                .Where(m => m.ChannelId == Repository.Settings.Idle.Channel &&         // Check for Channel
                            m.IsClientAway.HasValue && !m.IsClientAway.Value &&                // Check if not away (to prevent endless loop)
                            m.ClientIdleDuration.HasValue && m.ClientIdleDuration.Value.TotalMinutes < Repository.Settings.Idle.IdleTime && // Check if not idle
                            Repository.Client.HasLastChannelByClientId(m.ClientDatabaseId)))   // Check for last-channel entry
            {
                if (!Repository.Channel.GetClientSticky(client.ClientDatabaseId).HasValue)
                {
                    var idleClient = Repository.Client.GetLastChannelByClientId(client.ClientDatabaseId);
                    var channel = Repository.Channel.GetChannelListInfo(idleClient.LastChannelId);
                    QueryRunner.MoveClient(client.ClientId, idleClient.LastChannelId);

                    Log(Repository.Settings.Idle,
                        string.Format("Client '{0}'(id:{1}) successfully moved back from Idle Channel to '{2}'(id:{3}).",
                                      client.Nickname, client.ClientDatabaseId, channel.Name, idleClient.LastChannelId));

                    if (!string.IsNullOrEmpty(Repository.Settings.Idle.TextMessage))
                    {
                        var idleTimespan = Repository.Static.Now - idleClient.Creation;
                        var messageContext = new MessageContext
                        {
                            ClientDatabaseId = client.ClientDatabaseId,
                            ClientNickname = client.Nickname,
                            ClientAwayTime = BasicHelper.GetTimespanString(idleTimespan),
                            ChannelId = idleClient.LastChannelId,
                            ChannelName = channel.Name
                        };
                        QueryRunner.SendTextMessage(MessageTarget.Server, Repository.Connection.CredentialEntity.Self.VirtualServerId, Repository.Settings.Idle.TextMessage.ToMessage(messageContext));
                    }
                }
                Repository.Client.RemoveLastChannelByClientId(client.ClientDatabaseId);
            }
        }

        #endregion
    }
}