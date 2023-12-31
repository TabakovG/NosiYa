﻿namespace NosiYa.Services.Messaging
{
    public class NullMessageSender : IEmailSender
    {
        public Task SendEmailAsync(
            string to,
            string subject,
            string htmlContent,
            IEnumerable<EmailAttachment>? attachments = null)
        {
            return Task.CompletedTask;
        }
    }
}
