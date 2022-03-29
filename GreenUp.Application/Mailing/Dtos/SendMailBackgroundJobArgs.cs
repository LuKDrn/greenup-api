namespace GreenUp.Application.Mailing.Dtos
{
    public class SendMailBackgroundJobArgs
    {
        /// <summary>
        /// Obtient ou définit l'id de l'entité
        /// à laquelle nous souhaitons envoyer un mail.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Obtient ou définit l'email de l'entité
        /// à laquelle nous souhaitons envoyer un mail.
        /// </summary>
        public string Email { get; set; }
    }
}
