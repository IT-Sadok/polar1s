namespace eShop.Persistence.Filters
{
    public record CartFilter
    {
        public IReadOnlyCollection<int>? Ids { get; private set; }
    }
}