using System.Reflection;
using LMS.Application.BorrowingRecords.Query;
using LMS.Application.Common;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controller;
[Route("api/v1/[controller]")]
[ApiController]
public class borrowingrecordController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async  Task<IActionResult> GetBorrowingRecords([FromQuery] PaginationQuery paginationQuery,CancellationToken cancellationToken)
    {
        var pagination = new PaginationFilter(paginationQuery.pageSize, paginationQuery.pageNumber);
        var query = new GetBorrowingRecordsQuery(pagination);

        var reslut = await mediator.Send(query, cancellationToken);

        return Ok(reslut);
    }
}
