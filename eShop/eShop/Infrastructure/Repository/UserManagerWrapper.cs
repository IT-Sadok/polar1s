using eShop.Application.Abstractions.Wrappers;
using Microsoft.AspNetCore.Identity;

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
    }
}
