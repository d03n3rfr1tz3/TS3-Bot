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
    using TS3QueryLib.Core.Query.Notification.EventArgs;

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
        /// Determines whether this instance can invoke.
        /// </summary>
        /// <returns>True or False</returns>
        public override bool CanInvoke()
        {
            return CanInvoke(new ISettings[]
                                 {
                                     Repository.Settings.Control.Help,
                                     Repository.Settings.Control.Seen,
                                     Repository.Settings.Control.Files,
                                     Repository.Settings.Control.Stick
                                 });
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        public override void Invoke(MessageReceivedEventArgs e)
        {
            Help(e);
            Seen(e);
            Files(e);
            Stick(e);
            Unstick(e);
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
        /// Validates the Help message
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
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
        /// Validates the Seen message
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
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
        /// Validates the Files message
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
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

        #endregion

        #region Message Execution

        /// <summary>
        /// Receives the Help message
        /// </summary>
        /// <param name="message">The message.</param>
        private void Execute(HelpMessage message)
        {
            var helpMessage = MessageHelper.GetHelpMessages(Repository.Client.GetClientInfo(message.SenderClientId).ServerGroups);
            if (helpMessage != null)
                QueryRunner.SendTextMessage(MessageTarget.Client, message.SenderClientId, helpMessage.ToMessage());

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

            foreach (var clientDatabaseId in message.ClientDatabaseIds)
            {
                var client = Repository.Client.GetClientDataBaseInfo(clientDatabaseId);
                var lastSeen = Repository.Client.GetLastSeen(clientDatabaseId);
                var context = new MessageContext
                                  {
                                      ClientDatabaseId = client.DatabaseId,
                                      ClientNickname = client.NickName,
                                      ClientLastLogin = client.LastConnected.ToString(Repository.Static.DateTimeFormat),
                                      ClientLastSeen = lastSeen != default(DateTime) && lastSeen > DateTime.MinValue
                                                           ? lastSeen.ToString(Repository.Static.DateTimeFormat)
                                                           : "Nie"
                                  };

                QueryRunner.SendTextMessage(MessageTarget.Client, message.SenderClientId,
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
                foreach (var server in Repository.Server.GetServerList())
                {
                    var fileList = Repository.File.GetFileList(server.ServerId);
                    var context = new MessageContext
                                      {
                                          ServerName = server.ServerName,
                                          ServerId = server.ServerId,
                                          ServerPort = server.ServerPort
                                      };

                    QueryRunner.SendTextMessage(MessageTarget.Client, message.SenderClientId,
                                                Repository.Settings.Control.Files.MessagePerServer.ToMessage(context));
                    if (fileList.Count == 0)
                    {
                        QueryRunner.SendTextMessage(MessageTarget.Client, message.SenderClientId,
                                                    Repository.Settings.Control.Files.MessageNoFilesFound.ToMessage(context));
                        continue;
                    }

                    QueryRunner.SendTextMessage(MessageTarget.Client, message.SenderClientId,
                                                Repository.Settings.Control.Files.MessageFilesFound.ToMessage(context));
                    fileList.ForEach(f => QueryRunner.SendTextMessage(
                                              MessageTarget.Client, message.SenderClientId,
                                              Repository.Settings.Control.Files.MessageFile
                                                  .ToMessage(new MessageContext
                                                                 {
                                                                     ChannelId = f.ChannelId,
                                                                     ChannelName = f.ChannelName,
                                                                     FileName = f.Name,
                                                                     FileCreated = f.Created.ToString(Repository.Static.DateTimeFormat),
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

        #endregion
    }
}