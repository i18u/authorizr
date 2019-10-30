using System.Runtime.Serialization;

namespace i18u.Authorizr.Web.Models
{
    /// <summary>
    /// Represents the result of attempting to register.
    /// </summary>
    [DataContract]
    public class RegistrationResult
    {
        /// <summary>
        /// Whether or not the registration was successful.
        /// </summary>
        [DataMember(Name = "success")]
        public bool Success { get; }

        /// <summary>
        /// The message to return to the consuming party.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; }

        /// <summary>
        /// Creates a new instance of the <see cref="RegistrationResult"/> class.
        /// </summary>
        /// <param name="success">Whether or no the action was successful.</param>
        /// <param name="message">The message to return to the consuming party.</param>
        public RegistrationResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}