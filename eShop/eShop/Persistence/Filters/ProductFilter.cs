namespace eShop.Persistence.Filters
{
    public record ProductFilter
    {
        public IReadOnlyCollection<int>? Ids { get; private set; }
    }
}
