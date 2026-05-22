using Catalog.Service.Models.Reports;
using Catalog.Service.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IReportsService _reportsService;

    public ReportsController(IReportsService reportsService)
    {
        _reportsService = reportsService;
    }

    [HttpGet("top-brands-by-avg-price")]
    public async Task<ActionResult<IReadOnlyList<TopBrandResponse>>> GetTopBrandsByAvgPrice(
        [FromQuery] int limit = 10,
        CancellationToken ct = default)
    {
        var result = await _reportsService.GetTopBrandsByAvgPriceAsync(limit, ct);
        return Ok(result);
    }

    [HttpGet("products-without-images")]
    public async Task<ActionResult<IReadOnlyList<ProductWithoutImageResponse>>> GetProductsWithoutImages(
        CancellationToken ct = default)
    {
        var result = await _reportsService.GetProductsWithoutImagesAsync(ct);
        return Ok(result);
    }

    [HttpGet("cheapest-supplier-per-product")]
    public async Task<ActionResult<IReadOnlyList<CheapestSupplierResponse>>> GetCheapestSupplierPerProduct(
        CancellationToken ct = default)
    {
        var result = await _reportsService.GetCheapestSupplierPerProductAsync(ct);
        return Ok(result);
    }

    [HttpGet("products-with-multiple-suppliers")]
    public async Task<ActionResult<IReadOnlyList<ProductWithMultipleSuppliersResponse>>> GetProductsWithMultipleSuppliers(
        CancellationToken ct = default)
    {
        var result = await _reportsService.GetProductsWithMultipleSuppliersAsync(ct);
        return Ok(result);
    }

    [HttpGet("unused-brands")]
    public async Task<ActionResult<IReadOnlyList<UnusedBrandResponse>>> GetUnusedBrands(
        CancellationToken ct = default)
    {
        var result = await _reportsService.GetUnusedBrandsAsync(ct);
        return Ok(result);
    }
}
