using eShop.Application.Abstractions.Wrappers;
using eShop.Application.DTOs.Admin;
using eShop.Persistence.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserManagerWrapper<ApplicationUser> _userManager;

        public AdminController(IUserManagerWrapper<ApplicationUser> userManager)
        {
            _userManager = userManager;

        }

        [HttpDelete("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "User was deleted successfully" });
        }

        [HttpPost("change-role")]
        public async Task<IActionResult> ChangeUserRole([FromBody] ChangeUserRoleDTO changeUserRoleDTO)
        {
            var user = await _userManager.FindByIdAsync(changeUserRoleDTO.UserId);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!removeRolesResult.Succeeded)
            {
                return BadRequest(removeRolesResult.Errors);
            }

            var addRoleResult = await _userManager.AddToRoleAsync(user, changeUserRoleDTO.Role);
            if (!addRoleResult.Succeeded)
            {
                return BadRequest(addRoleResult.Errors);
            }

            return Ok(new { Message = "User role updated successfully" });
        }

        [HttpGet("get-users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.GetUsersAsync();

            var usersDTO = users.Select(user => new GetUsersDTO(
                UserId: user.Id,
                UserName: user.UserName!,
                Email: user.Email!,
                PhoneNumber: user.PhoneNumber!,
                Roles: _userManager.GetRolesAsync(user).Result
                ));

            return Ok(usersDTO);
        }
    }
}
