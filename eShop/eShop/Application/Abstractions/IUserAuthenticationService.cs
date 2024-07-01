using eShop.Application.DTOs.Register;
using Microsoft.AspNetCore.Identity;

namespace eShop.Application.Abstractions
{
    public interface IUserAuthenticationService
    {
        Task<IdentityResult> RegisterAsync(RegisterUserDTO registerUserDTO);
    }
}
