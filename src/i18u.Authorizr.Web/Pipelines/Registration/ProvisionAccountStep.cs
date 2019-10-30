using i18u.Authorizr.Web.Models;
using i18u.Authorizr.Web.Util;

namespace i18u.Authorizr.Web.Pipelines.Registration
{
    /// <summary>
    /// Inserts the given <see cref="Account"/> into the backing database.
    /// </summary>
    public class ProvisionAccountStep : PipelineStep<Account, Account>
    {
        /// <inheritdoc />
        public override string Name => nameof(ProvisionAccountStep);

        /// <inheritdoc />
        public override Account Execute(Account input, PipelineContext ctx)
        {
			if (input == null)
			{
				ctx.Success = false;
				ctx.Log("Account object was null - nothing to insert.");
				return input;
			}

            var repo = Mongo.GetRepository<Account>(Account.Database, Account.Collection);
            var result = repo.Insert(input);

            return input;
        }
    }
}