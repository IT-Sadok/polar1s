using eShop.Application.Abstractions;
using eShop.Application.DTOs.Register;
using eShop.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using eShop.Domain.Constants;
using AutoMapper;

namespace eShop.Application.Implementations
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserRoleValidationService _userRoleValidationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserAuthenticationService(IUserRoleValidationService userRoleValidationService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userRoleValidationService = userRoleValidationService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterUserDTO registerUserDTO)
        {
            if (!_userRoleValidationService.IsRoleAllowed(registerUserDTO.Role))
            {
                string description = $"Invalid role. Allowed roles are: {string.Join(' ', UserRolesConstants.AllowedRoles)}";

                return IdentityResult.Failed(new IdentityError { Description = description });
            }

            var user = _mapper.Map<ApplicationUser>(registerUserDTO);

            var result = await _userManager.CreateAsync(user, registerUserDTO.Password);

            if (!result.Succeeded) return result;

            await _userManager.AddToRoleAsync(user, registerUserDTO.Role);

            return result;
        }
    }
}
