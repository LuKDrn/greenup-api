using Abp.Dependency;
using GreenUp.Application.Users.Dtos;

namespace GreenUp.Application.Users
{
    public interface IUserAppService : ITransientDependency
    {
        void ConfirmAccount(GetAllUsersInput input);

        void ContactGreenUp(GetAllUsersInput input);
    }
}
