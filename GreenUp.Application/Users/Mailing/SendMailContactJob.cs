using Abp.IO.Extensions;
using Abp.Reflection.Extensions;
using Abp.Threading;
using GreenUp.Application.Mailing;
using GreenUp.Application.Users.Dtos.Mailing;
using GreenUp.Core;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Text;
using System.Threading.Tasks;

namespace GreenUp.Application.Users.Mailing
{
    public class SendMailContactJob : SendMailBackgroundJob<SendMailContactBackgroundJobArgs>
    {
        public override void Execute(SendMailContactBackgroundJobArgs args)
        {
            // Pré-traitement habituel pour tout job d'envoi de mail en masse.
            base.Execute(args);

            AsyncHelper.RunSync(() => Send(args));
        }

        protected static async Task ReplaceBodyAndSend(StringBuilder emailTemplate, StringBuilder mailMessage)
        {
            emailTemplate.Replace("{EMAIL_MESSAGE}", mailMessage.ToString());
            BodyBuilder body = new()
            {
                TextBody = mailMessage.ToString(),
                HtmlBody = emailTemplate.ToString()
            };
            var mail = new MimeMessage()
            {
                Subject = "Contact",
                Body = body.ToMessageBody(),
            };
            mail.From.Add(new MailboxAddress("GreenUp Web App", "greenUp.asso@gmail.com"));
            mail.To.Add(new MailboxAddress("GreenUp", "greenUp.asso@gmail.com"));

            var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("greenup.asso@gmail.com", "GreenUp_MDS");
            await client.SendAsync(mail);
            await client.DisconnectAsync(true);
        }

        /// <summary>    
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Send(SendMailContactBackgroundJobArgs args)
        {
            var emailTemplate = GetSender($"{args.UserName} {args.Email}", args.UserPhone);
            StringBuilder mailMessage = GetBody(args.Message);
            await ReplaceBodyAndSend(emailTemplate, mailMessage);
        }

        private static StringBuilder GetSender(string sender, string phone)
        {
            var emailTemplate = new StringBuilder(GetDefaultTemplate());
            emailTemplate.Replace("{EMAIL_SENDER}", sender);
            emailTemplate.Replace("{EMAIL_PHONE}", phone);

            return emailTemplate;
        }

        private StringBuilder GetBody(string message)
        {
            var mailMessage = new StringBuilder();
            mailMessage.AppendLine($"<p>{message}</p>");
            return mailMessage;
        }

        private static string GetDefaultTemplate()
        {
            using var stream = typeof(GreenUpConsts).GetAssembly().GetManifestResourceStream("GreenUp.Core.Net.Emailing.EmailTemplates.contact.html");
            var bytes = stream.GetAllBytes();
            var template = Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3);
            return template.Replace("{THIS_YEAR}", DateTime.Now.Year.ToString());
        }
    }
}
