using StreamingService.Domain.Enums;

namespace StreamingService.Application.Common.Interfaces
{
    public interface IUserProvider
    {
        int GetUserId();
        UserType GetUserType();
    }
}
