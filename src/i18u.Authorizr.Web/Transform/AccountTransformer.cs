using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using i18u.Authorizr.Core.Crypto;
using i18u.Authorizr.Core.Crypto.Pbkdf2;
using i18u.Authorizr.Web.Models;
using i18u.Authorizr.Web.Transform.Events;
using i18u.Authorizr.Web.Util;

namespace i18u.Authorizr.Web.Transform
{
    /// <summary>
    /// The class responsible for generating <see cref="Account"/> objects.
    /// </summary>
    internal class AccountTransformer : IDisposable
    {
        private readonly IHashProvider _hashProvider;

        /// <summary>
        /// The event handler delegate type for Account actions.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The arguments related to what occurred.</param>
        public delegate void AccountEventHandler(object sender, AccountEventArgs args);

        /// <summary>
        /// Fires when the account transformer has generated an <see cref="Account"/>.
        /// </summary>
        public event AccountEventHandler Generated;

        /// <summary>
        /// Creates a new instance of the <see cref="AccountTransformer"/> class.
        /// </summary>
        public AccountTransformer()
        {
            _hashProvider = new Rfc2898HashProvider();
        }

        private string GenerateHash(string password)
        {
            return _hashProvider.GenerateHash(password);
        }

        private bool HashesMatch(string password, string hash)
        {
            return _hashProvider.Test(password, hash);
        }

        /// <summary>
        /// Generates an account from the given <see cref="RegistrationForm"/> input.
        /// </summary>
        /// <returns>The generated <see cref="Account"/> object.</returns>
        public Account Generate(RegistrationForm form)
        {
			if (form == null)
			{
				return default;
			}

            var email = form.EmailAddress;
            var password = form.Password;

            var passwordHashAndSalt = GenerateHash(password);

            var account = new Account(email, passwordHashAndSalt);
            OnGenerated(this, "create", true);

            return account;
        }

        /// <summary>
        /// Fires the <see cref="Generated"/> event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="action">The action that was taken.</param>
        /// <param name="success">Whether or not the action was successful.</param>
        protected virtual void OnGenerated(object sender, string action, bool success)
        {
            Generated?.Invoke(this, new AccountEventArgs(action, success));
        }

        /// <summary>
        /// Disposes of the underlying crypto providers.
        /// </summary>
        public void Dispose()
        {
            _hashProvider?.Dispose();
        }
    }
}