namespace DirkSarodnick.TS3_Bot.TestConsole
{
    using System;
    using System.Diagnostics;
    using System.Timers;
    using Core;
    using Core.Service;

    /// <summary>
    /// Defines the Program class.
    /// </summary>
    class Program
    {
        private static readonly Timer Timer = new Timer();

        /// <summary>
        /// Mains the specified args.
        /// </summary>
        /// <param name="args">The args.</param>
        static void Main(string[] args)
        {
            Timer.Elapsed += timer_Elapsed;
            Timer.Interval = 800;
            Timer.Enabled = true;
            Timer.Start();
            LogService.Log += LogServiceOnLog;
            Console.Read();
        }

        /// <summary>
        /// Handles the Elapsed event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs"/> instance containing the event data.</param>
        private static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            BotFactory.Tick();
        }

        /// <summary>
        /// Logs the service on log.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        private static void LogServiceOnLog(EventLogEntryType type, string message)
        {
            Console.WriteLine("{0}: {1}", type, message);
        }
    }
}