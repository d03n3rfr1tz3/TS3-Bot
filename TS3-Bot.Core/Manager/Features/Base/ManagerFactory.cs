namespace DirkSarodnick.TS3_Bot.Core.Manager.Features.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Helper;
    using Repository;
    using TS3QueryLib.Core.Server.Notification.EventArgs;

    /// <summary>
    /// Defines the ManagerFactory class.
    /// </summary>
    public class ManagerFactory : IDisposable
    {
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerFactory"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ManagerFactory(DataRepository repository)
        {
            Repository = repository;
            Manager = new List<DefaultManager>
                          {
                              new AwayManager(Repository),
                              new ChannelManager(Repository),
                              new CompliantManager(Repository),
                              new ControlManager(Repository),
                              new EventManager(Repository),
                              new MessageManager(Repository),
                              new NickManager(Repository),
                              new RecordManager(Repository),
                              new TimeManager(Repository)
                          };
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="ManagerFactory"/> is reclaimed by garbage collection.
        /// </summary>
        ~ManagerFactory()
        {
            Dispose();
        }

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>The repository.</value>
        private DataRepository Repository { get; set; }

        /// <summary>
        /// Gets or sets the manager.
        /// </summary>
        /// <value>The manager.</value>
        private List<DefaultManager> Manager { get; set; }

        /// <summary>
        /// Invokes this instance.
        /// </summary>
        public virtual void Invoke()
        {
            Manager.Where(m => m.CanInvoke()).ForEach(m => m.Invoke());
        }

        /// <summary>
        /// Slows the invoke.
        /// </summary>
        public virtual void SlowInvoke()
        {
            Manager.Where(m => m.CanSlowInvoke()).ForEach(m => m.SlowInvoke());
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientMovedByClientEventArgs"/> instance containing the event data.</param>
        public virtual void Invoke(ClientMovedByClientEventArgs e)
        {
            Manager.Where(m => m.CanInvoke(e)).ForEach(m => m.Invoke(e));
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientMovedEventArgs"/> instance containing the event data.</param>
        public virtual void Invoke(ClientMovedEventArgs e)
        {
            Manager.Where(m => m.CanInvoke(e)).ForEach(m => m.Invoke(e));
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientJoinedEventArgs"/> instance containing the event data.</param>
        public virtual void Invoke(ClientJoinedEventArgs e)
        {
            Manager.Where(m => m.CanInvoke(e)).ForEach(m => m.Invoke(e));
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientDisconnectEventArgs"/> instance containing the event data.</param>
        public virtual void Invoke(ClientDisconnectEventArgs e)
        {
            Manager.Where(m => m.CanInvoke(e)).ForEach(m => m.Invoke(e));
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.ClientConnectionLostEventArgs"/> instance containing the event data.</param>
        public virtual void Invoke(ClientConnectionLostEventArgs e)
        {
            Manager.Where(m => m.CanInvoke(e)).ForEach(m => m.Invoke(e));
        }

        /// <summary>
        /// Invokes the specified e.
        /// </summary>
        /// <param name="e">The <see cref="TS3QueryLib.Core.Server.Notification.EventArgs.MessageReceivedEventArgs"/> instance containing the event data.</param>
        public virtual void Invoke(MessageReceivedEventArgs e)
        {
            Manager.Where(m => m.CanInvoke(e)).ForEach(m => m.Invoke(e));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            Manager.ForEach(m => m.Dispose());
            Manager.Clear();

            GC.SuppressFinalize(this);
        }
    }
}