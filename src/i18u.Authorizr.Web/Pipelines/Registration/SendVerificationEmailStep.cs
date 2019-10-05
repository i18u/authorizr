using System;
using i18u.Authorizr.Web.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace i18u.Authorizr.Web.Pipelines.Registration
{
    /// <summary>
    /// Send a verification email to the newly registered <see cref="Account"/>
    /// entity's email address to verify the email is valid.
    /// </summary>
    public class SendVerificationEmailStep : PipelineStep<Account, RegistrationResult>
    {
        /// <inheritdoc />
        public override string Name => nameof(SendVerificationEmailStep);

        /// <inheritdoc />
        public override RegistrationResult Execute(Account input, PipelineContext ctx)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return default;
            }

            Console.WriteLine(apiKey);

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("hi@imaaronnicetomeetyou.me", "Cookbook");
            var subject = "Thanks for Registering!";
            var to = new EmailAddress(input.EmailAddress);
            var plainTextContent = "Thank you for joining Cookbook! Please visit this link in your web browser to complete your registration:";
            var htmlContent = @"<h1>Thank you for joining Cookbook!</h1>
                <a href=""#"">Click here</a> to complete your registration!";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg).GetAwaiter().GetResult();

            Console.WriteLine(response.Body.ReadAsStringAsync().GetAwaiter().GetResult());
            var statusCode = (int)response.StatusCode;

            if (statusCode.ToString().StartsWith("2"))
            {
                Console.WriteLine($"Successfully sent verification email to {input.EmailAddress}");
                return new RegistrationResult(true, "Registration completed successfully.");
            }

            Console.WriteLine($"Unable to send verification email to {input.EmailAddress}");
            return new RegistrationResult(false, "Registration may have completed successfully, but an email was unable to be sent.");
        }
    }
}