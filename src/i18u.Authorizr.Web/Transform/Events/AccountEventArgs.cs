using System;
using i18u.Authorizr.Web.Models;

namespace i18u.Authorizr.Web.Transform.Events
{
    /// <summary>
    /// Custom arguments for <see cref="Account"/> events.
    /// </summary>
    public class AccountEventArgs : EventArgs
    {
        /// <summary>
        /// Represents the action that was undertaken.
        /// </summary>
        public string Action { get; }

        /// <summary>
        /// Represents whether the action was successful.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="AccountEventArgs"/> object.
        /// </summary>
        public AccountEventArgs(string action, bool success)
        {
            Action = action;
            Success = success;
        }
    }
}