namespace DirkSarodnick.TS3_Bot.Core.Settings
{
    using System.Xml.Serialization;

    /// <summary>
    /// Defines the PunishBehavior enum.
    /// </summary>
    public enum PunishBehavior
    {
        /// <summary>
        /// Move the client to sticky channel
        /// </summary>
        [XmlEnum(Name = "Sticky")]
        MoveToSticky,

        /// <summary>
        /// Kicks the client from the current channel
        /// </summary>
        [XmlEnum(Name = "Channel")]
        KickFromChannel,

        /// <summary>
        /// Kicks the client from the server
        /// </summary>
        [XmlEnum(Name = "Server")]
        KickFromServer
    }
}