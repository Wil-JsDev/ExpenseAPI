using ExpenseAPI.Application.DTOs.Email;
using ExpenseAPI.Application.Interfaces.Service;
using ExpenseAPI.Domain.Settings;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Org.BouncyCastle.Asn1.Ocsp;
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
        private MailSettings _mailSettings { get; }

        public EmailService(IOptions<MailSettings> options)
        {
            _mailSettings = options.Value;
        }

        public async Task Execute(EmailRequestDto request)
        {
            MimeMessage email = new();
            email.Sender = MailboxAddress.Parse(_mailSettings.EmailFrom);
            email.To.Add(MailboxAddress.Parse(request.To)); //Esto es para a quien le quiero enviar ese correo
            email.Subject = request.Subject;
            BodyBuilder builder = new();
            builder.HtmlBody = request.Body;
            email.Body = builder.ToMessageBody();

            //Configuracion del SMTP
            using MailKit.Net.Smtp.SmtpClient smtp = new();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
