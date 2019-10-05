using System;
using System.Security.Cryptography;
using i18u.Authorizr.Core.Util;

namespace i18u.Authorizr.Core.Crypto.Pbkdf2
{
    /// <summary>
    /// A hash provider that leverages the PBKDF2 hash function.
    /// </summary>
    public class Rfc2898HashProvider : IHashProvider
    {
        private const int DefaultHashSize = 32;
        private const int DefaultIterationCount = 10000;
        private const int DefaultSaltSize = 32;

        private readonly RNGCryptoServiceProvider _cryptoProvider;
        private readonly int _saltSize;

        /// <summary>
        /// Generates a password hash given the salt to use.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt to use when hashing.</param>
        /// <returns>The hashed password.</returns>
        protected string GenerateHash(string password, byte[] salt)
        {
            return GenerateHash(password, salt, DefaultHashSize, DefaultIterationCount);
        }

        /// <summary>
        /// Generates a password hash given the salt, hash size, and iteration count.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt to use.</param>
        /// <param name="hashSize">The size of the output hash.</param>
        /// <param name="iterationCount">The number of iterations to perform.</param>
        /// <returns>The hashed password.</returns>
        protected string GenerateHash(string password, byte[] salt, int hashSize, int iterationCount)
        {
            var saltSize = salt.Length;

            byte[] hashBytes;

            using (var hashFunction = new Rfc2898DeriveBytes(password, salt, iterationCount))
            {
                hashBytes = hashFunction.GetBytes(hashSize);
            }

            var fullBytes = new byte[hashSize + saltSize];
            Array.Copy(salt, 0, fullBytes, 0, saltSize);
            Array.Copy(hashBytes, 0, fullBytes, saltSize, hashSize);

            return Convert.ToBase64String(fullBytes);
        }

        /// <inheritdoc />
        public byte[] GenerateSalt()
        {
            var salt = new byte[_saltSize];

            _cryptoProvider.GetBytes(salt);

            return salt;
        }

        /// <inheritdoc />
        public bool Test(string password, string hash)
        {
            var fullBytes = Convert.FromBase64String(hash);
            var saltBytes = new byte[_saltSize];

            Array.Copy(fullBytes, 0, saltBytes, 0, _saltSize);

            var passwordAttemptHash = GenerateHash(password, saltBytes);
            var passwordAttemptHashBytes = Convert.FromBase64String(passwordAttemptHash);

            return passwordAttemptHashBytes.SlowEquals(fullBytes);
        }

        /// <inheritdoc />
        public string GenerateHash(string password, int hashSize = DefaultHashSize, int iterationCount = DefaultIterationCount)
        {
            var salt = GenerateSalt();
            return GenerateHash(password, salt, hashSize, iterationCount);
        }

        /// <inheritdoc />
        public string GenerateHash(string password)
        {
            return GenerateHash(password, DefaultHashSize, DefaultIterationCount);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _cryptoProvider?.Dispose();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Rfc2898HashProvider"/> class.
        /// </summary>
        /// <param name="saltSize">The size of the salt.</param>
        public Rfc2898HashProvider(int saltSize = DefaultSaltSize)
        {
            _cryptoProvider = new RNGCryptoServiceProvider();
            _saltSize = saltSize;
        }
    }
}