namespace DirkSarodnick.TS3_Bot.Core.Entity.Messages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TS3QueryLib.Core.Server.Notification.EventArgs;

    /// <summary>
    /// Defines HoursMessage class.
    /// </summary>
    public class HoursMessage : Message
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HoursMessage"/> class.
        /// </summary>
        public HoursMessage()
        {
            TimeSpan = new TimeSpanEntity();
            ClientDatabaseIds = new List<uint>();
        }

        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>The command.</value>
        protected override string Command { get { return this.Repository.Settings.Control.Hours.Command; } }

        /// <summary>
        /// Gets the time span.
        /// </summary>
        public TimeSpanEntity TimeSpan { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [all clients].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [all clients]; otherwise, <c>false</c>.
        /// </value>
        public bool AllClients { get; private set; }

        /// <summary>
        /// Gets or sets the client database ids.
        /// </summary>
        /// <value>The client database ids.</value>
        public List<uint> ClientDatabaseIds { get; private set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Validates the message
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public override bool Validate(string[] parameters)
        {
            return parameters.Length > 0 &&
                   parameters[0].StartsWith(Command, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Initializes the message
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        /// <param name="parameters">The parameters.</param>
        public override void Initialize(MessageReceivedEventArgs e, string[] parameters)
        {
            SenderClientId = e.InvokerClientId;

            string nickname = null;
            if (parameters.Length > 2)
            {
                TimeSpanEntity timeSpanEntity;
                if (TimeSpanEntity.TryParse(parameters[1], out timeSpanEntity))
                {
                    this.TimeSpan = timeSpanEntity;
                    nickname = parameters[2];
                }
                else if (TimeSpanEntity.TryParse(parameters[2], out timeSpanEntity))
                {
                    this.TimeSpan = timeSpanEntity;
                    nickname = parameters[1];
                }
                else
                {
                    ErrorMessage = "Could not parse your request!";
                }
            }
            else if (parameters.Length > 1)
            {
                TimeSpanEntity timeSpanEntity;
                if (TimeSpanEntity.TryParse(parameters[1], out timeSpanEntity))
                {
                    this.TimeSpan = timeSpanEntity;
                }
                else
                {
                    nickname = parameters[1];
                }
            }
            else
            {
                AllClients = true;
            }

            if (nickname != null)
            {
                AllClients = false;

                uint clientDatabaseId;
                if (uint.TryParse(nickname, out clientDatabaseId))
                {
                    ClientDatabaseIds.Add(clientDatabaseId);
                }
                else
                {
                    var clientIds = Repository.Client.GetClientsFromDatabase()
                        .Where(m => m.NickName.ToLower().Contains(nickname.ToLower()))
                        .Select(m => m.DatabaseId).ToList();
                    if (clientIds.Any())
                    {
                        ClientDatabaseIds = clientIds;
                    }
                    else
                    {
                        ErrorMessage = string.Format("Could not find nickname or database id '{0}'", nickname);
                    }
                }
            }
        }
    }
}