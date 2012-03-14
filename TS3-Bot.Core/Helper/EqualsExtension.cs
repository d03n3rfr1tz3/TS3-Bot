namespace DirkSarodnick.TS3_Bot.Core.Helper
{
    /// <summary>
    /// Defines the EqualsExtension extension.
    /// </summary>
    public static class EqualsExtension
    {
        /// <summary>
        /// Determines wether the object equals any of the given objects.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="objArray">The obj array.</param>
        /// <returns>True or False</returns>
        public static bool EqualsAny(this uint obj, params uint[] objArray)
        {
            var result = true;

            var count = objArray.Length;
            for (var i = 0; i < count; i++)
            {
                result &= obj == objArray[i];
            }

            return result;
        }
    }
}