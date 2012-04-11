namespace DirkSarodnick.TS3_Bot.Core.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Base;
    using Entity;
    using Helper;

    /// <summary>
    /// Defines the FileData class.
    /// </summary>
    public class FileData : DefaultData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileData"/> class.
        /// </summary>
        /// <param name="repository"></param>
        public FileData(DataRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// Gets the file list.
        /// </summary>
        /// <returns>The file list</returns>
        public List<FileEntity> GetFileList(uint serverId)
        {
            Repository.Channel.GetChannelList(serverId).ForEach(c => GetFiles(serverId, c.ChannelId, GetFileListByChannel(serverId, c.ChannelId, c.Name)));
            return Container.FileList.Where(m => m.ServerId == serverId).ToList();
        }

        /// <summary>
        /// Gets the files of a channel.
        /// </summary>
        /// <param name="serverId">The server id.</param>
        /// <param name="channelId">The channel id.</param>
        /// <param name="fileList">The file list.</param>
        private void GetFiles(uint serverId, uint channelId, IEnumerable<FileEntity> fileList)
        {
            lock (Container.lockFileList)
            {
                Container.FileList.RemoveAll(f => f.ServerId == serverId && f.ChannelId == channelId);
                Container.FileList.AddRange(fileList);
            }
        }

        /// <summary>
        /// Gets the file list by channel.
        /// </summary>
        /// <param name="serverId">The server id.</param>
        /// <param name="channelId">The channel id.</param>
        /// <param name="channelName">Name of the channel.</param>
        /// <returns>The file list</returns>
        private IEnumerable<FileEntity> GetFileListByChannel(uint serverId, uint channelId, string channelName)
        {
            var result = new List<FileEntity>();
            var files = DynamicQueryRunner(serverId).GetFileList(channelId, "/").ToList();

            files.Where(f => f.Type == 1).ForEach(f => result.Add(new FileEntity(serverId, channelName, f)));
            files.Where(f => f.Type == 0)
                .ForEach(f => result.AddRange(GetFileListByChannel(serverId, f.ChannelId, channelName, string.Format("/{0}/", f.Name))));

            return result;
        }

        /// <summary>
        /// Gets the file list by channel.
        /// </summary>
        /// <param name="serverId">The server id.</param>
        /// <param name="channelId">The channel id.</param>
        /// <param name="channelName">Name of the channel.</param>
        /// <param name="path">The path.</param>
        /// <returns>The file list</returns>
        private IEnumerable<FileEntity> GetFileListByChannel(uint serverId, uint channelId, string channelName, string path)
        {
            var result = new List<FileEntity>();
            var files = DynamicQueryRunner(serverId).GetFileList(channelId, path).ToList();

            files.Where(f => f.Type == 1).ForEach(f => result.Add(new FileEntity(serverId, channelName, f, path)));
            files.Where(f => f.Type == 0)
                .ForEach(f => result.AddRange(GetFileListByChannel(serverId, f.ChannelId, channelName, string.Format("{0}{1}/", path, f.Name))));

            return result;
        }
    }
}