namespace Catalog.Service.Common.Configuration;

public sealed class CacheOptions
{
    public const string SectionName = "Cache";

    public string ConnectionString { get; set; } = string.Empty;
    public string InstanceName { get; set; } = string.Empty;
    public TimeSpan AbsoluteExpiration { get; set; } = TimeSpan.FromMinutes(60);
}
