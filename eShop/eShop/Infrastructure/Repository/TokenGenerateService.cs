using eShop.Application.Abstractions;
using eShop.Persistence.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using eShop.Infrastructure.Configuration;

namespace eShop.Infrastructure.Repository
{
    public class TokenGenerateService : ITokenGenerateService
    {
        private readonly JwtOptions _jwtOptions;

        public TokenGenerateService(IOptions<JwtOptions> jwtOptions)
            => _jwtOptions = jwtOptions.Value;

        public Task<string> GenerateJWTToken(ApplicationUser user, List<string> userRoles)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: credentials
                );

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
