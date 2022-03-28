using GreenUp.Application.Mailing.Dtos;

namespace GreenUp.Application.Users.Dtos.Mailing
{
    public class SendMailConfirmAccountBackgroundJobArgs : SendMailBackgroundJobArgs
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
