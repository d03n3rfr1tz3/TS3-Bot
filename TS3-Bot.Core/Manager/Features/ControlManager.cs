namespace DirkSarodnick.TS3_Bot.Core.Manager.Features
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Base;
    using Connection;
    using Entity;
    using Entity.Messages;
    using Helper;
    using Repository;
    using Settings;
    using TS3QueryLib.Core.CommandHandling;
    using TS3QueryLib.Core.Server.Notification.EventArgs;

    /// <summary>
    /// Defines the ControlManager class.
    /// </summary>
    public class ControlManager : DefaultManager
    {
        #region Constructor & Invokation

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ControlManager(DataRepository repository) : base(repository)
        {
            MessageHelper = new MessageHelper(repository);
        }

        /// <summary>
        /// Determines whether this instance can invoke the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        /// <returns>
        ///   <c>true</c> if this instance can invoke the specified e; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanInvoke(MessageReceivedEventArgs e)
        {
            return CanInvoke(new ISettings[]
                                 {
                                     Repository.Settings.Control.Help,
                                     Repository.Settings.Control.Seen,
                                     Repository.Settings.Control.Files,
                                     Repository.Settings.Control.Stick,
                                     Repository.Settings.Control.Hours,
                                     Repository.Settings.Control.Moderator,
                                     Repository.Settings.Control.SeenGroup,
                                     Repository.Settings.Control.SeenModerator,
                                     Repository.Settings.Control.Punish
                                 });
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        public override void Invoke(MessageReceivedEventArgs e)
        {
            Help(e);
            Seen(e);
            Files(e);
            Stick(e);
            Unstick(e);
            Hour(e);
            Moderator(e);
            SeenGroup(e);
            SeenModerator(e);
            Punish(e);
            Unpunish(e);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the message helper.
        /// </summary>
        /// <value>The message helper.</value>
        private MessageHelper MessageHelper { get; set; }

        #endregion

        #region Message Validation

        /// <summary>
        /// Validates the Help message.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        protected void Help(MessageReceivedEventArgs e)
        {
            if(!Repository.Settings.Control.Help.Enabled) return;

            if (MessageHelper.CanBeMessage<HelpMessage>(e.Message) &&
                Repository.Client.GetClientInfo(e.InvokerClientId).ServerGroups
                    .Any(m => PermissionHelper.IsGranted(Repository.Settings.Control.Help, m)))
            {
                Execute(MessageHelper.GetMessageInformation<HelpMessage>(e, e.Message));
            }
        }

        /// <summary>
        /// Validates the Seen message.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        protected void Seen(MessageReceivedEventArgs e)
        {
            if (!Repository.Settings.Control.Seen.Enabled) return;

            if (MessageHelper.CanBeMessage<SeenMessage>(e.Message) &&
                Repository.Client.GetClientInfo(e.InvokerClientId).ServerGroups
                    .Any(m => PermissionHelper.IsGranted(Repository.Settings.Control.Seen, m)))
            {
                Execute(MessageHelper.GetMessageInformation<SeenMessage>(e, e.Message));
            }
        }

        /// <summary>
        /// Validates the Files message.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        protected void Files(MessageReceivedEventArgs e)
        {
            if (!Repository.Settings.Control.Files.Enabled) return;

            if (MessageHelper.CanBeMessage<FilesMessage>(e.Message) &&
                Repository.Client.GetClientInfo(e.InvokerClientId).ServerGroups
                    .Any(m => PermissionHelper.IsGranted(Repository.Settings.Control.Files, m)))
            {
                Execute(MessageHelper.GetMessageInformation<FilesMessage>(e, e.Message));
            }
        }

        /// <summary>
        /// Validates the Stick message.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        protected void Stick(MessageReceivedEventArgs e)
        {
            if (!Repository.Settings.Control.Stick.Enabled) return;

            if (MessageHelper.CanBeMessage<StickMessage>(e.Message) &&
                Repository.Client.GetClientInfo(e.InvokerClientId).ServerGroups
                    .Any(m => PermissionHelper.IsGranted(Repository.Settings.Control.Stick, m)))
            {
                Execute(MessageHelper.GetMessageInformation<StickMessage>(e, e.Message));
            }
        }

        /// <summary>
        /// Validates the Unstick message.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        protected void Unstick(MessageReceivedEventArgs e)
        {
            if (!Repository.Settings.Control.Stick.Enabled) return;

            if (MessageHelper.CanBeMessage<UnstickMessage>(e.Message) &&
                Repository.Client.GetClientInfo(e.InvokerClientId).ServerGroups
                    .Any(m => PermissionHelper.IsGranted(Repository.Settings.Control.Stick, m)))
            {
                Execute(MessageHelper.GetMessageInformation<UnstickMessage>(e, e.Message));
            }
        }

        /// <summary>
        /// Validates the Hours message.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        protected void Hour(MessageReceivedEventArgs e)
        {
            if (!Repository.Settings.Control.Hours.Enabled) return;

            if (MessageHelper.CanBeMessage<HoursMessage>(e.Message) &&
                Repository.Client.GetClientInfo(e.InvokerClientId).ServerGroups
                    .Any(m => PermissionHelper.IsGranted(Repository.Settings.Control.Hours, m)))
            {
                Execute(MessageHelper.GetMessageInformation<HoursMessage>(e, e.Message));
            }
        }

        /// <summary>
        /// Validates the Moderator message.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        protected void Moderator(MessageReceivedEventArgs e)
        {
            if (!Repository.Settings.Control.Moderator.Enabled) return;

            if (MessageHelper.CanBeMessage<ModeratorMessage>(e.Message) &&
                Repository.Client.GetClientInfo(e.InvokerClientId).ServerGroups
                    .Any(m => PermissionHelper.IsGranted(Repository.Settings.Control.Moderator, m)))
            {
                Execute(MessageHelper.GetMessageInformation<ModeratorMessage>(e, e.Message));
            }
        }

        /// <summary>
        /// Validates the SeenGroup message.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        protected void SeenGroup(MessageReceivedEventArgs e)
        {
            if (!Repository.Settings.Control.SeenGroup.Enabled) return;

            if (MessageHelper.CanBeMessage<SeenGroupMessage>(e.Message) &&
                Repository.Client.GetClientInfo(e.InvokerClientId).ServerGroups
                    .Any(m => PermissionHelper.IsGranted(Repository.Settings.Control.SeenGroup, m)))
            {
                Execute(MessageHelper.GetMessageInformation<SeenGroupMessage>(e, e.Message));
            }
        }

        /// <summary>
        /// Validates the SeenModerator message.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        protected void SeenModerator(MessageReceivedEventArgs e)
        {
            if (!Repository.Settings.Control.SeenModerator.Enabled) return;
            if (Repository.Settings.Control.SeenModerator.ServerGroup <= 0) return;

            if (MessageHelper.CanBeMessage<SeenModeratorMessage>(e.Message) &&
                Repository.Client.GetClientInfo(e.InvokerClientId).ServerGroups
                    .Any(m => PermissionHelper.IsGranted(Repository.Settings.Control.SeenModerator, m)))
            {
                Execute(MessageHelper.GetMessageInformation<SeenModeratorMessage>(e, e.Message));
            }
        }

        /// <summary>
        /// Validates the Punish message.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        protected void Punish(MessageReceivedEventArgs e)
        {
            if (!Repository.Settings.Control.Punish.Enabled) return;
            if (Repository.Settings.Control.Punish.ServerGroup <= 0) return;

            if (MessageHelper.CanBeMessage<PunishMessage>(e.Message) &&
                Repository.Client.GetClientInfo(e.InvokerClientId).ServerGroups
                    .Any(m => PermissionHelper.IsGranted(Repository.Settings.Control.Punish, m)))
            {
                Execute(MessageHelper.GetMessageInformation<PunishMessage>(e, e.Message));
            }
        }

        /// <summary>
        /// Validates the Unpunish message.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        protected void Unpunish(MessageReceivedEventArgs e)
        {
            if (!Repository.Settings.Control.Punish.Enabled) return;
            if (Repository.Settings.Control.Punish.ServerGroup <= 0) return;

            if (MessageHelper.CanBeMessage<UnpunishMessage>(e.Message) &&
                Repository.Client.GetClientInfo(e.InvokerClientId).ServerGroups
                    .Any(m => PermissionHelper.IsGranted(Repository.Settings.Control.Punish, m)))
            {
                Execute(MessageHelper.GetMessageInformation<UnpunishMessage>(e, e.Message));
            }
        }

        #endregion

        #region Message Execution

        /// <summary>
        /// Receives the Help message
        /// </summary>
        /// <param name="message">The message.</param>
        private void Execute(HelpMessage message)
        {
            var helpMessages = MessageHelper.GetHelpMessages(Repository.Client.GetClientInfo(message.SenderClientId).ServerGroups);
            if (helpMessages != null)
            {
                helpMessages.ForEach(helpMessage => QueryRunner.SendTextMessage(Repository.Settings.Control.Help.Target, Repository.Settings.Control.Help.TargetId > 0 ? Repository.Settings.Control.Help.TargetId : message.SenderClientId,
                    helpMessage.ToMessage()));
            }

            var clientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.Help,
                string.Format("Client '{0}'(id:{1}) used !help.",
                              clientEntry.Nickname, clientEntry.DatabaseId));
        }

        /// <summary>
        /// Receives the Seen message
        /// </summary>
        /// <param name="message">The message.</param>
        private void Execute(SeenMessage message)
        {
            if (message.ErrorMessage != null)
            {
                Log(LogLevel.Warning, message.ErrorMessage);
                return;
            }

            if (message.ClientDatabaseIds.Count == 1 &&
                message.ClientDatabaseIds.Contains(Self.ClientDatabaseId))
                return;

            for (int index = 0; index < message.ClientDatabaseIds.Count; index++)
            {
                var clientDatabaseId = message.ClientDatabaseIds[index];
                var client = Repository.Client.GetClientDataBaseInfo(clientDatabaseId);
                var lastSeen = Repository.Client.GetLastSeen(clientDatabaseId);
                var context = new MessageContext
                                  {
                                      Index = index,
                                      ClientDatabaseId = client.DatabaseId,
                                      ClientNickname = client.NickName,
                                      ClientLastLogin = client.LastConnected.ToLocalTime().ToString(Repository.Static.DateTimeFormat),
                                      ClientLastSeen = lastSeen != default(DateTime) && lastSeen > DateTime.MinValue
                                                           ? lastSeen.ToLocalTime().ToString(Repository.Static.DateTimeFormat)
                                                           : "Never"
                                  };

                QueryRunner.SendTextMessage(Repository.Settings.Control.Seen.Target,
                                            Repository.Settings.Control.Seen.TargetId > 0 ? Repository.Settings.Control.Seen.TargetId : message.SenderClientId,
                                            Repository.Settings.Control.Seen.TextMessage.ToMessage(context));
            }

            var senderClientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.Seen,
                string.Format("Client '{0}'(id:{1}) used !seen for clients '{2}'.",
                              senderClientEntry.Nickname, senderClientEntry.DatabaseId,
                              string.Join("', '", message.ClientDatabaseIds.ConvertAll(i => i.ToString(CultureInfo.InvariantCulture)).ToArray())));
        }

        /// <summary>
        /// Receives the Files message
        /// </summary>
        /// <param name="message">The message.</param>
        private void Execute(FilesMessage message)
        {
            using (new DynamicCredentialManager(Repository))
            {
                for (int index = 0; index < Repository.Server.GetServerList().Count; index++)
                {
                    var server = Repository.Server.GetServerList()[index];
                    var fileList = Repository.File.GetFileList(server.ServerId);
                    var context = new MessageContext
                                      {
                                          ServerName = server.ServerName,
                                          ServerId = server.ServerId,
                                          ServerPort = server.ServerPort
                                      };

                    QueryRunner.SendTextMessage(Repository.Settings.Control.Files.Target,
                                                Repository.Settings.Control.Files.TargetId > 0 ? Repository.Settings.Control.Files.TargetId : message.SenderClientId,
                                                Repository.Settings.Control.Files.MessagePerServer.ToMessage(context));
                    if (fileList.Count == 0)
                    {
                        QueryRunner.SendTextMessage(Repository.Settings.Control.Files.Target,
                                                    Repository.Settings.Control.Files.TargetId > 0 ? Repository.Settings.Control.Files.TargetId : message.SenderClientId,
                                                    Repository.Settings.Control.Files.MessageNoFilesFound.ToMessage(context));
                        continue;
                    }

                    QueryRunner.SendTextMessage(Repository.Settings.Control.Files.Target,
                                                Repository.Settings.Control.Files.TargetId > 0 ? Repository.Settings.Control.Files.TargetId : message.SenderClientId,
                                                Repository.Settings.Control.Files.MessageFilesFound.ToMessage(context));
                    fileList.ForEach(f => QueryRunner.SendTextMessage(
                        Repository.Settings.Control.Files.Target,
                        Repository.Settings.Control.Files.TargetId > 0 ? Repository.Settings.Control.Files.TargetId : message.SenderClientId,
                        Repository.Settings.Control.Files.MessageFile.ToMessage(new MessageContext
                                           {
                                               Index = index,
                                               ChannelId = f.ChannelId,
                                               ChannelName = f.ChannelName,
                                               FileName = f.Name,
                                               FileCreated =
                                                   f.Created.ToLocalTime().ToString(Repository.Static.DateTimeFormat),
                                               FileSize = f.Size
                                           })));
                }
            }

            var clientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.Files,
                string.Format("Client '{0}'(id:{1}) used !files.",
                              clientEntry.Nickname, clientEntry.DatabaseId));
        }

        /// <summary>
        /// Executes the Stick message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void Execute(StickMessage message)
        {
            foreach (uint clientDatabaseId in message.ClientDatabaseIds)
            {
                Repository.Channel.AddStickyClients(clientDatabaseId, message.ChannelId, message.StickTime);
            }

            var clientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.Stick,
                string.Format("Client '{0}'(id:{1}) used !stick.",
                              clientEntry.Nickname, clientEntry.DatabaseId));
        }

        /// <summary>
        /// Executes the Unstick message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void Execute(UnstickMessage message)
        {
            foreach (uint clientDatabaseId in message.ClientDatabaseIds)
            {
                Repository.Channel.RemoveStickyClients(clientDatabaseId, true);
            }

            var clientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.Stick,
                string.Format("Client '{0}'(id:{1}) used !unstick.",
                              clientEntry.Nickname, clientEntry.DatabaseId));
        }

        /// <summary>
        /// Executes the Hours message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void Execute(HoursMessage message)
        {
            if (message.ErrorMessage != null)
            {
                Log(LogLevel.Warning, message.ErrorMessage);
                return;
            }

            var server = Repository.Server.GetCurrentServer();
            var context = new MessageContext
            {
                ServerId = server.Id,
                ServerName = server.Name,
                ServerPort = server.Port
            };
            QueryRunner.SendTextMessage(Repository.Settings.Control.Hours.Target, Repository.Settings.Control.Hours.TargetId > 0 ? Repository.Settings.Control.Hours.TargetId : message.SenderClientId,
                                        Repository.Settings.Control.Hours.TextMessage.ToMessage(context));

            var clientDatabaseDict = new Dictionary<uint, double>();
            var ids = message.ClientDatabaseIds.Any() ? message.ClientDatabaseIds : Repository.Client.GetTimeUsers(message.TimeSpan.FromDate, message.TimeSpan.ToDate);
            ids.ForEach(clientDatabaseId => clientDatabaseDict.Add(clientDatabaseId, Repository.Client.GetTime(clientDatabaseId, message.TimeSpan.FromDate, message.TimeSpan.ToDate)));
            var clientDatabaseIds = clientDatabaseDict.Where(m => m.Value >= 0.5).OrderByDescending(m => m.Value).Select(m => m.Key).Take(Repository.Settings.Control.Hours.Limit).ToList();

            for (int index = 0; index < clientDatabaseIds.Count; index++)
            {
                var clientDatabaseId = clientDatabaseIds[index];
                var client = Repository.Client.GetClientDataBaseInfo(clientDatabaseId);
                var lastSeen = Repository.Client.GetLastSeen(clientDatabaseId);
                var minutes = Repository.Client.GetTime(clientDatabaseId, message.TimeSpan.FromDate,
                                                      message.TimeSpan.ToDate);
                var messageContext = new MessageContext
                                         {
                                             Index = index,
                                             ClientDatabaseId = client.DatabaseId,
                                             ClientNickname = client.NickName,
                                             ClientLastLogin = client.LastConnected.ToLocalTime().ToString(Repository.Static.DateTimeFormat),
                                             ClientLastSeen = lastSeen != default(DateTime) && lastSeen > DateTime.MinValue
                                                     ? lastSeen.ToLocalTime().ToString(Repository.Static.DateTimeFormat)
                                                     : "Never",
                                             ClientHours = minutes/60
                                         };

                QueryRunner.SendTextMessage(Repository.Settings.Control.Hours.Target,
                                            Repository.Settings.Control.Hours.TargetId > 0 ? Repository.Settings.Control.Hours.TargetId : message.SenderClientId,
                                            Repository.Settings.Control.Hours.MessagePerClient.ToMessage(messageContext));
            }

            var senderClientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.Hours,
                string.Format("Client '{0}'(id:{1}) used !hours for clients '{2}'.",
                              senderClientEntry.Nickname, senderClientEntry.DatabaseId,
                              string.Join("', '", message.ClientDatabaseIds.ConvertAll(i => i.ToString(CultureInfo.InvariantCulture)).ToArray())));
        }

        /// <summary>
        /// Executes the Moderator message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void Execute(ModeratorMessage message)
        {
            if (message.ErrorMessage != null)
            {
                Log(LogLevel.Warning, message.ErrorMessage);
                return;
            }

            var server = Repository.Server.GetCurrentServer();
            var entities = Repository.Client.GetModeration(ModerationType.Added, message.TimeSpan.FromDate, message.TimeSpan.ToDate);

            var context = new MessageContext
                              {
                                  ServerId = server.Id,
                                  ServerName = server.Name,
                                  ServerPort = server.Port
                              };
            QueryRunner.SendTextMessage(Repository.Settings.Control.Moderator.Target, Repository.Settings.Control.Moderator.TargetId > 0 ? Repository.Settings.Control.Moderator.TargetId : message.SenderClientId,
                                        Repository.Settings.Control.Moderator.TextMessage.ToMessage(context));
            var groupedEntities = entities.GroupBy(m => m.Moderator).OrderByDescending(m => m.Count()).Take(Repository.Settings.Control.Hours.Limit).ToList();

            for (int index = 0; index < groupedEntities.Count; index++)
            {
                var entity = groupedEntities[index];
                var moderatorEntity = Repository.Client.GetClientSimple(entity.Key);
                var messageContext = new MessageContext
                                         {
                                             Index = index,
                                             ClientDatabaseId = moderatorEntity.ClientDatabaseId,
                                             ClientNickname = moderatorEntity.Nickname,
                                             ModeratorVerified = entity.Count()
                                         };
                QueryRunner.SendTextMessage(Repository.Settings.Control.Moderator.Target,
                                            Repository.Settings.Control.Moderator.TargetId > 0 ? Repository.Settings.Control.Moderator.TargetId : message.SenderClientId,
                                            Repository.Settings.Control.Moderator.MessagePerModerator.ToMessage(messageContext));
            }

            var clientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.Moderator,
                string.Format("Client '{0}'(id:{1}) used !mods.",
                              clientEntry.Nickname, clientEntry.DatabaseId));
        }

        /// <summary>
        /// Executes the SeenGroup message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void Execute(SeenGroupMessage message)
        {
            if (message.ErrorMessage != null)
            {
                Log(LogLevel.Warning, message.ErrorMessage);
                return;
            }

            var server = Repository.Server.GetCurrentServer();
            var serverGroup = Repository.Server.GetServerGroup(message.ServerGroup);
            var context = new MessageContext
            {
                ServerId = server.Id,
                ServerName = server.Name,
                ServerPort = server.Port,
                ServerGroupId = serverGroup.Id,
                ServerGroupName = serverGroup.Name
            };
            QueryRunner.SendTextMessage(Repository.Settings.Control.SeenGroup.Target, Repository.Settings.Control.SeenGroup.TargetId > 0 ? Repository.Settings.Control.SeenGroup.TargetId : message.SenderClientId,
                                        Repository.Settings.Control.SeenGroup.TextMessage.ToMessage(context));

            for (int index = 0; index < message.ClientDatabaseIds.Count; index++)
            {
                var clientDatabaseId = message.ClientDatabaseIds[index];
                var client = Repository.Client.GetClientDataBaseInfo(clientDatabaseId);
                var lastSeen = Repository.Client.GetLastSeen(clientDatabaseId);
                var messageContext = new MessageContext
                                         {
                                             Index = index,
                                             ServerGroupId = serverGroup.Id,
                                             ServerGroupName = serverGroup.Name,
                                             ClientDatabaseId = client.DatabaseId,
                                             ClientNickname = client.NickName,
                                             ClientLastLogin = client.LastConnected.ToLocalTime().ToString(Repository.Static.DateTimeFormat),
                                             ClientLastSeen = lastSeen != default(DateTime) && lastSeen > DateTime.MinValue
                                                     ? lastSeen.ToLocalTime().ToString(Repository.Static.DateTimeFormat)
                                                     : "Never"
                                         };

                QueryRunner.SendTextMessage(Repository.Settings.Control.SeenGroup.Target,
                                            Repository.Settings.Control.SeenGroup.TargetId > 0 ? Repository.Settings.Control.SeenGroup.TargetId : message.SenderClientId,
                                            Repository.Settings.Control.SeenGroup.MessagePerClient.ToMessage(messageContext));
            }

            var senderClientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.SeenGroup,
                string.Format("Client '{0}'(id:{1}) used !seengroup for server group '{2}'.",
                              senderClientEntry.Nickname, senderClientEntry.DatabaseId, message.ServerGroup));
        }

        /// <summary>
        /// Executes the SeenModerator message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void Execute(SeenModeratorMessage message)
        {
            if (message.ErrorMessage != null)
            {
                Log(LogLevel.Warning, message.ErrorMessage);
                return;
            }

            var server = Repository.Server.GetCurrentServer();
            var serverGroup = Repository.Server.GetServerGroup(message.ServerGroup);
            var context = new MessageContext
            {
                ServerId = server.Id,
                ServerName = server.Name,
                ServerPort = server.Port,
                ServerGroupId = serverGroup.Id,
                ServerGroupName = serverGroup.Name
            };
            QueryRunner.SendTextMessage(Repository.Settings.Control.SeenModerator.Target, Repository.Settings.Control.SeenModerator.TargetId > 0 ? Repository.Settings.Control.SeenModerator.TargetId : message.SenderClientId,
                                        Repository.Settings.Control.SeenModerator.TextMessage.ToMessage(context));

            for (int index = 0; index < message.ClientDatabaseIds.Count; index++)
            {
                var clientDatabaseId = message.ClientDatabaseIds[index];
                var client = Repository.Client.GetClientDataBaseInfo(clientDatabaseId);
                var lastSeen = Repository.Client.GetLastSeen(clientDatabaseId);
                var joinedGroup = Repository.Client.GetServerGroupJoined(clientDatabaseId, message.ServerGroup);
                var messageContext = new MessageContext
                                         {
                                             Index = index,
                                             ServerGroupId = serverGroup.Id,
                                             ServerGroupName = serverGroup.Name,
                                             ServerGroupJoined = joinedGroup > DateTime.MinValue
                                                     ? joinedGroup.ToLocalTime().ToString(Repository.Static.DateTimeFormat)
                                                     : "Never",
                                             ClientDatabaseId = client.DatabaseId,
                                             ClientNickname = client.NickName,
                                             ClientLastLogin = client.LastConnected.ToLocalTime().ToString(Repository.Static.DateTimeFormat),
                                             ClientLastSeen = lastSeen != default(DateTime) && lastSeen > DateTime.MinValue
                                                     ? lastSeen.ToLocalTime().ToString(Repository.Static.DateTimeFormat)
                                                     : "Never"
                                         };

                QueryRunner.SendTextMessage(Repository.Settings.Control.SeenModerator.Target,
                                            Repository.Settings.Control.SeenModerator.TargetId > 0 ? Repository.Settings.Control.SeenModerator.TargetId : message.SenderClientId,
                                            Repository.Settings.Control.SeenModerator.MessagePerClient.ToMessage(messageContext));
            }

            var senderClientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.SeenModerator,
                string.Format("Client '{0}'(id:{1}) used !seenmods.",
                              senderClientEntry.Nickname, senderClientEntry.DatabaseId));
        }

        /// <summary>
        /// Executes the PunishMessage message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void Execute(PunishMessage message)
        {
            foreach (uint clientDatabaseId in message.ClientDatabaseIds)
            {
                var clientId = Repository.Client.GetClientIdByClientDatabaseId(clientDatabaseId);
                if (clientId.HasValue)
                {
                    QueryRunner.MoveClient(clientId.Value, Repository.Settings.Control.Punish.Channel);
                }

                Repository.Client.RemoveClientServerGroups(clientDatabaseId, Repository.Client.GetClientServerGroups(clientDatabaseId).Select(m => m.Id).ToList());
                Repository.Client.AddClientServerGroups(clientDatabaseId, new[] { Repository.Settings.Control.Punish.ServerGroup });
            }

            var clientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.Stick,
                string.Format("Client '{0}'(id:{1}) used !stick.",
                              clientEntry.Nickname, clientEntry.DatabaseId));
        }

        /// <summary>
        /// Executes the UnpunishMessage message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void Execute(UnpunishMessage message)
        {
            foreach (uint clientDatabaseId in message.ClientDatabaseIds)
            {
                Repository.Client.RemoveClientServerGroups(clientDatabaseId, new[] { Repository.Settings.Control.Punish.ServerGroup });
                Repository.Client.RestoreGroups(clientDatabaseId);
            }

            var clientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.Punish,
                string.Format("Client '{0}'(id:{1}) used !unpunish.",
                              clientEntry.Nickname, clientEntry.DatabaseId));
        }

        #endregion
    }
}