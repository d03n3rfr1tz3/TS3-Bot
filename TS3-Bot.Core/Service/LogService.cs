namespace DirkSarodnick.TS3_Bot.Core.Service
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Defines the LogService class.
    /// </summary>
    public class LogService
    {
        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Error(string message)
        {
            try
            {
                var log = new EventLog { Log = "Application", Source = "TS3-Bot" };
                log.WriteEntry(message, EventLogEntryType.Error);
                log.Close();
            }
            catch
            { }
        }

        /// <summary>
        /// Errors the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public static void Error(Exception exception)
        {
            Error(exception.ToString());
        }

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Warning(string message)
        {
            try
            {
                var log = new EventLog { Log = "Application", Source = "TS3-Bot" };
                log.WriteEntry(message, EventLogEntryType.Warning);
                log.Close();
            }
            catch
            { }
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Debug(string message)
        {
            try
            {
                var log = new EventLog { Log = "Application", Source = "TS3-Bot" };
                log.WriteEntry(message, EventLogEntryType.Information);
                log.Close();
            }
            catch
            { }
        }
    }
}