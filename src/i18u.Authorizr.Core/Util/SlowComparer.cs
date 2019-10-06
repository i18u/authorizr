using System;
using System.Collections.Generic;
using System.Linq;

namespace i18u.Authorizr.Core.Util
{
    /// <summary>
    /// A comparison tool for non-short-circuiting collection comparison.
    /// </summary>
    public class SlowComparer
    {
        /// <summary>
        /// Fired when two items are compared by this comparer.
        /// </summary>
        public event EventHandler<IComparison> Compared;

        /// <summary>
        /// Performs the comparison between the first collection and the second 
        /// collection.
        /// </summary>
        /// <param name="a">One of the collections to compare.</param>
        /// <param name="b">One of the collections to compare.</param>
        /// <typeparam name="T">The type of the items in the collection.</typeparam>
        /// <returns>True if the collections are equal, otherwise false.</returns>
        public bool Equals<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            var equal = false;
            var lengths = new List<int>() { 0 };

            // Initialize these so we don't have to do comparisons/enumerations
            // multiple times.
            int? aCount = null;
            int? bCount = null;

            if (a != null)
            {
                aCount = a.Count();
                lengths.Add(aCount.Value);
            }

            if (b != null)
            {
                bCount = b.Count();
                lengths.Add(bCount.Value);
            }

            // Get the larger collection length (if of differing sizes), or zero
            var length = lengths.Max();
            bool? previousResult = null;

            for (var index = 0; index < length; index++)
            {
                var itemA = GetItemAtOrDefault(a, index);
                var itemB = GetItemAtOrDefault(b, index);

                var itemsEqual = Equals(itemA, itemB);
                OnCompared(itemA, itemB, itemsEqual);

                // If our items are equal
                if (itemsEqual)
                {
                    // ... and we haven't done a comparison before, or our last
                    // comparison was also a match.
                    if (!previousResult.HasValue || previousResult.Value)
                    {
                        equal = true;
                    }
                }
                else
                {
                    equal = false;
                }

                // We update our previous result here because otherwise we'd
                // run into problems with the check above.
                previousResult = equal;
            }

            return equal;
        }

        /// <summary>
        /// Gets the item at the specified index, otherwise returns default
        /// </summary>
        /// <param name="collection">The collection to get the item from.</param>
        /// <param name="index">The index to retrieve the item at.</param>
        /// <typeparam name="T">The type of the item to return.</typeparam>
        /// <returns>The item at the given index in the collection, otherwise default.</returns>
        private T GetItemAtOrDefault<T>(IEnumerable<T> collection, int index)
        {
            // If our collection is empty, bail
            if (collection == null) 
            {
                return default;
            }

            int count = collection.Count();

            // If the number of items is greater than the index, the index will
            // be within bounds. If they are equal, the index will point at just
            // outside the collection.
            if (count > index)
            {
                return collection.ElementAt(index);
            }

            return default;
        }

        /// <summary>
        /// Fires the <see cref="Compared"/> event.
        /// </summary>
        /// <param name="a">One of the items in the comparison.</param>
        /// <param name="b">The other item in the comparison.</param>
        /// <param name="itemsEqual">
        /// Whether or not the items were deemed equal by the comparer.
        /// </param>
        protected virtual void OnCompared(object a, object b, bool itemsEqual)
        {
            var eventHandlers = Compared;

            eventHandlers?.Invoke(this, new Comparison(a, b, itemsEqual));
        }
    }
}