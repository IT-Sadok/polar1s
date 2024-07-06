namespace eShop.Infrastructure.Configuration
{
    public class JwtOptions
    {
        public const string Jwt = nameof(Jwt);
        public string Key { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience {  get; set; } = null!;
    }
}
