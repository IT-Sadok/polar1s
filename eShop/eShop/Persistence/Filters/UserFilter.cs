namespace eShop.Persistence.Filters
{
    public record UserFilter
    {
        public IReadOnlyCollection<int>? Ids { get; private set; }
    }
}
