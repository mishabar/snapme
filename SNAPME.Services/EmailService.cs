using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SNAPME.Services.Interfaces;

namespace SNAPME.Services
{
    public class EmailService : IEmailService
    {
        public void Send(string to, string subject, string body)
        {
            var client = new SmtpClient();
            var message = new MailMessage { IsBodyHtml = true, Subject = subject, Body = body };

            if (!string.IsNullOrEmpty(to))
            {
                message.To.Add(new MailAddress(to));
            }

            client.Send(message);
        }
    }
}
