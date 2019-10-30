namespace i18u.Authorizr.Web.Models
{
    /// <summary>
    /// Represents a form filled with information for registering a user.
    /// </summary>
    public class RegistrationForm
    {
        /// <summary>
        /// The unique identifier the user will use to login.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The password the user will use to authenticate.
        /// </summary>
        public string Password { get; set; }
    }
}