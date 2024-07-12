using AutoMapper;
using eShop.Application.Abstractions;
using eShop.Application.Abstractions.Wrappers;
using eShop.Application.DTOs.Login;
using eShop.Application.DTOs.Register;
using eShop.Application.Implementations;
using eShop.Domain.Constants;
using eShop.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace eShop.Tests
{
    public class UserAuthenticationTest
    {
        private readonly Mock<IUserManagerWrapper<ApplicationUser>> _userManagerMock;
        private readonly Mock<IUserRoleValidationService> _userRoleValidationServiceMock;
        private readonly Mock<ITokenGenerateService> _tokenGenerateServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserAuthenticationService _userAuthenticationServiceMock;

        public UserAuthenticationTest()
        {
            _userManagerMock = new Mock<IUserManagerWrapper<ApplicationUser>>();
            _userRoleValidationServiceMock = new Mock<IUserRoleValidationService>();
            _tokenGenerateServiceMock = new Mock<ITokenGenerateService>();
            _mapperMock = new Mock<IMapper>();

            _userAuthenticationServiceMock = new UserAuthenticationService(
                _userManagerMock.Object,
                _userRoleValidationServiceMock.Object,
                _tokenGenerateServiceMock.Object,
                _mapperMock.Object);
        }

        [Fact]
        public async Task RegisterAsync_ReturnsFailedResult_WhenRoleIsInvalid()
        {
            // Arrange
            var registerUserDTO = new RegisterUserDTO("Test", "test@test.com", "Password123", "InvalidRole");

            _userRoleValidationServiceMock.Setup(v => v.IsRoleAllowed(registerUserDTO.Role)).Returns(false);

            // Act
            var result = await _userAuthenticationServiceMock.RegisterAsync(registerUserDTO);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains(result.Errors, e => e.Description.Contains("Invalid role"));
        }

        [Fact]
        public async Task RegisterAsync_ReturnsSucceededResult_WhenUserIsCreatedSuccessfully()
        {
            // Arrange
            var registerUserDTO = new RegisterUserDTO("Test", "test@test.com", "Password123", UserRolesConstants.User);
            var user = new ApplicationUser();

            _userRoleValidationServiceMock.Setup(v => v.IsRoleAllowed(registerUserDTO.Role)).Returns(true);
            _mapperMock.Setup(m => m.Map<ApplicationUser>(registerUserDTO)).Returns(user);
            _userManagerMock.Setup(um => um.CreateAsync(user, registerUserDTO.Password)).ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(um => um.AddToRoleAsync(user, registerUserDTO.Role)).Returns(Task.FromResult(IdentityResult.Success));

            // Act
            var result = await _userAuthenticationServiceMock.RegisterAsync(registerUserDTO);

            // Assert
            Assert.True(result.Succeeded);
        }

        [Fact]
        public async Task LoginAsync_ReturnsNull_WhenUserNotFound()
        {
            // Arrange
            var loginUserDTO = new LoginUserDTO ("test@example.com", "Password123");
            _userManagerMock.Setup(um => um.FindByEmailAsync(loginUserDTO.Email)).ReturnsAsync((ApplicationUser?)null);

            // Act
            var result = await _userAuthenticationServiceMock.LoginAsync(loginUserDTO);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task LoginAsync_ReturnsNull_WhenPasswordIsIncorrect()
        {
            // Arrange
            var loginUserDTO = new LoginUserDTO ("test@example.com", "Password123");
            var user = new ApplicationUser();

            _userManagerMock.Setup(um => um.FindByEmailAsync(loginUserDTO.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, loginUserDTO.Password)).ReturnsAsync(false);

            // Act
            var result = await _userAuthenticationServiceMock.LoginAsync(loginUserDTO);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task LoginAsync_ReturnsLoginResponseDTO_WhenCredentialsAreValid()
        {
            // Arrange
            var loginUserDTO = new LoginUserDTO ("test@example.com", "Password123");
            var user = new ApplicationUser { UserName = "testuser", Email = "test@example.com" };
            var roles = new List<string> { UserRolesConstants.User };
            var token = "generated-jwt-token";

            _userManagerMock.Setup(um => um.FindByEmailAsync(loginUserDTO.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.CheckPasswordAsync(user, loginUserDTO.Password)).ReturnsAsync(true);
            _userManagerMock.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(roles);
            _tokenGenerateServiceMock.Setup(tg => tg.GenerateJWTToken(user, roles)).ReturnsAsync(token);

            // Act
            var result = await _userAuthenticationServiceMock.LoginAsync(loginUserDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal(token, result.Token);
            Assert.Equal(roles, result.Roles);
        }
    }
}