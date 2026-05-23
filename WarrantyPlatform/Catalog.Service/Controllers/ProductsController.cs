using Catalog.Service.Models;
using Catalog.Service.Services.Contracts;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ApiControllerBase
{
    private readonly IProductService _productService;
    private readonly IValidator<CreateProductRequest> _createValidator;
    private readonly IValidator<UpdateProductRequest> _updateValidator;

    public ProductsController(
        IProductService productService,
        IValidator<CreateProductRequest> createValidator,
        IValidator<UpdateProductRequest> updateValidator)
    {
        _productService = productService;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductResponse>>> GetAll(
        [FromQuery] GetProductsRequest request,
        CancellationToken ct)
    {
        var result = await _productService.GetAllAsync(request, ct);
        return result.IsSuccess ? OkPaged(result.Value!) : ToErrorResult(result.Error!);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductResponse>> GetById(Guid id, CancellationToken ct)
    {
        var result = await _productService.GetByIdAsync(id, ct);
        return result.IsSuccess ? Ok(result.Value) : ToErrorResult(result.Error!);
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponse>> Create(CreateProductRequest request, CancellationToken ct)
    {
        var validationResult = await _createValidator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
        }

        var result = await _productService.CreateAsync(request, ct);
        if (!result.IsSuccess)
        {
            return ToErrorResult(result.Error!);
        }

        return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProductResponse>> Update(Guid id, UpdateProductRequest request, CancellationToken ct)
    {
        var validationResult = await _updateValidator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(validationResult.ToDictionary()));
        }

        var result = await _productService.UpdateAsync(id, request, ct);
        return result.IsSuccess ? Ok(result.Value) : ToErrorResult(result.Error!);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
    {
        var result = await _productService.DeleteAsync(id, ct);
        return result.IsSuccess ? NoContent() : ToErrorResult(result.Error!);
    }
}
