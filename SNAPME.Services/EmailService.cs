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
            Send(to, subject, body, null);
        }


        public void Send(string to, string subject, string body, string[] inlineAttachments)
        {
            var client = new SmtpClient();
            var message = new MailMessage { IsBodyHtml = true, Subject = subject };

            if (!string.IsNullOrEmpty(to))
            {
                message.To.Add(new MailAddress(to));
            }

            // add inline attachments if needed
            if (inlineAttachments != null && inlineAttachments.Length > 0)
            {
                string[] cids = new string[inlineAttachments.Length];
                List<LinkedResource> resources = new List<LinkedResource>();

                for (int i = 0; i < inlineAttachments.Length; i++)
                {
                    string ContentId = Guid.NewGuid().ToString();
                    string attachmentPath = System.Web.HttpContext.Current.Server.MapPath(inlineAttachments[i]);
                    Attachment inlineAttachment = new Attachment(attachmentPath);
                    inlineAttachment.ContentDisposition.Inline = true;
                    inlineAttachment.ContentDisposition.DispositionType = System.Net.Mime.DispositionTypeNames.Inline;
                    inlineAttachment.ContentId = ContentId;
                    inlineAttachment.ContentType.MediaType = "image/png";
                    inlineAttachment.ContentType.Name = ContentId;
                    message.Attachments.Add(inlineAttachment);
                    cids[i] = ContentId;
                }

                // update the cids
                message.Body = string.Format(body, cids);
            }
            else
            {
                message.Body = body;
            }

            client.Send(message);
        }
    }
}
