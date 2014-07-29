namespace DirkSarodnick.TS3_Bot.Service
{
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.Linq;
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
            using (var service = new ServiceController(this.ServiceInstaller.ServiceName))
            {
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);
                service.Close();
            }
        }

        /// <summary>
        /// Handles the BeforeInstall event of the ServiceInstaller control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Configuration.Install.InstallEventArgs"/> instance containing the event data.</param>
        private void ServiceInstaller_BeforeInstall(object sender, InstallEventArgs e)
        {
            var services = ServiceController.GetServices();
            var service = services.FirstOrDefault(s => s.ServiceName == this.ServiceInstaller.ServiceName);
            if (service != null && service.CanStop)
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
                service.Close();
            }
        }
    }
}