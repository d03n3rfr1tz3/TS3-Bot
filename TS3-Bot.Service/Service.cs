namespace DirkSarodnick.TS3_Bot.Service
{
    using System.Diagnostics;
    using System.IO;
    using System.ServiceProcess;
    using System.Timers;
    using Core;

    /// <summary>
    /// Defines the Service class.
    /// </summary>
    public partial class Service : ServiceBase
    {
        private readonly Timer timer = new Timer();

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        public Service()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            timer.Elapsed += timer_Elapsed;
            timer.Interval = 800;
            timer.Enabled = true;
            timer.Start();

            Process pc = Process.GetCurrentProcess();
            Directory.SetCurrentDirectory(pc.MainModule.FileName.Substring(0, pc.MainModule.FileName.LastIndexOf(@"\", System.StringComparison.Ordinal)));
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
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