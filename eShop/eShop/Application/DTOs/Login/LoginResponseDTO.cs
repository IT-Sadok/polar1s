namespace eShop.Application.DTOs.Login
{
    public record LoginResponseDTO(
        string UserName,
        string Email,
        string Token,
        IList<string> Roles
        );
}
