namespace DirkSarodnick.TS3_Bot.Core.Manager.Features
{
    using System;
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
                                     Repository.Settings.Control.Punish,
                                     Repository.Settings.Control.SelfGroup
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
            SelfGroupAdd(e);
            SelfGroupRemove(e);
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

        /// <summary>
        /// Validates the SelfGroupAdd message.
        /// </summary>
        /// <param name="e">The <see cref="MessageReceivedEventArgs" /> instance containing the event data.</param>
        protected void SelfGroupAdd(MessageReceivedEventArgs e)
        {
            if (!Repository.Settings.Control.SelfGroup.Enabled) return;
            if (Repository.Settings.Control.SelfGroup.AllowedServerGroups == null) return;

            if (MessageHelper.CanBeMessage<SelfGroupAddMessage>(e.Message) &&
                Repository.Client.GetClientInfo(e.InvokerClientId).ServerGroups
                    .Any(m => PermissionHelper.IsGranted(Repository.Settings.Control.SelfGroup, m)))
            {
                Execute(MessageHelper.GetMessageInformation<SelfGroupAddMessage>(e, e.Message));
            }
        }

        /// <summary>
        /// Validates the SelfGroupRemove message.
        /// </summary>
        /// <param name="e">The <see cref="MessageReceivedEventArgs" /> instance containing the event data.</param>
        protected void SelfGroupRemove(MessageReceivedEventArgs e)
        {
            if (!Repository.Settings.Control.SelfGroup.Enabled) return;
            if (Repository.Settings.Control.SelfGroup.AllowedServerGroups == null) return;

            if (MessageHelper.CanBeMessage<SelfGroupRemoveMessage>(e.Message) &&
                Repository.Client.GetClientInfo(e.InvokerClientId).ServerGroups
                    .Any(m => PermissionHelper.IsGranted(Repository.Settings.Control.SelfGroup, m)))
            {
                Execute(MessageHelper.GetMessageInformation<SelfGroupRemoveMessage>(e, e.Message));
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
                string.Format("Client '{1}'(id:{2}) used {0}.", Repository.Settings.Control.Help.Command,
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
                if (client != null)
                {
                    var lastSeen = Repository.Client.GetLastSeen(clientDatabaseId);
                    var context = new MessageContext
                                      {
                                          Index = index + 1,
                                          ClientDatabaseId = client.DatabaseId,
                                          ClientNickname = client.NickName,
                                          ClientLastLogin = client.LastConnected.ToLocalTime().ToString(Repository.Static.DateTimeFormat),
                                          ClientLastSeen = lastSeen != default(DateTime) && lastSeen > DateTime.MinValue
                                                               ? lastSeen.ToLocalTime().ToString(Repository.Static.DateTimeFormat)
                                                               : client.LastConnected.ToLocalTime().ToString(Repository.Static.DateTimeFormat)
                                      };

                    QueryRunner.SendTextMessage(Repository.Settings.Control.Seen.Target,
                                                Repository.Settings.Control.Seen.TargetId > 0 ? Repository.Settings.Control.Seen.TargetId : message.SenderClientId,
                                                Repository.Settings.Control.Seen.TextMessage.ToMessage(context));
                }
            }

            var senderClientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.Seen,
                string.Format("Client '{1}'(id:{2}) used {0} for clients '{3}'.", Repository.Settings.Control.Seen.Command,
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
                var serverList = Repository.Server.GetServerList();
                for (int index = 0; index < serverList.Count; index++)
                {
                    var server = serverList[index];
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
                                               Index = index + 1,
                                               ServerName = server.ServerName,
                                               ServerId = server.ServerId,
                                               ServerPort = server.ServerPort,
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
                string.Format("Client '{1}'(id:{2}) used {0}.", Repository.Settings.Control.Files.Command,
                              clientEntry.Nickname, clientEntry.DatabaseId));
        }

        /// <summary>
        /// Executes the Stick message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void Execute(StickMessage message)
        {
            if (message.ErrorMessage != null)
            {
                Log(LogLevel.Warning, message.ErrorMessage);
                return;
            }

            var clientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.Stick,
                string.Format("Client '{1}'(id:{2}) used {0}.", Repository.Settings.Control.Stick.Command,
                              clientEntry.Nickname, clientEntry.DatabaseId));

            foreach (uint clientDatabaseId in message.ClientDatabaseIds)
            {
                Repository.Channel.AddStickyClients(clientDatabaseId, message.ChannelId, message.StickTime);
                Log(Repository.Settings.Control.Stick, string.Format("Client (id:{0}) got sticked.", clientDatabaseId));
            }
        }

        /// <summary>
        /// Executes the Unstick message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void Execute(UnstickMessage message)
        {
            if (message.ErrorMessage != null)
            {
                Log(LogLevel.Warning, message.ErrorMessage);
                return;
            }

            var clientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.Stick,
                string.Format("Client '{1}'(id:{2}) used {0}.", Repository.Settings.Control.Stick.UndoCommand,
                              clientEntry.Nickname, clientEntry.DatabaseId));

            foreach (uint clientDatabaseId in message.ClientDatabaseIds)
            {
                Repository.Channel.RemoveStickyClients(clientDatabaseId);
                Log(Repository.Settings.Control.Stick, string.Format("Client (id:{0}) got unsticked.", clientDatabaseId));
            }
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

            var clientDatabaseTimes = message.AllClients
                                          ? Repository.Client.GetTime(message.TimeSpan.FromDate, message.TimeSpan.ToDate)
                                          : Repository.Client.GetTime(message.ClientDatabaseIds, message.TimeSpan.FromDate, message.TimeSpan.ToDate);
            var clientDatabaseIds = clientDatabaseTimes.OrderByDescending(m => m.Value).Take(Repository.Settings.Control.Hours.Limit).ToList();

            for (int index = 0; index < clientDatabaseIds.Count; index++)
            {
                var clientDatabaseTime = clientDatabaseIds[index];
                var client = Repository.Client.GetClientDataBaseInfo(clientDatabaseTime.Key);
                if (client != null)
                {
                    var lastSeen = Repository.Client.GetLastSeen(clientDatabaseTime.Key);
                    var messageContext = new MessageContext
                                             {
                                                 Index = index + 1,
                                                 ServerName = server.Name,
                                                 ServerId = server.Id,
                                                 ServerPort = server.Port,
                                                 ClientDatabaseId = client.DatabaseId,
                                                 ClientNickname = client.NickName,
                                                 ClientLastLogin = client.LastConnected.ToLocalTime().ToString(Repository.Static.DateTimeFormat),
                                                 ClientLastSeen = lastSeen != default(DateTime) && lastSeen > DateTime.MinValue
                                                         ? lastSeen.ToLocalTime().ToString(Repository.Static.DateTimeFormat)
                                                         : client.LastConnected.ToLocalTime().ToString(Repository.Static.DateTimeFormat),
                                                 ClientHours = clientDatabaseTime.Value / 60
                                             };

                    QueryRunner.SendTextMessage(Repository.Settings.Control.Hours.Target,
                                                Repository.Settings.Control.Hours.TargetId > 0 ? Repository.Settings.Control.Hours.TargetId : message.SenderClientId,
                                                Repository.Settings.Control.Hours.MessagePerClient.ToMessage(messageContext));
                }
            }

            var senderClientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.Hours,
                string.Format("Client '{1}'(id:{2}) used {0} for clients '{3}'.", Repository.Settings.Control.Hours.Command,
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
            var groupedEntities = entities.GroupBy(m => m.Moderator).OrderByDescending(m => m.Count()).Take(Repository.Settings.Control.Moderator.Limit).ToList();

            for (int index = 0; index < groupedEntities.Count; index++)
            {
                var entity = groupedEntities[index];
                var moderatorEntity = Repository.Client.GetClientDataBaseInfo((uint)entity.Key);
                var messageContext = new MessageContext
                                         {
                                             Index = index + 1,
                                             ServerName = server.Name,
                                             ServerId = server.Id,
                                             ServerPort = server.Port,
                                             ClientDatabaseId = moderatorEntity == null ? default(uint) : moderatorEntity.DatabaseId,
                                             ClientNickname = moderatorEntity == null ? default(string) : moderatorEntity.NickName,
                                             ModeratorVerified = entity.Count()
                                         };
                QueryRunner.SendTextMessage(Repository.Settings.Control.Moderator.Target,
                                            Repository.Settings.Control.Moderator.TargetId > 0 ? Repository.Settings.Control.Moderator.TargetId : message.SenderClientId,
                                            Repository.Settings.Control.Moderator.MessagePerModerator.ToMessage(messageContext));
            }

            var clientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.Moderator,
                string.Format("Client '{1}'(id:{2}) used {0}.", Repository.Settings.Control.Moderator.Command,
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
                if (client != null)
                {
                    var lastSeen = Repository.Client.GetLastSeen(clientDatabaseId);
                    var messageContext = new MessageContext
                                             {
                                                 Index = index + 1,
                                                 ServerName = server.Name,
                                                 ServerId = server.Id,
                                                 ServerPort = server.Port,
                                                 ServerGroupId = serverGroup.Id,
                                                 ServerGroupName = serverGroup.Name,
                                                 ClientDatabaseId = client.DatabaseId,
                                                 ClientNickname = client.NickName,
                                                 ClientLastLogin = client.LastConnected.ToLocalTime().ToString(Repository.Static.DateTimeFormat),
                                                 ClientLastSeen = lastSeen != default(DateTime) && lastSeen > DateTime.MinValue
                                                         ? lastSeen.ToLocalTime().ToString(Repository.Static.DateTimeFormat)
                                                         : client.LastConnected.ToLocalTime().ToString(Repository.Static.DateTimeFormat)
                                             };

                    QueryRunner.SendTextMessage(Repository.Settings.Control.SeenGroup.Target,
                                                Repository.Settings.Control.SeenGroup.TargetId > 0 ? Repository.Settings.Control.SeenGroup.TargetId : message.SenderClientId,
                                                Repository.Settings.Control.SeenGroup.MessagePerClient.ToMessage(messageContext));
                }
            }

            var senderClientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.SeenGroup,
                string.Format("Client '{1}'(id:{2}) used {0} for server group '{3}'.", Repository.Settings.Control.SeenGroup.Command,
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
                if (client != null)
                {
                    var lastSeen = Repository.Client.GetLastSeen(clientDatabaseId);
                    var joinedGroup = Repository.Client.GetServerGroupJoined(clientDatabaseId, message.ServerGroup);
                    var messageContext = new MessageContext
                                             {
                                                 Index = index + 1,
                                                 ServerName = server.Name,
                                                 ServerId = server.Id,
                                                 ServerPort = server.Port,
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
                                                         : client.LastConnected.ToLocalTime().ToString(Repository.Static.DateTimeFormat)
                                             };

                    QueryRunner.SendTextMessage(Repository.Settings.Control.SeenModerator.Target,
                                                Repository.Settings.Control.SeenModerator.TargetId > 0 ? Repository.Settings.Control.SeenModerator.TargetId : message.SenderClientId,
                                                Repository.Settings.Control.SeenModerator.MessagePerClient.ToMessage(messageContext));
                }
            }

            var senderClientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.SeenModerator,
                string.Format("Client '{1}'(id:{2}) used {0}.", Repository.Settings.Control.SeenModerator.Command,
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

                Repository.Client.StripGroups(clientDatabaseId);
                Repository.Client.AddClientServerGroups(clientDatabaseId, new[] { Repository.Settings.Control.Punish.ServerGroup });
            }

            var clientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Log(Repository.Settings.Control.Punish,
                string.Format("Client '{1}'(id:{2}) used {0}.", Repository.Settings.Control.Punish.Command,
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
                string.Format("Client '{1}'(id:{2}) used {0}.", Repository.Settings.Control.Punish.UndoCommand,
                              clientEntry.Nickname, clientEntry.DatabaseId));
        }

        /// <summary>
        /// Executes the SelfGroupAddMessage message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void Execute(SelfGroupAddMessage message)
        {
            var clientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Repository.Client.AddClientServerGroups(clientEntry.DatabaseId, message.ServerGroupIds);

            foreach (uint serverGroup in message.ServerGroupIds)
            {
                Log(Repository.Settings.Control.SelfGroup,
                string.Format("Client '{1}'(id:{2}) used {0} with group '{3}'.", Repository.Settings.Control.SelfGroup.Command,
                              clientEntry.Nickname, clientEntry.DatabaseId, serverGroup));
            }
        }

        /// <summary>
        /// Executes the SelfGroupRemoveMessage message.
        /// </summary>
        /// <param name="message">The message.</param>
        private void Execute(SelfGroupRemoveMessage message)
        {
            var clientEntry = Repository.Client.GetClientInfo(message.SenderClientId);
            Repository.Client.RemoveClientServerGroups(clientEntry.DatabaseId, message.ServerGroupIds);

            foreach (uint serverGroup in message.ServerGroupIds)
            {
                Log(Repository.Settings.Control.SelfGroup,
                string.Format("Client '{1}'(id:{2}) used {0} with group '{3}'.", Repository.Settings.Control.SelfGroup.UndoCommand,
                              clientEntry.Nickname, clientEntry.DatabaseId, serverGroup));
            }
        }

        #endregion
    }
}