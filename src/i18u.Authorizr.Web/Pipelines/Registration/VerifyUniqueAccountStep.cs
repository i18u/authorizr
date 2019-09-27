using i18u.Authorizr.Web.Models;

namespace i18u.Authorizr.Web.Pipelines.Registration
{
    /// <summary>
    /// Verifies that the provided registration form represents a unique registration.
    /// </summary>
    public class VerifyUniqueAccountStep : PipelineStep<RegistrationForm>
    {
        private object GetAccount(string email)
        {
            return default;
        }

        /// <inheritdoc />
        public override RegistrationForm Execute(RegistrationForm input, PipelineContext context)
        {
            var user = GetAccount(input.EmailAddress);

            if (user != null)
            {
                context.Success = false;
                // Potential enumeration attack vector
                context.Log("There is already a user account for the provided email address."); 
            }

            return input;
        }
    }
}