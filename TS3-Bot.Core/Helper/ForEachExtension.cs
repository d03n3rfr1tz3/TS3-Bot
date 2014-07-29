namespace DirkSarodnick.TS3_Bot.Core.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the ForEachExtension extension.
    /// </summary>
    public static class ForEachExtension
    {
        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <typeparam name="T">any object.</typeparam>
        /// <param name="list">The list of objects.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            var array = list.ToArray();
            var count = array.Length;
            for (var i = 0; i < count; i++)
            {
                action(array[i]);
            }
        }
    }
}