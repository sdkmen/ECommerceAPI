using ECommerceAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services
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
            foreach (var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_configuration["Mail:Username"], _configuration["Mail:DisplayName"], System.Text.Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = Convert.ToInt16(_configuration["Mail:Port"]);
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine("Hello<br>You can continue the password update process by clicking on the link below.<br><strong>");
            mail.Append("<a target=\"_blank\" href=\"");
            mail.Append(_configuration["AngularClientUrl"]);
            mail.Append("/update-password/");
            mail.Append(userId);
            mail.Append("/");
            mail.Append(resetToken);
            mail.Append("\">Click for redirect to update password page</a></strong><br><br><br>");
            mail.Append("<span style=\"font-size:12px;\">If you have not requested to update your password, ignore this email.</span><br><br>");
            mail.Append("Mini E-Commerce App");

            await SendMailAsync(to, "Update Password", mail.ToString());
        }
        public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime date, string userNameSurname)
        {
            string mail = $"Hello {userNameSurname},<br><br>" +
                          $"<span style=\"font-weight:bold; color: black;\">{orderCode}</span> coded order was completed on {date} and delivered to the cargo company for shipment." +
                          $"<br><br>Thanks for choosing us..." +
                          $"<br><br><span style=\"font-weight:bold; color: black;\">Mini E-Commerce App</span>";

            await SendMailAsync(to, $"New update about order with {orderCode} code!", mail);
        }
    }
}
