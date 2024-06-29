namespace eShop.Domain.Constants
{
    public static class UserRolesConstants
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public static readonly string[] AllowedRoles = new[] { "Admin", "User" };

        public static bool IsRoleAllowed(string role) => AllowedRoles.Contains(role);
    }
}
