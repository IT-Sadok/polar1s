namespace eShop.Domain.Constants
{
    public static class UserRolesConstants
    {
        public const string Admin = nameof(Admin);
        public const string User = nameof(User);

        public static readonly string[] AllowedRoles = new[] { Admin, User };
    }
}
