namespace DirkSarodnick.TS3_Bot.Core.Repository
{
    using System;
    using Manager.Connection;
    using Settings;
    using TS3QueryLib.Core.Server.Entities;

    /// <summary>
    /// Defines the DataRepository class.
    /// </summary>
    public class DataRepository : IDisposable
    {
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataRepository"/> class.
        /// </summary>
        public DataRepository(TeamSpeakConnection connection, InstanceSettings settings)
        {
            Connection = connection;
            Settings = settings;
            Container = new DataContainer(this.Settings.Name);
            Channel = new ChannelData(this);
            Client = new ClientData(this);
            Compliant = new CompliantData(this);
            File = new FileData(this);
            Server = new ServerData(this);
            Static = new StaticData(this);
        }

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="DataRepository"/> is reclaimed by garbage collection.
        /// </summary>
        ~DataRepository()
        {
            Dispose();
        }

        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public TeamSpeakConnection Connection { get; private set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public InstanceSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>The container.</value>
        public DataContainer Container { get; private set; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>The channel.</value>
        public ChannelData Channel { get; private set; }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>The client.</value>
        public ClientData Client { get; private set; }

        /// <summary>
        /// Gets or sets the compliant.
        /// </summary>
        /// <value>The compliant.</value>
        public CompliantData Compliant { get; private set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        /// <value>The file.</value>
        public FileData File { get; private set; }

        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public ServerData Server { get; private set; }

        /// <summary>
        /// Gets or sets the static.
        /// </summary>
        /// <value>The static.</value>
        public StaticData Static { get; private set; }

        /// <summary>
        /// Keeps the alive.
        /// </summary>
        public void KeepAlive()
        {
            Connection.CredentialEntity.NotificationQueryRunner.GetVersion();
            Connection.CredentialEntity.WorkerQueryRunner.UpdateCurrentQueryClient(new ClientModification { Nickname = Settings.Global.BotNickname });
            Client.CaptureTimes();
            Client.CaptureModeration();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (disposed) return;
            disposed = true;

            Connection.Dispose();
            Container.Dispose();
            Channel.Dispose();
            Client.Dispose();
            Compliant.Dispose();
            File.Dispose();
            Server.Dispose();
            Static.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}