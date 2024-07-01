namespace eShop.Application.DTOs.Register
{
    public record RegisterUserDTO(
        string UserName,
        string Email,
        string Password,
        string Role);
}
