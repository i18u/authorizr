namespace i18u.Authorizr.Core.Util
{
    /// <summary>
    /// Implementation of the <see cref="IComparison"/> interface.
    /// </summary>
    internal class Comparison : IComparison
    {
        /// <inheritdoc />
        public object ItemA { get; }

        /// <inheritdoc />
        public object ItemB { get; }

        /// <inheritdoc />
        public bool Equal { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="Comparison"/> class.
        /// </summary>
        /// <param name="itemA">One of the items compared.</param>
        /// <param name="itemB">One of the items compared.</param>
        /// <param name="equal">Whether or not the items are equal.</param>
        public Comparison(object itemA, object itemB, bool equal)
        {
            ItemA = itemA;
            ItemB = itemB;
            Equal = equal;
        }
    }
}