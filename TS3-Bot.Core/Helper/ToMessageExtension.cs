namespace DirkSarodnick.TS3_Bot.Core.Helper
{
    using System.Text;
    using System.Text.RegularExpressions;
    using Entity;

    /// <summary>
    /// Defines the ToMessageExtension extension.
    /// </summary>
    public static class ToMessageExtension
    {
        /// <summary>
        /// Toes the message.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="context">The context.</param>
        /// <returns>The message.</returns>
        public static string ToMessage(this string value, MessageContext context)
        {
            var result = new StringBuilder(value);

            if (context.ClientDatabaseId.HasValue)
            {
                result.Replace("[CLIENT_ID]", context.ClientDatabaseId.Value.ToString());
            }

            if (context.ClientNickname != null)
            {
                result.Replace("[CLIENT_NICKNAME]", context.ClientNickname);
            }

            if (context.ClientLastLogin != null)
            {
                result.Replace("[CLIENT_LASTLOGIN]", context.ClientLastLogin);
            }

            if (context.ClientLastSeen != null)
            {
                result.Replace("[CLIENT_LASTSEEN]", context.ClientLastSeen);
            }

            if (context.ChannelId.HasValue)
            {
                result.Replace("[CHANNEL_ID]", context.ChannelId.Value.ToString());
            }

            if (context.ChannelName != null)
            {
                result.Replace("[CHANNEL_NAME]", context.ChannelName);
            }

            if (context.ServerId.HasValue)
            {
                result.Replace("[SERVER_ID]", context.ServerId.Value.ToString());
            }

            if (context.ServerName != null)
            {
                result.Replace("[SERVER_NAME]", context.ServerName);
            }

            if (context.ServerPort.HasValue)
            {
                result.Replace("[SERVER_PORT]", context.ServerPort.Value.ToString());
            }

            if (context.EventName != null)
            {
                result.Replace("[EVENT_NAME]", context.EventName);
            }

            if (context.FileName != null)
            {
                result.Replace("[FILE_NAME]", context.FileName);
            }

            if (context.FileCreated != null)
            {
                result.Replace("[FILE_CREATED]", context.FileCreated);
            }

            if (context.FileSize.HasValue)
            {
                result.Replace("[FILE_SIZE]", BasicHelper.GetFileSize(context.FileSize.Value));
            }

            // killing some unnecessary whitespaces
            var resultString = result.Replace("\t", " ").ToString().Trim();
            var whitespaceKiller = new Regex("([ ]{2,})", RegexOptions.Compiled);

            while (whitespaceKiller.IsMatch(resultString))
            {
                resultString = whitespaceKiller.Replace(resultString, " ");
            }

            return resultString.Replace("\n ", "\n").Replace("\r ", "\r");
        }

        /// <summary>
        /// Toes the message.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The message.</returns>
        public static string ToMessage(this string value)
        {
            return ToMessage(value, new MessageContext());
        }
    }
}