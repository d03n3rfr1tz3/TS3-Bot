namespace DirkSarodnick.TS3_Bot.Core
{
    using System;
    using System.Net.Sockets;
    using System.Threading;
    using Service;
    using TS3QueryLib.Core.Query.Notification.EventArgs;

    /// <summary>
    /// Defines the BotInstance class.
    /// </summary>
    public partial class BotInstance
    {
        /// <summary>
        /// Executes the tick
        /// </summary>
        /// <param name="syncContext">The sync context.</param>
        private void FastTick(object syncContext)
        {
            if (SynchronizationContext.Current == null) SynchronizationContext.SetSynchronizationContext((SynchronizationContext)syncContext);

            try
            {
                if (Repository.Static.NextConnectionTime > Repository.Static.Now) return;
                if (Connection.Busy) return;
                Connection.Busy = true;

                ManagerFactory.Invoke();

                if (slowTick > 5) SlowTick();
                slowTick++;

                if (keepTick > 28) KeepAlive();
                keepTick++;
            }
            catch (SocketException ex)
            {
                LogService.Warning(string.Format("{0} (IP: {1}, QPort: {2}, Server: {3}, User: {4}){5}{6}", ex.Message,
                                                 Repository.Settings.TeamSpeak.Host,
                                                 Repository.Settings.TeamSpeak.QueryPort,
                                                 Repository.Settings.TeamSpeak.Instance,
                                                 Repository.Settings.TeamSpeak.Username,
                                                 Environment.NewLine,
                                                 ex.StackTrace));
                Repository.Static.LastConnectionError = Repository.Static.Now;
            }
            catch (Exception ex)
            {
                LogService.Error(ex.ToString());
            }
            finally
            {
                Repository.Container.Clean();
                Connection.Busy = false;
            }
        }

        /// <summary>
        /// Executes the slower tick
        /// </summary>
        private void SlowTick()
        {
            slowTick = 0;
            ManagerFactory.Invoke();
        }

        /// <summary>
        /// Keeps the Teamspeak connection alive.
        /// </summary>
        private void KeepAlive()
        {
            keepTick = 0;
            Repository.KeepAlive();
        }

        /// <summary>
        /// Handles the ClientJoined event of the Teamspeak Server.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.ClientJoinedEventArgs"/> instance containing the event data.</param>
        public void Notifications_ClientJoined(object sender, ClientJoinedEventArgs e)
        {
            try
            {
                ManagerFactory.Invoke(e);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.ToString());
            }
        }

        /// <summary>
        /// Handles the ClientMoved event of the Teamspeak Server.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.ClientMovedEventArgs"/> instance containing the event data.</param>
        public void Notifications_ClientMoved(object sender, ClientMovedEventArgs e)
        {
            try
            {
                ManagerFactory.Invoke(e);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.ToString());
            }
        }

        /// <summary>
        /// Handles the ClientMoveForced event of the Teamspeak Server.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.ClientMovedByClientEventArgs"/> instance containing the event data.</param>
        public void Notifications_ClientMoveForced(object sender, ClientMovedByClientEventArgs e)
        {
            try
            {
                ManagerFactory.Invoke(e);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.ToString());
            }
        }

        /// <summary>
        /// Handles the MessageReceived event of the Teamspeak Server.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Query.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        public void Notifications_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            try
            {
                ManagerFactory.Invoke(e);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.ToString());
            }
        }

        /// <summary>
        /// Executes the Tick.
        /// </summary>
        /// <param name="syncContext">The sync context.</param>
        public void Tick(SynchronizationContext syncContext)
        {
            ThreadPool.QueueUserWorkItem((context) => this.FastTick(context), syncContext);
        }
    }
}