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
            var equal = true;
            var lengths = new List<int>() { 0 };

            if (a == null)
            {
                equal = false;
            }
            else
            {
                lengths.Add(a.Count());
            }

            if (b == null)
            {
                equal = false;
            }
            else
            {
                lengths.Add(b.Count());
            }

            if (a.Count() != b.Count())
            {
                equal = false;
            }

            var length = lengths.Max();

            for (var index = 0; index < length; index++)
            {
                T itemA = default;
                T itemB = default;

                if (a.Count() > index)
                {
                    itemA = a.ElementAt(index);
                }

                if (b.Count() > index)
                {
                    itemB = b.ElementAt(index);
                }

                if (!Equals(itemA, itemB))
                {
                    equal = false;
                }
            }

            return equal;
        }
    }
}