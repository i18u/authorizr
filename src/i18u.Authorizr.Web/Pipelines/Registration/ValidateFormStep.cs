using i18u.Authorizr.Web.Models;

namespace i18u.Authorizr.Web.Pipelines.Registration
{
    /// <summary>
    /// The step responsible for validating input from the registration form.
    /// </summary>
    public class ValidateFormStep : PipelineStep<RegistrationForm>
    {
        /// <inheritdoc />
        public override string Name => nameof(ValidateFormStep);

        /// <inheritdoc />
        public override RegistrationForm Execute(RegistrationForm input, PipelineContext ctx)
        {
            if (string.IsNullOrWhiteSpace(input.EmailAddress)) 
            {
                ctx.Log("An email address must be provided when registering.");
                ctx.Success = false;
            }

            if (string.IsNullOrWhiteSpace(input.Password))
            {
                ctx.Log("A password must be provided when registering.");
                ctx.Success = false;
            }

            return input;
        }
    }
}