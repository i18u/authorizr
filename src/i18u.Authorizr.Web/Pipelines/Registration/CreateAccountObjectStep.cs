using i18u.Authorizr.Web.Models;
using i18u.Authorizr.Web.Transform;

namespace i18u.Authorizr.Web.Pipelines.Registration
{
    /// <summary>
    /// The step representing the transformation of a <see cref="RegistrationForm"/>
    /// object into an <see cref="Account"/> object.
    /// </summary>
    public class CreateAccountObjectStep : PipelineStep<RegistrationForm, Account>
    {
        /// <inheritdoc />
        public override string Name => nameof(CreateAccountObjectStep);

        /// <inheritdoc />
        public override Account Execute(RegistrationForm input, PipelineContext ctx)
        {
            var accountTransformer = new AccountTransformer();
            var account = accountTransformer.Generate(input);

            return account;
        }
    }
}