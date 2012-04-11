namespace DirkSarodnick.TS3_Bot.TestConsole
{
    using System;
    using System.Timers;
    using Core;

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
    }
}