using Catalog.Service.Common;
using Catalog.Service.Common.Const;
using Catalog.Service.Common.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Service.Controllers;

public abstract class ApiControllerBase : ControllerBase
{
    protected ActionResult ToErrorResult(IReadOnlyCollection<Error> errors)
    {
        var error = errors.First();
        return error.Type switch
        {
            ErrorType.NotFound => NotFound(error.Message),
            _ => StatusCode(StatusCodes.Status500InternalServerError),
        };
    }

    protected ActionResult OkPaged<T>(PagedList<T> pagedList)
    {
        Response.Headers[CustomHeaders.Pagination] = pagedList.CreateMetadata();
        Response.Headers.AccessControlExposeHeaders = CustomHeaders.Pagination;
        return Ok(pagedList);
    }
}
