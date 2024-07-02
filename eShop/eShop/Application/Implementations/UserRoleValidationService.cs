using eShop.Application.Abstractions;
using eShop.Domain.Constants;

namespace eShop.Application.Implementations
{
    public class UserRoleValidationService : IUserRoleValidationService
    {
        public bool IsRoleAllowed(string role) => UserRolesConstants.AllowedRoles.Contains(role);
    }
}
