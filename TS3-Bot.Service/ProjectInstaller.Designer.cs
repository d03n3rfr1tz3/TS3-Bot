namespace DirkSarodnick.TS3_Bot.Service
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.ServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ServiceProcessInstaller
            // 
            this.ServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ServiceProcessInstaller.Password = null;
            this.ServiceProcessInstaller.Username = null;
            // 
            // ServiceInstaller
            // 
            this.ServiceInstaller.DisplayName = "TeamSpeak 3 Bot - Dirk Sarodnick";
            this.ServiceInstaller.ServiceName = "TS3-Bot";
            this.ServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.ServiceInstaller.Committed += new System.Configuration.Install.InstallEventHandler(this.ServiceInstaller_Committed);
            this.ServiceInstaller.BeforeInstall += new System.Configuration.Install.InstallEventHandler(this.ServiceInstaller_BeforeInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ServiceInstaller,
            this.ServiceProcessInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller ServiceInstaller;
    }
}