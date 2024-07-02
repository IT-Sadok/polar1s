namespace eShop.Infrastructure.Filters
{
    public record UserFilter
    {
        public IReadOnlyCollection<string>? Ids { get; }
    }
}
