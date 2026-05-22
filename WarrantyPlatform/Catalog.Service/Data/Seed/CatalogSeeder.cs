using Catalog.Service.Domain;
using Catalog.Service.Domain.Const;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Data.Seed;

public static class CatalogSeeder
{
    public static async Task SeedAsync(CatalogDbContext context, CancellationToken ct = default)
    {
        var apple = await EnsureBrandAsync(context, "Apple", "USA", ct);
        var samsung = await EnsureBrandAsync(context, "Samsung", "South Korea", ct);
        var sony = await EnsureBrandAsync(context, "Sony", "Japan", ct);
        var nike = await EnsureBrandAsync(context, "Nike", "USA", ct);
        var adidas = await EnsureBrandAsync(context, "Adidas", "Germany", ct);

        // Xiaomi is intentionally left without any products (for the unused-brands demo query).
        await EnsureBrandAsync(context, "Xiaomi", "China", ct);

        var globalTech = await EnsureSupplierAsync(context, "GlobalTech Distributors", "orders@globaltech.example", ct);
        var asiaSource = await EnsureSupplierAsync(context, "AsiaSource Imports", "sales@asiasource.example", ct);
        var euroSupply = await EnsureSupplierAsync(context, "EuroSupply Co", "info@eurosupply.example", ct);
        var quickShip = await EnsureSupplierAsync(context, "QuickShip Logistics", "contact@quickship.example", ct);

        await context.SaveChangesAsync(ct);

        if (await context.Products.AnyAsync(ct))
        {
            return;
        }

        var iphone = NewProduct("IPH-15-256-BLK", "iPhone 15 Pro 256GB", ProductCategory.Electronics, apple.Id);
        var ipad = NewProduct("IPD-PRO-128", "iPad Pro 128GB", ProductCategory.Electronics, apple.Id);
        var macbook = NewProduct("MBA-M3-512", "MacBook Air M3 512GB", ProductCategory.Electronics, apple.Id);
        var galaxyS24 = NewProduct("GAL-S24-256", "Galaxy S24 Ultra 256GB", ProductCategory.Electronics, samsung.Id);
        var galaxyWatch = NewProduct("GAL-WATCH-6", "Galaxy Watch 6", ProductCategory.Electronics, samsung.Id);
        var ps5 = NewProduct("SNY-PS5-STD", "PlayStation 5", ProductCategory.Electronics, sony.Id);
        var sonyHeadphones = NewProduct("SNY-WH-1000XM5", "Sony WH-1000XM5", ProductCategory.Electronics, sony.Id);
        var airForce = NewProduct("NK-AF1-WHT-42", "Air Force 1 White 42", ProductCategory.Clothing, nike.Id);
        var driFit = NewProduct("NK-DRI-TEE-L", "Dri-FIT Tee L", ProductCategory.Clothing, nike.Id);
        var ultraboost = NewProduct("AD-UB22-43", "Ultraboost 22 size 43", ProductCategory.Clothing, adidas.Id);
        var stanSmith = NewProduct("AD-STAN-41", "Stan Smith 41", ProductCategory.Clothing, adidas.Id);
        var tracksuit = NewProduct("AD-TRK-M", "Adidas Tracksuit M", ProductCategory.Clothing, adidas.Id);

        context.Products.AddRange(
            iphone, ipad, macbook,
            galaxyS24, galaxyWatch,
            ps5, sonyHeadphones,
            airForce, driFit,
            ultraboost, stanSmith, tracksuit);

        // Some products are intentionally left without images (for NOT EXISTS / LEFT JOIN IS NULL demo queries).
        context.ProductImages.AddRange(
            NewImage(iphone.Id, "https://example.com/iphone15-front.jpg", isPrimary: true),
            NewImage(iphone.Id, "https://example.com/iphone15-back.jpg", isPrimary: false),
            NewImage(ipad.Id, "https://example.com/ipad-pro.jpg", isPrimary: true),
            NewImage(galaxyS24.Id, "https://example.com/galaxy-s24.jpg", isPrimary: true),
            NewImage(galaxyWatch.Id, "https://example.com/galaxy-watch.jpg", isPrimary: true),
            NewImage(ps5.Id, "https://example.com/ps5-front.jpg", isPrimary: true),
            NewImage(ps5.Id, "https://example.com/ps5-side.jpg", isPrimary: false),
            NewImage(sonyHeadphones.Id, "https://example.com/sony-wh1000.jpg", isPrimary: true),
            NewImage(airForce.Id, "https://example.com/airforce1.jpg", isPrimary: true),
            NewImage(ultraboost.Id, "https://example.com/ultraboost.jpg", isPrimary: true),
            NewImage(stanSmith.Id, "https://example.com/stansmith-1.jpg", isPrimary: true),
            NewImage(stanSmith.Id, "https://example.com/stansmith-2.jpg", isPrimary: false),
            NewImage(tracksuit.Id, "https://example.com/tracksuit.jpg", isPrimary: true)
            // macbook, driFit — no images on purpose
        );

        // Varying counts/prices/lead times across products for richer aggregation queries.
        // driFit, tracksuit — no suppliers on purpose (for orphaned-product queries).
        context.ProductSuppliers.AddRange(
            new ProductSupplier { ProductId = iphone.Id, SupplierId = globalTech.Id, UnitCost = 850.00m, LeadTimeDays = 5 },
            new ProductSupplier { ProductId = iphone.Id, SupplierId = asiaSource.Id, UnitCost = 780.00m, LeadTimeDays = 14 },
            new ProductSupplier { ProductId = ipad.Id, SupplierId = globalTech.Id, UnitCost = 620.00m, LeadTimeDays = 5 },
            new ProductSupplier { ProductId = macbook.Id, SupplierId = globalTech.Id, UnitCost = 1100.00m, LeadTimeDays = 7 },
            new ProductSupplier { ProductId = macbook.Id, SupplierId = euroSupply.Id, UnitCost = 1180.00m, LeadTimeDays = 3 },
            new ProductSupplier { ProductId = galaxyS24.Id, SupplierId = asiaSource.Id, UnitCost = 720.00m, LeadTimeDays = 12 },
            new ProductSupplier { ProductId = galaxyS24.Id, SupplierId = quickShip.Id, UnitCost = 750.00m, LeadTimeDays = 4 },
            new ProductSupplier { ProductId = galaxyWatch.Id, SupplierId = asiaSource.Id, UnitCost = 180.00m, LeadTimeDays = 14 },
            new ProductSupplier { ProductId = ps5.Id, SupplierId = asiaSource.Id, UnitCost = 420.00m, LeadTimeDays = 14 },
            new ProductSupplier { ProductId = ps5.Id, SupplierId = quickShip.Id, UnitCost = 440.00m, LeadTimeDays = 6 },
            new ProductSupplier { ProductId = sonyHeadphones.Id, SupplierId = asiaSource.Id, UnitCost = 280.00m, LeadTimeDays = 12 },
            new ProductSupplier { ProductId = airForce.Id, SupplierId = globalTech.Id, UnitCost = 65.00m, LeadTimeDays = 5 },
            new ProductSupplier { ProductId = airForce.Id, SupplierId = euroSupply.Id, UnitCost = 72.00m, LeadTimeDays = 3 },
            new ProductSupplier { ProductId = airForce.Id, SupplierId = quickShip.Id, UnitCost = 68.00m, LeadTimeDays = 4 },
            new ProductSupplier { ProductId = ultraboost.Id, SupplierId = euroSupply.Id, UnitCost = 110.00m, LeadTimeDays = 3 },
            new ProductSupplier { ProductId = ultraboost.Id, SupplierId = quickShip.Id, UnitCost = 115.00m, LeadTimeDays = 5 },
            new ProductSupplier { ProductId = stanSmith.Id, SupplierId = euroSupply.Id, UnitCost = 75.00m, LeadTimeDays = 3 });

        await context.SaveChangesAsync(ct);
    }

    private static async Task<Brand> EnsureBrandAsync(CatalogDbContext context, string name, string country, CancellationToken ct)
    {
        var existing = await context.Brands.FirstOrDefaultAsync(b => b.Name == name, ct);
        if (existing is not null) return existing;

        var brand = new Brand { Id = Guid.NewGuid(), Name = name, Country = country };
        context.Brands.Add(brand);
        return brand;
    }

    private static async Task<Supplier> EnsureSupplierAsync(CatalogDbContext context, string name, string email, CancellationToken ct)
    {
        var existing = await context.Suppliers.FirstOrDefaultAsync(s => s.ContactEmail == email, ct);
        if (existing is not null) return existing;

        var supplier = new Supplier { Id = Guid.NewGuid(), Name = name, ContactEmail = email };
        context.Suppliers.Add(supplier);
        return supplier;
    }

    private static Product NewProduct(string sku, string name, ProductCategory category, Guid brandId) =>
        new()
        {
            Id = Guid.NewGuid(),
            Sku = sku,
            Name = name,
            Category = category,
            BrandId = brandId,
        };

    private static ProductImage NewImage(Guid productId, string url, bool isPrimary) =>
        new()
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            Url = url,
            IsPrimary = isPrimary,
        };
}
