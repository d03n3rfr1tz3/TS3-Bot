namespace DirkSarodnick.TS3_Bot.Core.Entity
{
    using System;
    using TS3QueryLib.Core.Query.HelperClasses;

    /// <summary>
    /// Defines the FileEntity class.
    /// </summary>
    public class FileEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileEntity"/> class.
        /// </summary>
        /// <param name="serverId">The server id</param>
        /// <param name="channelName">Name of the channel.</param>
        /// <param name="fileEntry">The file entry.</param>
        public FileEntity(uint serverId, string channelName, FileTransferFileEntry fileEntry)
        {
            ServerId = serverId;
            ChannelName = channelName;
            ChannelId = fileEntry.ChannelId;
            Name = fileEntry.Name;
            Size = fileEntry.Size;
            Created = fileEntry.Created;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileEntity"/> class.
        /// </summary>
        /// <param name="serverId">The server id</param>
        /// <param name="channelName">Name of the channel.</param>
        /// <param name="fileEntry">The file entry.</param>
        /// <param name="path">The path.</param>
        public FileEntity(uint serverId, string channelName, FileTransferFileEntry fileEntry, string path)
        {
            ServerId = serverId;
            ChannelName = channelName;
            ChannelId = fileEntry.ChannelId;
            Name = path.Remove(0, 1) + fileEntry.Name;
            Size = fileEntry.Size;
            Created = fileEntry.Created;
        }

        /// <summary>
        /// Gets or sets the server id.
        /// </summary>
        /// <value>The server id.</value>
        public uint ServerId { get; set; }

        /// <summary>
        /// Gets or sets the channel id.
        /// </summary>
        /// <value>The channel id.</value>
        public uint ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the name of the channel.
        /// </summary>
        /// <value>The name of the channel.</value>
        public string ChannelName { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public ulong Size { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>The created.</value>
        public DateTime Created { get; set; }
    }
}