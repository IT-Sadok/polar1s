namespace Warranty.Service.Common.Configuration;

public sealed class GeneralDatabaseOptions
{
    public const string SectionName = "GeneralDb";

    public string ConnectionString { get; set; } = string.Empty;
}
