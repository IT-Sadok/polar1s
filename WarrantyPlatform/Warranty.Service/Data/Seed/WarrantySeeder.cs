using Microsoft.EntityFrameworkCore;
using Warranty.Service.Entities;
using Warranty.Service.Entities.Const;

namespace Warranty.Service.Data.Seed;

public static class WarrantySeeder
{
    // Deterministic customer ids so subsequent reseeds don't fragment fake data across customers.
    private static readonly Guid AliceId = Guid.Parse("11111111-1111-1111-1111-111111111111");
    private static readonly Guid BobId = Guid.Parse("22222222-2222-2222-2222-222222222222");
    private static readonly Guid CarolId = Guid.Parse("33333333-3333-3333-3333-333333333333");

    // Synthetic product ids — these do NOT match real Catalog seed (Catalog uses Guid.NewGuid()).
    // Trust-the-client design: Warranty stores ProductId opaquely without cross-service validation.
    private static readonly Guid ProductIphone = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000001");
    private static readonly Guid ProductIpad = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000002");
    private static readonly Guid ProductPs5 = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000003");
    private static readonly Guid ProductHeadphones = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000004");
    private static readonly Guid ProductSneakers = Guid.Parse("aaaaaaaa-0000-0000-0000-000000000005");

    public static async Task SeedAsync(WarrantyDbContext context, CancellationToken ct = default)
    {
        if (await context.Warranties.AnyAsync(ct))
        {
            return;
        }

        var now = DateTime.UtcNow;

        // Mix of active and "past-expiry but Status=Active" — the latter feeds the future batch-job demo
        // (Phase 10 expiry-sweep will flip these to Expired).
        context.Warranties.AddRange(
            NewWarranty(ProductIphone, AliceId, purchasedDaysAgo: 30, durationYears: 2, WarrantyStatus.Active, now),
            NewWarranty(ProductIpad, AliceId, purchasedDaysAgo: 100, durationYears: 1, WarrantyStatus.Active, now),
            NewWarranty(ProductPs5, BobId, purchasedDaysAgo: 200, durationYears: 1, WarrantyStatus.Active, now),
            NewWarranty(ProductHeadphones, BobId, purchasedDaysAgo: 400, durationYears: 1, WarrantyStatus.Active, now), // already past expiry
            NewWarranty(ProductSneakers, BobId, purchasedDaysAgo: 10, durationYears: 2, WarrantyStatus.Active, now),
            NewWarranty(ProductIphone, CarolId, purchasedDaysAgo: 5, durationYears: 2, WarrantyStatus.Active, now),
            NewWarranty(ProductHeadphones, CarolId, purchasedDaysAgo: 60, durationYears: 2, WarrantyStatus.Active, now),
            NewWarranty(ProductIpad, CarolId, purchasedDaysAgo: 500, durationYears: 1, WarrantyStatus.Active, now)); // already past expiry

        await context.SaveChangesAsync(ct);
    }

    private static WarrantyEntity NewWarranty(
        Guid productId,
        Guid customerId,
        int purchasedDaysAgo,
        int durationYears,
        WarrantyStatus status,
        DateTime now) =>
        new()
        {
            Id = Guid.NewGuid(),
            ProductId = productId,
            CustomerId = customerId,
            PurchaseDate = now.AddDays(-purchasedDaysAgo),
            ExpiresAt = now.AddDays(-purchasedDaysAgo).AddYears(durationYears),
            Status = status,
        };
}
