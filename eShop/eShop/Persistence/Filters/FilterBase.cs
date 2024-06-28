namespace eShop.Persistence.Filters
{
    public abstract record FilterBase
    {
       public IReadOnlyCollection<int>? Ids { get; private set; }
    }
}
