using eShop.Application.Abstractions;
using eShop.Application.DTOs.Register;
using eShop.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using eShop.Domain.Constants;
using AutoMapper;
using eShop.Application.Abstractions.Wrappers;
using eShop.Application.DTOs.Login;

namespace eShop.Application.Implementations
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserManagerWrapper<ApplicationUser> _userManager;
        private readonly IUserRoleValidationService _userRoleValidator;
        private readonly ITokenGenerateService _tokenGenerator;
        private readonly IMapper _mapper;

        public UserAuthenticationService(IUserManagerWrapper<ApplicationUser> userManagerWrapper, IUserRoleValidationService userRoleValidationService, 
            ITokenGenerateService tokenGenerateService, IMapper mapper)
        {
            _userManager = userManagerWrapper;
            _userRoleValidator = userRoleValidationService;
            _tokenGenerator = tokenGenerateService;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterUserDTO registerUserDTO)
        {
            if (!_userRoleValidator.IsRoleAllowed(registerUserDTO.Role))
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

        public async Task<LoginResponseDTO?> LoginAsync(LoginUserDTO loginUserDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDTO.Email);

            if (user == null) return null;

            var passwordCheck = await _userManager.CheckPasswordAsync(user!, loginUserDTO.Password);

            if(!passwordCheck) return null;

            var userRoles = await _userManager.GetRolesAsync(user!);

            var token = await _tokenGenerator.GenerateJWTToken(user!, userRoles.ToList());

            var response = new LoginResponseDTO(user.UserName!, user.Email!, token, userRoles);

            return response;
        }
    }
}
