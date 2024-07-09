using eShop.Application.Abstractions.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eShop.Infrastructure.Repository
{
    public class UserManagerWrapper<TUser> : IUserManagerWrapper<TUser> where TUser : class
    {
        private readonly UserManager<TUser> _userManager;

        public UserManagerWrapper(UserManager<TUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(TUser user, string role)
            => await _userManager.AddToRoleAsync(user, role);

        public async Task<IdentityResult> CreateAsync(TUser user, string password)
            => await _userManager.CreateAsync(user, password);

        public async Task<TUser?> FindByIdAsync(string id)
            => await _userManager.FindByIdAsync(id);

        public async Task<IList<string>> GetRolesAsync(TUser user)
            => await _userManager.GetRolesAsync(user);

        public async Task<TUser?> FindByEmailAsync(string email)
            => await _userManager.FindByEmailAsync(email);

        public async Task<bool> CheckPasswordAsync(TUser user, string password)
            => await _userManager.CheckPasswordAsync(user, password);

        public async Task<IdentityResult> DeleteAsync(TUser user)
            => await _userManager.DeleteAsync(user);

        public async Task<IdentityResult> RemoveFromRolesAsync(TUser user, IEnumerable<string> roles)
            => await _userManager.RemoveFromRolesAsync(user, roles);

        public async Task<IEnumerable<TUser>> GetUsersAsync()
            => await _userManager.Users.ToListAsync();
    }
}
