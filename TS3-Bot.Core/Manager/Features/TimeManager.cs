namespace DirkSarodnick.TS3_Bot.Core.Manager.Features
{
    using Base;
    using Repository;
    using TS3QueryLib.Core.Server.Notification.EventArgs;

    public class TimeManager : DefaultManager
    {
        #region Constructor & Invokation

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public TimeManager(DataRepository repository) : base(repository)
        {
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
            return true;
        }

        /// <summary>
        /// Determines whether this instance can invoke the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientDisconnectEventArgs"/> instance containing the event data.</param>
        /// <returns>
        ///   <c>true</c> if this instance can invoke the specified e; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanInvoke(ClientDisconnectEventArgs e)
        {
            return true;
        }

        /// <summary>
        /// Determines whether this instance can invoke the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientConnectionLostEventArgs"/> instance containing the event data.</param>
        /// <returns>
        ///   <c>true</c> if this instance can invoke the specified e; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanInvoke(ClientConnectionLostEventArgs e)
        {
            return true;
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientJoinedEventArgs"/> instance containing the event data.</param>
        public override void Invoke(ClientJoinedEventArgs e)
        {
            TimeCapture(e);
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientDisconnectEventArgs"/> instance containing the event data.</param>
        public override void Invoke(ClientDisconnectEventArgs e)
        {
            TimeCapture(e);
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientConnectionLostEventArgs"/> instance containing the event data.</param>
        public override void Invoke(ClientConnectionLostEventArgs e)
        {
            TimeCapture(e);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Times the capture.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientJoinedEventArgs"/> instance containing the event data.</param>
        protected void TimeCapture(ClientJoinedEventArgs e)
        {
            Repository.Client.CaptureTime(e.ClientDatabaseId, Repository.Static.Now, null);
        }

        /// <summary>
        /// Times the capture.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientDisconnectEventArgs"/> instance containing the event data.</param>
        protected void TimeCapture(ClientDisconnectEventArgs e)
        {
            var clientDatabaseId = Repository.Client.GetClientDatabaseIdByClientId(e.ClientId);
            if (clientDatabaseId.HasValue)
            {
                Repository.Client.CaptureTime(clientDatabaseId.Value, null, Repository.Static.Now);
            }
        }

        /// <summary>
        /// Times the capture.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientConnectionLostEventArgs"/> instance containing the event data.</param>
        protected void TimeCapture(ClientConnectionLostEventArgs e)
        {
            var clientDatabaseId = Repository.Client.GetClientDatabaseIdByClientId(e.ClientId);
            if (clientDatabaseId.HasValue)
            {
                Repository.Client.CaptureTime(clientDatabaseId.Value, null, Repository.Static.Now);
            }
        }

        #endregion
    }
}