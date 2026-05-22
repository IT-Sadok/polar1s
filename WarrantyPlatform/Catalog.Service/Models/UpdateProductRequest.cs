using Catalog.Service.Domain.Const;

namespace Catalog.Service.Models;

public record UpdateProductRequest(string Name, ProductCategory Category);
