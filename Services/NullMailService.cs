using Microsoft.Extensions.Logging;

namespace DutchTreat.Services
{
    public class NullMailService : IMailService
    {
        //field
        private readonly ILogger<NullMailService> _logger;

        // Inject a Logger into the class that we can use to log information
        public NullMailService(ILogger<NullMailService> logger)
        {
            _logger = logger;
        }

        public void SendMessage(string to, string subject, string body)
        {
            // Log the message
            _logger.LogInformation($"To: {to} Subject: {subject} Body: {body}");
        }
    }
}