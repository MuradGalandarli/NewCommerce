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

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
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

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {

            StringBuilder mail = new();
            mail.AppendLine("Salam<br>Əgər yeni şifrə tələbi göndərmisinizsə, aşağıdakı linkdən şifrənizi yeniləyə bilərsiniz.<br><strong><a target=\"_blank\" href=\"");
            mail.AppendLine(_configuration["UIUrl"]);
            mail.AppendLine("/update-password/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">Yeni şifrə tələbi üçün klikləyin...</a></strong><br><br><span style=\"font-size:12px;\">QEYD: Əgər bu tələb sizin tərəfinizdən göndərilməyibsə, zəhmət olmasa bu e-məktubu nəzərə almayın.</span><br>Hörmətlə...<br><br><br>NewCommerce");

            await SendMailAsync(to, "Şifrə Yeniləmə Tələbi", mail.ToString());

        }

    }
}
