namespace eShop.Application.DTOs.Admin
{
    public record GetUsersDTO(
        string UserId,
        string UserName,
        string Email,
        string PhoneNumber,
        IList<string> Roles);
}
