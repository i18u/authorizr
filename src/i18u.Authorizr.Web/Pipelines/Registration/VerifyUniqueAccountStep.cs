using System.Linq;
using i18u.Authorizr.Web.Models;
using i18u.Authorizr.Web.Util;

namespace i18u.Authorizr.Web.Pipelines.Registration
{
    /// <summary>
    /// Verifies that the provided registration form represents a unique registration.
    /// </summary>
    public class VerifyUniqueAccountStep : PipelineStep<RegistrationForm>
    {
        /// <inheritdoc />
        public override string Name => nameof(VerifyUniqueAccountStep);

        private Account GetAccount(string email)
        {
            return Account.Get(email);
        }

        /// <inheritdoc />
        public override RegistrationForm Execute(RegistrationForm input, PipelineContext context)
        {
			if (input == null)
			{
				context.Success = false;
				context.Log($"Input of type {nameof(RegistrationForm)} was null.");
                
                return input;
			}

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