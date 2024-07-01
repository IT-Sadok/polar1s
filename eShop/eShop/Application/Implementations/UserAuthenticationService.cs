using eShop.Application.Abstractions;
using eShop.Application.DTOs.Register;
using eShop.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using eShop.Domain.Constants;
using AutoMapper;
using eShop.Application.Abstractions.Wrappers;

namespace eShop.Application.Implementations
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserManagerWrapper<ApplicationUser> _userManagerWrapper;
        private readonly IUserRoleValidationService _userRoleValidationService;
        private readonly IMapper _mapper;

        public UserAuthenticationService(IUserManagerWrapper<ApplicationUser> userManagerWrapper, IUserRoleValidationService userRoleValidationService, IMapper mapper)
        {
            _userManagerWrapper = userManagerWrapper;
            _userRoleValidationService = userRoleValidationService;
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

            var result = await _userManagerWrapper.CreateAsync(user, registerUserDTO.Password);

            

            if (!result.Succeeded) return result;

            await _userManagerWrapper.AddToRoleAsync(user, registerUserDTO.Role);

            return result;
        }
    }
}
