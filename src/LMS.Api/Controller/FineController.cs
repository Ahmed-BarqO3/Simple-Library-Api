using LMS.Application.Common;
using LMS.Application.Common.Helpers;
using LMS.Application.Fines.Query;
using LMS.Application.Interface;
using LMS.Application.Response;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controller;

[Route(ApiRoutes.Fine.Get)]
[ApiController]
public class FineController(IMediator mediator,IUriService uri) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult>  GetFines([FromQuery] PaginationQuery paginationQuery,CancellationToken cancellationToken)
    {
        var paginationFilter = new PaginationFilter(paginationQuery.pageSize, paginationQuery.pageNumber);
        var query = new GetFinesQuery(paginationFilter);
        
        var result = await mediator.Send(query, cancellationToken);
        
        var paginationResponse =
            PaginationHelper<FinesResponse>.CreatePaginatedResponse(uri, ApiRoutes.Fine.Get, paginationFilter, result);
        return Ok(paginationResponse);
    }
    
}
