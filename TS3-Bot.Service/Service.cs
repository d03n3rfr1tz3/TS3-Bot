namespace DirkSarodnick.TS3_Bot.Service
{
    using System.ServiceProcess;
    using System.Timers;
    using Core;

    public partial class Service : ServiceBase
    {
        private readonly Timer timer = new Timer();

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer.Elapsed += timer_Elapsed;
            timer.Interval = 800;
            timer.Enabled = true;
            timer.Start();
        }

        protected override void OnStop()
        {
        }

        private static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            BotFactory.Tick();
        }
    }
}