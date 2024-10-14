using ExpenseAPI.Application.DTOs.Email;
using ExpenseAPI.Application.Interfaces.Service;
using ExpenseAPI.Domain.Settings;
using Microsoft.Extensions.Options;
using Resend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseAPI.Infraestructure.Shared.Service
{
    public class EmailService : IEmailService
    {
        private readonly IResend _resend;
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> options,IResend resend)
        {
            _resend = resend;
            _mailSettings = options.Value;
        }

        public async Task Execute(EmailRequestDto emailRequest)
        {
            var message = new EmailMessage();
            message.From = _mailSettings.EmailFrom;
            message.To.Add(emailRequest.To);
            message.Subject = emailRequest.Subject;
            message.HtmlBody = emailRequest.Body;

            await _resend.EmailSendAsync(message);
        }
    }
}
