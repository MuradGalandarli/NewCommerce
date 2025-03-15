using Microsoft.Extensions.Configuration;
using NewCommerce.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace NewCommerce.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;
public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

       public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml)
        {
            await SendMessageAsync(new[] {to },subject,body,isBodyHtml);
        }

        public async Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml)
        {
            
                MailMessage mail = new();
                mail.IsBodyHtml = isBodyHtml;
                foreach (string to in tos)
                    mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.From = new(_configuration["Mail:UserName"], "NewCommerce mail", System.Text.Encoding.UTF8);
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Host = _configuration["Mail:Host"];
                await smtpClient.SendMailAsync(mail);
           
        }
    }
}
