using Domain.Enums;

namespace Application.Interfaces
{
    public interface IUserProvider
    {
        int GetUserId();
        UserType GetUserType();
    }
}
