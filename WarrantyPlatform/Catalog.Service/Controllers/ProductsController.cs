using Catalog.Service.Models;
using Catalog.Service.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ApiControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductResponse>>> GetAll(
        [FromQuery] GetProductsRequest request,
        CancellationToken ct)
    {
        var result = await _productService.GetAllAsync(request, ct);
        return result.IsSuccess ? OkPaged(result.Value!) : ToErrorResult(result.Errors);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductResponse>> GetById(Guid id, CancellationToken ct)
    {
        var result = await _productService.GetByIdAsync(id, ct);
        return result.IsSuccess ? Ok(result.Value) : ToErrorResult(result.Errors);
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponse>> Create(CreateProductRequest request, CancellationToken ct)
    {
        var result = await _productService.CreateAsync(request, ct);
        if (!result.IsSuccess)
        {
            return ToErrorResult(result.Errors);
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProductResponse>> Update(Guid id, UpdateProductRequest request, CancellationToken ct)
    {
        var result = await _productService.UpdateAsync(id, request, ct);
        return result.IsSuccess ? Ok(result.Value) : ToErrorResult(result.Errors);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
    {
        var result = await _productService.DeleteAsync(id, ct);
        return result.IsSuccess ? NoContent() : ToErrorResult(result.Errors);
    }
}
