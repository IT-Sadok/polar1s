using Catalog.Service.Common;
using Catalog.Service.Common.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Service.Controllers;

public abstract class ApiControllerBase : ControllerBase
{
    protected ActionResult ToErrorResult(Error error) => error.Type switch
    {
        ErrorType.NotFound => NotFound(error.Message),
        _ => StatusCode(StatusCodes.Status500InternalServerError),
    };

    protected ActionResult OkPaged<T>(PagedList<T> pagedList)
    {
        Response.Headers["X-Pagination"] = pagedList.CreateMetadata();
        Response.Headers.AccessControlExposeHeaders = "X-Pagination";
        return Ok(pagedList);
    }
}
