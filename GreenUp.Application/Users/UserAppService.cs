using GreenUp.Application.Users.Dtos;
using GreenUp.Application.Users.Dtos.Mailing;
using GreenUp.Application.Users.Mailing;
using GreenUp.EntityFrameworkCore.Data;
using Hangfire;
using System.Threading;

namespace GreenUp.Application.Users
{
    public class UserAppService : IUserAppService
    {
        protected readonly GreenUpContext _context;
        private readonly IBackgroundJobClient _backgroundJob;

        public UserAppService(GreenUpContext context, IBackgroundJobClient backgroundJob)
        {
            _context = context;
            _backgroundJob = backgroundJob;
        }

        public void ConfirmAccount(GetAllUsersInput input)
        {
            foreach (var user in input.Users)
            {
                SendMailConfirmAccountBackgroundJobArgs args = new()
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };

                _backgroundJob.Enqueue<SendMailConfirmAccountJob>((job) => job.Execute(args));

                //On fais une pause dans l'algorithme car le serveur de mail n'autorise pas plus de 3 connexions simultanées.
                Thread.Sleep(500);
            }
        }

        public void ContactGreenUp(GetAllUsersInput input)
        {
            foreach (var user in input.Users)
            {
                SendMailContactBackgroundJobArgs args = new()
                {
                    Email = user.Email,
                    UserName = user.FirstName,
                    UserPhone = user.PhoneNumber,
                    Message = input.Message
                };

                _backgroundJob.Enqueue<SendMailContactJob>((job) => job.Execute(args));

                //On fais une pause dans l'algorithme car le serveur de mail n'autorise pas plus de 3 connexions simultanées.
                Thread.Sleep(500);
            }
        }
    }
}
