namespace eShop.Persistence.Filters
{
    public record CartItemFilter
    {
        public IReadOnlyCollection<int>? Ids { get; private set; }
    }
}
