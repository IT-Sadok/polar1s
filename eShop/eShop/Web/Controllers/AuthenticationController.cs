using eShop.Application.Abstractions;
using eShop.Application.DTOs.Login;
using eShop.Application.DTOs.Register;
using eShop.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserAuthenticationService _userAuthenticationService;

        public AuthenticationController(IUserAuthenticationService userAuthenticationService)
            => _userAuthenticationService = userAuthenticationService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDTO)
        {
            var result = await _userAuthenticationService.RegisterAsync(registerUserDTO);

            if (result.Succeeded) return Ok();

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            var result = await _userAuthenticationService.LoginAsync(loginUserDTO);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized("Login failed");
            }

            return Ok(result);
        }
    }
}
