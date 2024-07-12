using eShop.Application.Abstractions.Wrappers;
using eShop.Application.DTOs.Admin;
using eShop.Persistence.Models;
using eShop.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

public class AdminControllerTests
{
    private readonly Mock<IUserManagerWrapper<ApplicationUser>> _userManagerMock;
    private readonly AdminController _controller;

    public AdminControllerTests()
    {
        _userManagerMock = new Mock<IUserManagerWrapper<ApplicationUser>>();

        _controller = new AdminController(_userManagerMock.Object);
    }

    [Fact]
    public async Task DeleteUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser?)null);

        // Act
        var result = await _controller.DeleteUser("fake-id");

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteUser_ReturnsOk_WhenUserDeletedSuccessfully()
    {
        // Arrange
        var user = new ApplicationUser();
        _userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManagerMock.Setup(um => um.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _controller.DeleteUser("real-Id");

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task ChangeUserRole_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser?)null);

        // Act
        var result = await _controller.ChangeUserRole("fake-Id", new ChangeUserRoleDTO("Admin"));

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task ChangeUserRole_ReturnsOk_WhenUserRoleUpdatedSuccessfully()
    {
        // Arrange
        var user = new ApplicationUser();
        var newRole = "Admin";
        _userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManagerMock.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(new List<string> { "User" });
        _userManagerMock.Setup(um => um.RemoveFromRolesAsync(user, It.IsAny<IEnumerable<string>>())).ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(um => um.AddToRoleAsync(user, newRole)).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _controller.ChangeUserRole("real-Id", new ChangeUserRoleDTO (newRole));

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetUsers_ReturnsUsers()
    {
        // Arrange
        var users = new List<ApplicationUser>
        {
            new ApplicationUser { Id = "1", UserName = "user1", Email = "user1@example.com", PhoneNumber = "1234567890" },
            new ApplicationUser { Id = "2", UserName = "user2", Email = "user2@example.com", PhoneNumber = "0987654321" }
        };

        _userManagerMock.Setup(um => um.GetUsersAsync()).ReturnsAsync(users);
        _userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(new List<string> { "User" });

        // Act
        var result = await _controller.GetUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var usersDTO = Assert.IsAssignableFrom<IEnumerable<GetUsersDTO>>(okResult.Value);
        Assert.Equal(2, usersDTO.Count());
    }
}