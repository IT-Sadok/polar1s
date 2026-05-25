namespace Catalog.Service.Models.Reports;

public record TopBrandsRequest
{
    public int Limit { get; set; } = 10;
}
