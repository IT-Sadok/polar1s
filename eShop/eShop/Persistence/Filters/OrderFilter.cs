namespace eShop.Persistence.Filters
{
    public record OrderFilter
    {
        public IReadOnlyCollection<int>? Ids { get; private set; }
    }
}
