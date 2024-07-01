using eShop.Domain.Constants;

namespace eShop.Application.Abstractions
{
    public interface IUserRoleValidationService
    {
        bool IsRoleAllowed(string role) => UserRolesConstants.IsRoleAllowed(role);
    }
}
