using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        /// <returns>An array containing 'value1', and 'value2'.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Create a registration entry.
        /// </summary>
        /// <param name="form">The registration form.</param>
        /// <returns>The (currently) string result.</returns>
        [HttpPost]
        public ActionResult<string> Create([FromBody] RegistrationForm form)
        {
            var pipeline = Pipeline
                .Create(new ValidateFormStep())
                .Then((email, ctx) => email);

            var pipelineContext = new PipelineContext();

            return Ok(pipeline.Execute(form, pipelineContext));
        }
    }
}
