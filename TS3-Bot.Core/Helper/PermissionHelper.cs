namespace DirkSarodnick.TS3_Bot.Core.Helper
{
    using System.Collections.Generic;
    using System.Linq;
    using Settings;

    /// <summary>
    /// Defines the PermissionHelper class.
    /// </summary>
    public class PermissionHelper
    {
        /// <summary>
        /// Determines whether the specified setting is granted.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <param name="group">The group.</param>
        /// <returns>
        /// 	<c>true</c> if the specified setting is granted; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsGranted(ISettings setting, uint group)
        {
            if (setting.DeniedServerGroups != null && setting.DeniedServerGroups.Any())
            {
                return !setting.DeniedServerGroups.Any(g => g == group);
            }

            if (setting.PermittedServerGroups != null && setting.PermittedServerGroups.Any())
            {
                return setting.PermittedServerGroups.Any(g => g == group);
            }

            return true;
        }

        /// <summary>
        /// Determines whether the specified setting is granted.
        /// </summary>
        /// <param name="setting">The setting.</param>
        /// <param name="groups">The groups.</param>
        /// <returns>
        /// 	<c>true</c> if the specified setting is granted; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsGranted(ISettings setting, List<uint> groups)
        {
            var result = true;

            if (setting.DeniedServerGroups.Any())
            {
                result &= !setting.DeniedServerGroups.Any(groups.Contains);
            }

            if (setting.PermittedServerGroups.Any())
            {
                result &= setting.PermittedServerGroups.Any(groups.Contains);
            }

            return result;
        }
    }
}