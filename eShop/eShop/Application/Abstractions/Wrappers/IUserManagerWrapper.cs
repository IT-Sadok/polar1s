using Microsoft.AspNetCore.Identity;

namespace eShop.Application.Abstractions.Wrappers
{
    public interface IUserManagerWrapper<TUser> where TUser : class
    {
        Task<IdentityResult> CreateAsync(TUser user, string password);
        Task<IList<string>> GetRolesAsync(TUser user);
        Task<IdentityResult> AddToRoleAsync(TUser user, string role);
        Task<TUser?> FindByIdAsync(string id);
        Task<TUser?> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(TUser user, string password);
        Task<IdentityResult> DeleteAsync(TUser user);
        Task<IdentityResult> RemoveFromRolesAsync(TUser user, IEnumerable<string> role);
        Task<IEnumerable<TUser>> GetUsersAsync();
    }
}
