namespace Catalog.Service.Models.Reports;

public record CheapestSupplierResponse(Guid ProductId, string ProductName, string SupplierName, decimal CheapestPrice);
