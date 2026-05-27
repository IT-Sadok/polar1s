using Microsoft.AspNetCore.Http.HttpResults;
using Warranty.Service.Common.Pagination;
using Warranty.Service.Models;
using Warranty.Service.Services.Contracts;

namespace Warranty.Service.Endpoints;

public static class WarrantyEndpoints
{
    public static IEndpointRouteBuilder MapWarrantyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/warranties").WithTags("Warranties");

        group.MapGet("/", GetByCustomerAsync);
        group.MapGet("/{id:guid}", GetByIdAsync);

        return app;
    }

    private static async Task<Ok<CursorPage<WarrantyResponse>>> GetByCustomerAsync(
        [AsParameters] GetWarrantiesByCustomerRequest request,
        IWarrantyService service,
        CancellationToken ct)
    {
        var result = await service.GetByCustomerAsync(request, ct);
        return TypedResults.Ok(result.Value);
    }

    private static async Task<Results<Ok<WarrantyResponse>, NotFound<string>>> GetByIdAsync(
        Guid id,
        IWarrantyService service,
        CancellationToken ct)
    {
        var result = await service.GetByIdAsync(id, ct);
        return result.IsSuccess
            ? TypedResults.Ok(result.Value)
            : TypedResults.NotFound(result.Errors.First().Message);
    }
}
