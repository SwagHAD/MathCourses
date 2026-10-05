using SharedKernel.Enums;

namespace SharedKernel.Interfaces
{
    public interface IUserProvider
    {
        int GetUserId();
        RoleType GetRoleType();
    }
}
