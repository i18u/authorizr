namespace i18u.Authorizr.Core.Util
{
    /// <summary>
    /// Represents a comparison between two objects, and the outcome.
    /// </summary>
    public interface IComparison
    {
        /// <summary>
        /// One of the items in the comparison.
        /// </summary>
        object ItemA { get; }

        /// <summary>
        /// One of the items in the comparison.
        /// </summary>
        object ItemB { get; }

        /// <summary>
        /// The outcome of the comparison.
        /// </summary>
        bool Equal { get; }
    }
}