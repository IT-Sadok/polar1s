namespace Catalog.Service.Models.Reports;

public record ProductWithMultipleSuppliersResponse(Guid Id, string Sku, string Name, int SupplierCount);
