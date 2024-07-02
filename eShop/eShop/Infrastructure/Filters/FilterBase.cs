namespace eShop.Infrastructure.Filters
{
    public abstract record FilterBase
    {
        public IReadOnlyCollection<int>? Ids { get; }
    }
}
