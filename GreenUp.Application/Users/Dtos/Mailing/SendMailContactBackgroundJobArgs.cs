using GreenUp.Application.Mailing.Dtos;

namespace GreenUp.Application.Users.Dtos.Mailing
{
    public class SendMailContactBackgroundJobArgs : SendMailBackgroundJobArgs
    {
        public string Message { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
    }
}
