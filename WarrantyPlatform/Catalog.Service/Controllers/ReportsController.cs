using Catalog.Service.Models.Reports;
using Catalog.Service.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ApiControllerBase
{
    private readonly IReportsService _reportsService;

    public ReportsController(IReportsService reportsService)
    {
        _reportsService = reportsService;
    }

    [HttpGet("top-brands-by-avg-price")]
    public async Task<ActionResult<IReadOnlyList<TopBrandResponse>>> GetTopBrandsByAvgPrice(
        [FromQuery] TopBrandsRequest request,
        CancellationToken ct = default)
    {
        var result = await _reportsService.GetTopBrandsByAvgPriceAsync(request, ct);
        return result.IsSuccess ? Ok(result.Value) : ToErrorResult(result.Errors);
    }

    [HttpGet("products-without-images")]
    public async Task<ActionResult<IReadOnlyList<ProductWithoutImageResponse>>> GetProductsWithoutImages(
        CancellationToken ct = default)
    {
        var result = await _reportsService.GetProductsWithoutImagesAsync(ct);
        return result.IsSuccess ? Ok(result.Value) : ToErrorResult(result.Errors);
    }

    [HttpGet("cheapest-supplier-per-product")]
    public async Task<ActionResult<IReadOnlyList<CheapestSupplierResponse>>> GetCheapestSupplierPerProduct(
        CancellationToken ct = default)
    {
        var result = await _reportsService.GetCheapestSupplierPerProductAsync(ct);
        return result.IsSuccess ? Ok(result.Value) : ToErrorResult(result.Errors);
    }

    [HttpGet("products-with-multiple-suppliers")]
    public async Task<ActionResult<IReadOnlyList<ProductWithMultipleSuppliersResponse>>> GetProductsWithMultipleSuppliers(
        CancellationToken ct = default)
    {
        var result = await _reportsService.GetProductsWithMultipleSuppliersAsync(ct);
        return result.IsSuccess ? Ok(result.Value) : ToErrorResult(result.Errors);
    }

    [HttpGet("unused-brands")]
    public async Task<ActionResult<IReadOnlyList<UnusedBrandResponse>>> GetUnusedBrands(
        CancellationToken ct = default)
    {
        var result = await _reportsService.GetUnusedBrandsAsync(ct);
        return result.IsSuccess ? Ok(result.Value) : ToErrorResult(result.Errors);
    }
}
