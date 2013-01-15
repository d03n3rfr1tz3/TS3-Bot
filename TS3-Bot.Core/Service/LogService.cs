namespace DirkSarodnick.TS3_Bot.Core.Service
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Defines the LogService class.
    /// </summary>
    public class LogService
    {
        public delegate void LogHandler(EventLogEntryType type, string message);
        public static event LogHandler Log;

        /// <summary>
        /// Called when [log].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        public static void OnLog(EventLogEntryType type, string message)
        {
            LogHandler handler = Log;
            if (handler != null) handler(type, message);
            if (type == EventLogEntryType.Information) return;

            try
            {
                var log = new EventLog { Log = "Application", Source = "TS3-Bot" };
                log.WriteEntry(message, type);
                log.Close();
            }
            catch
            { }
        }

        /// <summary>
        /// Called when [log].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="exception">The exception.</param>
        public static void OnLog(EventLogEntryType type, Exception exception)
        {
            LogHandler handler = Log;
            if (handler != null) handler(type, exception.Message);
            if (type == EventLogEntryType.Information) return;

            try
            {
                var log = new EventLog { Log = "Application", Source = "TS3-Bot" };
                log.WriteEntry(exception + (exception.InnerException != null ? exception.InnerException.ToString() : string.Empty), type);
                log.Close();
            }
            catch
            { }
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Error(string message)
        {
            OnLog(EventLogEntryType.Error, message);
        }

        /// <summary>
        /// Errors the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public static void Error(Exception exception)
        {
            OnLog(EventLogEntryType.Error, exception);
        }

        /// <summary>
        /// Warnings the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Warning(string message)
        {
            OnLog(EventLogEntryType.Warning, message);
        }

        /// <summary>
        /// Debugs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Debug(string message)
        {
            OnLog(EventLogEntryType.Information, message);
        }
    }
}