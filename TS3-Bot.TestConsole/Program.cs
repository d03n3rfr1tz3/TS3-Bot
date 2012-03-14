
namespace DirkSarodnick.TS3_Bot.TestConsole
{
    using System;
    using System.Timers;
    using Core;

    class Program
    {
        private static readonly Timer timer = new Timer();

        static void Main(string[] args)
        {
            timer.Elapsed += timer_Elapsed;
            timer.Interval = 800;
            timer.Enabled = true;
            timer.Start();
            Console.Read();
        }

        private static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            BotFactory.Tick();
        }
    }
}