namespace DirkSarodnick.TS3_Bot.Core.Helper
{
    using Settings.SettingClasses;
    using TS3QueryLib.Core.Server.Entities;

    /// <summary>
    /// Defines some Extensions for Away features.
    /// </summary>
    public static class AwayClientExtension
    {
        /// <summary>
        /// Determines whether the specified client is away.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>
        ///   <c>true</c> if the specified client is away; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAway(this ClientListEntry client, AwaySettings settings)
        {
            return settings.Enabled && settings.Channel > 0 && client.IsClientAway.GetValueOrDefault();
        }

        /// <summary>
        /// Determines whether [is input muted] [the specified client].
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>
        ///   <c>true</c> if [is input muted] [the specified client]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInputMuted(this ClientListEntry client, AwaySettings settings)
        {
            return settings.Enabled && settings.MuteMicrophoneChannel > 0 && client.IsClientInputMuted.GetValueOrDefault();
        }

        /// <summary>
        /// Determines whether [is output muted] [the specified client].
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>
        ///   <c>true</c> if [is output muted] [the specified client]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsOutputMuted(this ClientListEntry client, AwaySettings settings)
        {
            return settings.Enabled && settings.MuteHeadphoneChannel > 0 && client.IsClientOutputMuted.GetValueOrDefault();
        }

        /// <summary>
        /// Determines whether the specified client is idle.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>
        ///   <c>true</c> if the specified client is idle; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsIdle(this ClientListEntry client, IdleSettings settings)
        {
            return settings.Enabled && settings.Channel > 0 && client.ClientIdleDuration.GetValueOrDefault().TotalMinutes >= settings.IdleTime;
        }

        /// <summary>
        /// Gets the away channel by the clients away state.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>the away channel.</returns>
        public static uint? GetAwayChannelByState(this ClientListEntry client, AwaySettings settings)
        {
            if (client.IsAway(settings)) return settings.Channel;
            if (client.IsInputMuted(settings)) return settings.MuteMicrophoneChannel;
            if (client.IsOutputMuted(settings)) return settings.MuteHeadphoneChannel;
            return null;
        }
    }
}