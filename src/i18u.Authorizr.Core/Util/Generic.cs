using System.Collections.Generic;
using System.Linq;

namespace i18u.Authorizr.Core.Util
{
    /// <summary>
    /// Generic helper methods for all kinds of objects/collections.
    /// </summary>
    internal static class Generic
    {
        /// <summary>
        /// Does a non-short-ciruiting, element-by-element comparison between two collections.
        /// </summary>
        /// <param name="a">Collection a to compare.</param>
        /// <param name="b">Collection b to compare.</param>
        /// <typeparam name="T">The type of the input objects.</typeparam>
        /// <returns>True if inputs match, otherwise false.</returns>
        public static bool SlowEquals<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            var slowComparer = new SlowComparer();
            return slowComparer.Equals(a, b);
        }
    }
}