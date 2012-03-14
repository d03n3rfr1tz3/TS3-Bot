namespace DirkSarodnick.TS3_Bot.Core.Settings
{
    using System;
    using Helper;
    using SettingClasses;

    /// <summary>
    /// Defines the Settings class.
    /// </summary>
    [Serializable]
    public class InstanceSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceSettings"/> class.
        /// </summary>
        public InstanceSettings() : this(new GlobalSettings())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public InstanceSettings(GlobalSettings globalSettings)
        {
            Global = globalSettings;

            Away = new AwaySettings();
            BadNickname = new BadNicknameSettings();
            Control = new ControlSettings();
            Event = new EventSettings();
            Idle = new IdleSettings();
            Message = new MessageSettings();
            Record = new RecordSettings();
            Sticky = new StickySettings();
            TeamSpeak = new TeamSpeakServerSettings();
            Vote = new VoteSettings();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="InstanceSettings"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the global.
        /// </summary>
        /// <value>The global.</value>
        public GlobalSettings Global { get; set; }

        /// <summary>
        /// Gets or sets the away.
        /// </summary>
        /// <value>The away.</value>
        public AwaySettings Away { get; set; }

        /// <summary>
        /// Gets or sets the bad nickname.
        /// </summary>
        /// <value>The bad nickname.</value>
        public BadNicknameSettings BadNickname { get; set; }

        /// <summary>
        /// Gets or sets the control.
        /// </summary>
        /// <value>The control.</value>
        public ControlSettings Control { get; set; }

        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        /// <value>The event.</value>
        public EventSettings Event { get; set; }

        /// <summary>
        /// Gets or sets the idle.
        /// </summary>
        /// <value>The idle.</value>
        public IdleSettings Idle { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public MessageSettings Message { get; set; }

        /// <summary>
        /// Gets or sets the record.
        /// </summary>
        /// <value>The record.</value>
        public RecordSettings Record { get; set; }

        /// <summary>
        /// Gets or sets the sticky.
        /// </summary>
        /// <value>The sticky.</value>
        public StickySettings Sticky { get; set; }

        /// <summary>
        /// Gets or sets the team speak.
        /// </summary>
        /// <value>The team speak.</value>
        public TeamSpeakServerSettings TeamSpeak { get; set; }

        /// <summary>
        /// Gets or sets the vote.
        /// </summary>
        /// <value>The vote.</value>
        public VoteSettings Vote { get; set; }

        /// <summary>
        /// Applies the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void ApplySettings(InstanceSettings settings)
        {
            this.Enabled = settings.Enabled;

            this.Away.ApplySetting(settings.Away);
            this.BadNickname.ApplySetting(settings.BadNickname);
            this.Control.ApplySetting(settings.Control);
            this.Event.ApplySetting(settings.Event);
            this.Idle.ApplySetting(settings.Idle);
            this.Message.ApplySetting(settings.Message);
            this.Record.ApplySetting(settings.Record);
            this.Sticky.ApplySetting(settings.Sticky);
            this.Vote.ApplySetting(settings.Vote);

            if (!string.IsNullOrEmpty(settings.Global.BotNickname))
                this.Global.BotNickname = settings.Global.BotNickname;
            if (BasicHelper.IsValidCulture(settings.Global.Globalization))
                this.Global.Globalization = settings.Global.Globalization;

            this.TeamSpeak.Host = settings.TeamSpeak.Host;
            this.TeamSpeak.Instance = settings.TeamSpeak.Instance;
            this.TeamSpeak.QueryPort = settings.TeamSpeak.QueryPort;
            this.TeamSpeak.Username = settings.TeamSpeak.Username;
            this.TeamSpeak.Password = settings.TeamSpeak.Password;
        }
    }
}