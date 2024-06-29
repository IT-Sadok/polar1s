namespace eShop.Persistence.Filters
{
    public abstract record FilterBase
    {
       public IReadOnlyCollection<string>? Ids { get; }
    }
}
