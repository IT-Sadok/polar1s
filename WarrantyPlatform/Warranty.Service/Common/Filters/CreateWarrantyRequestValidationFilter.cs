using FluentValidation;
using Warranty.Service.Models;

namespace Warranty.Service.Common.Filters;

public sealed class CreateWarrantyRequestValidationFilter(IValidator<CreateWarrantyRequest> validator) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = context.GetArgument<CreateWarrantyRequest>(0);
        var result = await validator.ValidateAsync(request, context.HttpContext.RequestAborted);

        if (!result.IsValid)
        {
            return TypedResults.ValidationProblem(result.ToDictionary());
        }

        return await next(context);
    }
}
