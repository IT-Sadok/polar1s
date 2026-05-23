using Catalog.Service.Entities.Const;

namespace Catalog.Service.Models;

public record UpdateProductRequest(string Name, ProductCategory Category);
