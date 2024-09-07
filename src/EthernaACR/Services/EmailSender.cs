// Copyright 2021-present Etherna SA
// This file is part of Etherna ACR.
// 
// Etherna ACR is free software: you can redistribute it and/or modify it under the terms of the
// GNU Lesser General Public License as published by the Free Software Foundation,
// either version 3 of the License, or (at your option) any later version.
// 
// Etherna ACR is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY;
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License along with Etherna ACR.
// If not, see <https://www.gnu.org/licenses/>.

using Etherna.ACR.Helpers;
using Etherna.ACR.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Etherna.ACR.Services
{
    public class EmailSender : IEmailSender
    {
        // Fields.
        private readonly EmailSettings settings;

        // Constructor.
        public EmailSender(IOptions<EmailSettings> opts)
        {
            ArgumentNullException.ThrowIfNull(opts, nameof(opts));

            settings = opts.Value;
        }

        // Methods.
        public Task SendEmailAsync(string email, string subject, string message)
        {
            if (!EmailHelper.IsValidEmail(email))
                throw new ArgumentException("Email is not valid", nameof(email));

            return settings.CurrentService switch
            {
                EmailSettings.EmailService.Mailtrap => MailtrapSendEmailAsync(email, subject, message),
                EmailSettings.EmailService.Sendgrid => SendgridSendEmailAsync(email, subject, message),
                EmailSettings.EmailService.FakeSender => Task.CompletedTask,
                _ => throw new InvalidOperationException()
            };
        }

        // Helpers.
        private async Task MailtrapSendEmailAsync(string email, string subject, string message)
        {
            using var client = new SmtpClient
            {
                Host = "smtp.mailtrap.io",
                Port = 2525,
                Credentials = new NetworkCredential(settings.ServiceUser, settings.ServiceKey),
                EnableSsl = true,
            };
            using var mail = new MailMessage(
                new MailAddress(settings.SendingAddress, settings.DisplayName),
                new MailAddress(email))
            {
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            await client.SendMailAsync(mail);
        }

        private async Task<Response> SendgridSendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(settings.ServiceKey);

            var from = new EmailAddress(settings.SendingAddress, settings.DisplayName);
            var to = new EmailAddress(email);

            var plainTextContent = message;
            var htmlContent = message;

            var mail = MailHelper.CreateSingleEmail(
                from,
                to,
                subject,
                plainTextContent,
                htmlContent);

            return await client.SendEmailAsync(mail);
        }
    }
}
