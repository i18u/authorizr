using System;
using System.Collections.Generic;
using System.Net;
using i18u.Authorizr.Web.Models;
using i18u.Authorizr.Web.Pipelines;
using i18u.Authorizr.Web.Pipelines.Registration;
using Microsoft.AspNetCore.Mvc;

namespace i18u.Authorizr.Web.Controllers
{
    /// <summary>
    /// The API route controller for account information.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Retrieves some default values.
        /// </summary>
        /// <returns>The matching <see cref="Account"/> object.</returns>
        [HttpGet("{email}")]
        public ActionResult<IEnumerable<Account>> Get(string email)
        {
            return Ok(Account.Get(email));
        }

        /// <summary>
        /// Create a registration entry.
        /// </summary>
        /// <param name="form">The registration form.</param>
        /// <returns>The (currently) string result.</returns>
        [HttpPost]
        public ActionResult<RegistrationResult> Create([FromBody] RegistrationForm form)
        {
            var pipeline = Pipeline
                .Create(new ValidateFormStep())
                .Then(new VerifyUniqueAccountStep())
                .Then(new CreateAccountObjectStep())
                .Then(new ProvisionAccountStep())
                .Then(new SendVerificationEmailStep());

            var pipelineContext = new PipelineContext();
            RegistrationResult result = null;

            try 
            {
                result = pipeline.Execute(form, pipelineContext);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went terribly, terribly wrong: {ex}");
            }

            if (result == null)
            {
                Console.WriteLine("Something failed.");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            return Ok(result);
        }
    }
}
