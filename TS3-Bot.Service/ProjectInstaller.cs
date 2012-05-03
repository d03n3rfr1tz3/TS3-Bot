namespace DirkSarodnick.TS3_Bot.Service
{
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.ServiceProcess;

    /// <summary>
    /// Defines the ProjectInstaller class.
    /// </summary>
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectInstaller"/> class.
        /// </summary>
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Committed event of the ServiceInstaller control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Configuration.Install.InstallEventArgs"/> instance containing the event data.</param>
        private void ServiceInstaller_Committed(object sender, InstallEventArgs e)
        {
            new ServiceController(this.ServiceInstaller.ServiceName).Start();
        }
    }
}