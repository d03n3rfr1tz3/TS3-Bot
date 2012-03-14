namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses.MessageSetting
{
    using System.Xml.Serialization;

    public enum MessageType
    {
        /// <summary>
        /// The global advert message type.
        /// </summary>
        [XmlEnum("Global")]
        Global,

        /// <summary>
        /// The welcome message type.
        /// </summary>
        [XmlEnum("Welcome")]
        Welcome,

        /// <summary>
        /// The advert message type.
        /// </summary>
        [XmlEnum("Advert")]
        Advert
    }
}