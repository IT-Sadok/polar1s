namespace eShop.Persistence.Filters
{
    public record UserFilter
    {
        public IReadOnlyCollection<string>? Ids { get; }
    }
}
