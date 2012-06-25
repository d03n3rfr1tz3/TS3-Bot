namespace DirkSarodnick.TS3_Bot.Core.Manager.Features.Base
{
    using System;
    using System.Linq;
    using Connection;
    using Repository;
    using Settings;
    using TS3QueryLib.Core.CommandHandling;
    using TS3QueryLib.Core.Server;
    using TS3QueryLib.Core.Server.Entities;
    using TS3QueryLib.Core.Server.Notification.EventArgs;
    using TS3QueryLib.Core.Server.Responses;

    /// <summary>
    /// Defines the DefaultManager class.
    /// </summary>
    public class DefaultManager : IDisposable
    {
        private bool disposed;

        #region Constructor & Invokation
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultManager"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public DefaultManager(DataRepository repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="DefaultManager"/> is reclaimed by garbage collection.
        /// </summary>
        ~DefaultManager()
        {
            Dispose();
        }

        /// <summary>
        /// Determines whether this instance can invoke.
        /// </summary>
        /// <returns>True or False</returns>
        public virtual bool CanInvoke()
        {
            return false;
        }

        /// <summary>
        /// Determines whether this instance [can slow invoke].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance [can slow invoke]; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanSlowInvoke()
        {
            return false;
        }

        /// <summary>
        /// Determines whether this instance can invoke the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientMovedByClientEventArgs"/> instance containing the event data.</param>
        /// <returns>
        ///   <c>true</c> if this instance can invoke the specified e; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanInvoke(ClientMovedByClientEventArgs e)
        {
            return false;
        }

        /// <summary>
        /// Determines whether this instance can invoke the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientMovedEventArgs"/> instance containing the event data.</param>
        /// <returns>
        ///   <c>true</c> if this instance can invoke the specified e; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanInvoke(ClientMovedEventArgs e)
        {
            return false;
        }

        /// <summary>
        /// Determines whether this instance can invoke the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientJoinedEventArgs"/> instance containing the event data.</param>
        /// <returns>
        ///   <c>true</c> if this instance can invoke the specified e; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanInvoke(ClientJoinedEventArgs e)
        {
            return false;
        }

        /// <summary>
        /// Determines whether this instance can invoke the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientDisconnectEventArgs"/> instance containing the event data.</param>
        /// <returns>
        ///   <c>true</c> if this instance can invoke the specified e; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanInvoke(ClientDisconnectEventArgs e)
        {
            return false;
        }

        /// <summary>
        /// Determines whether this instance can invoke the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientConnectionLostEventArgs"/> instance containing the event data.</param>
        /// <returns>
        ///   <c>true</c> if this instance can invoke the specified e; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanInvoke(ClientConnectionLostEventArgs e)
        {
            return false;
        }

        /// <summary>
        /// Determines whether this instance can invoke the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        /// <returns>
        ///   <c>true</c> if this instance can invoke the specified e; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool CanInvoke(MessageReceivedEventArgs e)
        {
            return false;
        }

        /// <summary>
        /// Invokes this instance.
        /// </summary>
        public virtual void Invoke()
        {
        }

        /// <summary>
        /// Slows the invoke.
        /// </summary>
        public virtual void SlowInvoke()
        {
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientMovedByClientEventArgs"/> instance containing the event data.</param>
        public virtual void Invoke(ClientMovedByClientEventArgs e)
        {
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientMovedEventArgs"/> instance containing the event data.</param>
        public virtual void Invoke(ClientMovedEventArgs e)
        {
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientJoinedEventArgs"/> instance containing the event data.</param>
        public virtual void Invoke(ClientJoinedEventArgs e)
        {
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientDisconnectEventArgs"/> instance containing the event data.</param>
        public virtual void Invoke(ClientDisconnectEventArgs e)
        {
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientConnectionLostEventArgs"/> instance containing the event data.</param>
        public virtual void Invoke(ClientConnectionLostEventArgs e)
        {
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        public virtual void Invoke(MessageReceivedEventArgs e)
        {
        }

        #endregion

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>The repository.</value>
        protected DataRepository Repository { get; set; }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <value>The connection.</value>
        protected TeamSpeakConnection Connection
        {
            get
            {
                return Repository.Connection;
            }
        }

        /// <summary>
        /// Gets the query runner.
        /// </summary>
        /// <value>The query runner.</value>
        protected QueryRunner QueryRunner
        {
            get { return Connection.CredentialEntity.WorkerQueryRunner; }
        }

        /// <summary>
        /// Gets the notification query runner.
        /// </summary>
        /// <value>The notification query runner.</value>
        protected QueryRunner NotificationQueryRunner
        {
            get { return Connection.CredentialEntity.NotificationQueryRunner; }
        }

        /// <summary>
        /// Gets the self.
        /// </summary>
        /// <value>The self.</value>
        protected WhoAmIResponse Self
        {
            get { return Connection.CredentialEntity.Self; }
        }

        /// <summary>
        /// Gets the self worker.
        /// </summary>
        /// <value>The self worker.</value>
        protected WhoAmIResponse SelfWorker
        {
            get { return Connection.CredentialEntity.SelfWorker; }
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="setting">The setting.</param>
        /// <param name="message">The message.</param>
        protected void Log(LogLevel logLevel, ISettings setting, string message)
        {
            if (setting.LogEnabled)
                QueryRunner.AddLogEntry(new LogEntryLight(logLevel, message));
        }

        /// <summary>
        /// Logs the specified setting.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <param name="message">The message.</param>
        protected void Log(ISettings setting, string message)
        {
            Log(LogLevel.Info, setting, message);
        }

        /// <summary>
        /// Logs the specified log level.
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">The message.</param>
        protected void Log(LogLevel logLevel, string message)
        {
            QueryRunner.AddLogEntry(new LogEntryLight(logLevel, message));
        }

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void Log(string message)
        {
            Log(LogLevel.Info, message);
        }

        /// <summary>
        /// Determines whether this instance can invoke the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>True or False</returns>
        protected static bool CanInvoke(ISettings[] settings)
        {
            return settings.Any(s => s.Enabled);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            Repository.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}