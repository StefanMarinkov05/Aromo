using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace Aromo
{    public class ValidationSim : IEmailSender
    {
        // Simulating email sending for validation purposes
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }

}
