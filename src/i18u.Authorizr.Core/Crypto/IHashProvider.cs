using System;

namespace i18u.Authorizr.Core.Crypto
{
    /// <summary>
    /// A provider for manipulating passwords and hashes.
    /// </summary>
    public interface IHashProvider : IDisposable
    {
        /// <summary>
        /// Generates a password hash given a hash size and iteration count.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="hashSize">The desired size of the resultant hash.</param>
        /// <param name="iterationCount">The number of iterations to perform.</param>
        /// <returns>Returns the salted hash from the given password.</returns>
        string GenerateHash(string password, int hashSize, int iterationCount);

        /// <summary>
        /// Generates a password hash.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>Returns the salted hash from the given password.</returns>
        string GenerateHash(string password);

        /// <summary>
        /// Test the given password against the given hash.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="hash">The hash to check against.</param>
        /// <returns>True if the password matches the provided hash, otherwise false.</returns>
        bool Test(string password, string hash);
    }
}