namespace DirkSarodnick.TS3_Bot.Service
{
    using System.ComponentModel;
    using System.Configuration.Install;

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
    }
}