using eShop.Persistence.Models;

namespace eShop.Application.Abstractions
{
    public interface ITokenGenerateService
    {
        Task<string> GenerateJWTToken(ApplicationUser user, List<string> userRoles);
    }
}
