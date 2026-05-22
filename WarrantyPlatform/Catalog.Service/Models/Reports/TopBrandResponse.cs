namespace Catalog.Service.Models.Reports;

public record TopBrandResponse(Guid Id, string Name, int ProductCount, decimal AverageUnitCost);
