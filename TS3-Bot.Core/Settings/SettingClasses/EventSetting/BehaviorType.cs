namespace DirkSarodnick.TS3_Bot.Core.Settings.SettingClasses.EventSetting
{
    using System.Xml.Serialization;

    public enum BehaviorType
    {
        /// <summary>
        /// Pokes the Client
        /// </summary>
        [XmlEnum("Poke")]
        Poke,

        /// <summary>
        /// Sends private message to Client
        /// </summary>
        [XmlEnum("Message")]
        Message,

        /// <summary>
        /// Sends a webrequest with Client id
        /// </summary>
        [XmlEnum("Web")]
        WebRequest
    }
}