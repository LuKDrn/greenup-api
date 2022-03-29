using Abp.IO.Extensions;
using Abp.Reflection.Extensions;
using Abp.Threading;
using GreenUp.Application.Mailing;
using GreenUp.Application.Users.Dtos.Mailing;
using GreenUp.Core;
using GreenUp.EntityFrameworkCore.Data;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Text;
using System.Threading.Tasks;

namespace GreenUp.Application.Users.Mailing
{
    public class SendMailConfirmAccountJob : SendMailBackgroundJob<SendMailConfirmAccountBackgroundJobArgs>
    {
        private readonly GreenUpContext _context;
        private readonly string _emailButtonStyle =
    "padding-left: 30px; padding-right: 30px; padding-top: 12px; padding-bottom: 12px; color: #ffffff; background-color: #00bb77; font-size: 14pt;border-radius: 1%; text-decoration: none;";
        private readonly string _emailButtonColor = "#00bb77";
        private readonly string _emailEnglishButtonStyle =
    "padding-left: 30px; padding-right: 30px; padding-top: 12px; padding-bottom: 12px; color: #ffffff; background-color: #0b5394; font-size: 12pt;border-radius: 1%; text-decoration: none;";
        private readonly string _emailEnglishButtonColor = "#0b5394";
        public SendMailConfirmAccountJob(GreenUpContext context)
        {
            _context = context;
        }

        public override void Execute(SendMailConfirmAccountBackgroundJobArgs args)
        {
            // Pré-traitement habituel pour tout job d'envoi de mail en masse.
            base.Execute(args);

            AsyncHelper.RunSync(() => Send(args));
        }

        protected static async Task ReplaceBodyAndSend(string emailAddress, string subject, StringBuilder emailTemplate, StringBuilder mailMessage, StringBuilder mailEnglishMessage)
        {
            emailTemplate.Replace("{EMAIL_BODY}", mailMessage.ToString());
            emailTemplate.Replace("{EMAIL_BODY_ENGLISH}", mailEnglishMessage.ToString());
            BodyBuilder body = new()
            {
                TextBody = mailMessage.ToString() + mailEnglishMessage.ToString(),
                HtmlBody = emailTemplate.ToString()
            };
            var mail = new MimeMessage()
            {
                Subject = subject,
                Body = body.ToMessageBody(),
            };
            mail.From.Add(new MailboxAddress("GreenUp", "lucas72.derouin@gmail.com"));
            mail.To.Add(new MailboxAddress(emailAddress, emailAddress));

            var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("lucas72.derouin@gmail.com", "2Edc-9890Dff");
            await client.SendAsync(mail);
            await client.DisconnectAsync(true);
        }

        /// <summary>
        /// Envoie d'un mail pour demander à 
        /// l'utilisateur de confirmer son compte      
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Send(SendMailConfirmAccountBackgroundJobArgs user)
        {
            var emailTemplate = GetTitleAndSubTitle($"Veuillez confirmez votre adresse mail.",
                $"Bonjour, {user.FirstName} {user.LastName} ! Pour utiliser pleinement l'application, veuillez confirmer votre adresse mail",
                $"Please confirm your e-mail",
                $"Hello, {user.FirstName} {user.LastName} ! To fully use the app, please confirm your e-mail");
            StringBuilder mailMessage = GetFrenchBody(user.UserId);
            StringBuilder mailMessageEnglish = GetEnglishBody(user.UserId);
            await ReplaceBodyAndSend(user.Email, $"Vérification du compte", emailTemplate, mailMessage, mailMessageEnglish);
        }

        private static StringBuilder GetTitleAndSubTitle(string title, string subTitle, string titleEnglish, string subTitleEnglish)
        {
            var emailTemplate = new StringBuilder(GetDefaultTemplate());
            emailTemplate.Replace("{EMAIL_TITLE}", title);
            emailTemplate.Replace("{EMAIL_SUB_TITLE}", subTitle);

            emailTemplate.Replace("{EMAIL_TITLE_ENGLISH}", titleEnglish);
            emailTemplate.Replace("{EMAIL_SUB_TITLE_ENGLISH}", subTitleEnglish);

            return emailTemplate;
        }

        private StringBuilder GetFrenchBody(string userId)
        {
            var mailMessage = new StringBuilder();

            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<a style=\"" + _emailButtonStyle + "\" bg-color=\"" + _emailButtonColor +
                                   "\" href=\"" + GreenUpConsts.ApplicationProductionUrl + "/api/Auth/UpdateConfirmMail/" + userId + "\">" + "Confirmer" + "</a>");
            mailMessage.AppendLine("<p>Cordialement L'équipe GreenUp</p>");
            return mailMessage;
        }
        private StringBuilder GetEnglishBody(string userId)
        {
            var mailMessage = new StringBuilder();

            mailMessage.AppendLine("<br />");
            mailMessage.AppendLine("<a style=\"" + _emailEnglishButtonStyle + "\" bg-color=\"" + _emailEnglishButtonColor +
                                   "\" href=\"" + GreenUpConsts.ApplicationProductionUrl + "/api/Auth/UpdateConfirmMail/" + userId + "\">" + "Confirm" + "</a>");
            mailMessage.AppendLine("<p>Sincerely GreenUp</p>");
            return mailMessage;
        }
        private static string GetDefaultTemplate()
        {
            using var stream = typeof(GreenUpConsts).GetAssembly().GetManifestResourceStream("GreenUp.Core.Net.Emailing.EmailTemplates.default.html");
            var bytes = stream.GetAllBytes();
            var template = Encoding.UTF8.GetString(bytes, 3, bytes.Length - 3);
            return template.Replace("{THIS_YEAR}", DateTime.Now.Year.ToString());
        }
    }
}
