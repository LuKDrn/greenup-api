using GreenUp.Application.Users.Dtos.Mailing;
using System.Collections.Generic;

namespace GreenUp.Application.Users.Dtos
{
    public class GetAllUsersInput
    {
        public int MissionId { get; set; }
        public ICollection<UserDataForMail> Users { get; set; } = new List<UserDataForMail>();
    }
}
