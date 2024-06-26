using LMS.Application.BorrowingRecords.Commands;
using LMS.Application.BorrowingRecords.Query;
using LMS.Application.Common;
using LMS.Application.Common.Helpers;
using LMS.Application.Interface;
using LMS.Application.Response;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controller;

[Route("api/v1/[controller]")]
[ApiController]
public class borrowingrecordController(IMediator mediator,IUriService uri) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> AddBorroiwngRecord(CreateBorrowingRecordCommand command)
    {
        var reslut = await mediator.Send(command);
        
        if(reslut is not null)
            return Ok(reslut);

        return BadRequest();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateBorroiwngRecord(UpdateBorrowingRecordCommand command)
    {
        var reslut = await mediator.Send(command);
        
        if(reslut is not null)
            return Ok(reslut);

        return BadRequest();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetBorrowingRecords([FromQuery] PaginationQuery paginationQuery,
        CancellationToken cancellationToken)
    {
        var pagination = new PaginationFilter(paginationQuery.pageSize, paginationQuery.pageNumber);
        var query = new GetBorrowingRecordsQuery(pagination);

        var result = await mediator.Send(query, cancellationToken);
        var paginationResponse =
            PaginationHelper<BorrowingRecordResponse>.CreatePaginatedResponse(uri, ApiRoutes.BorrowingRecord.Get, pagination,
                result);

        return Ok(paginationResponse);
    }

    [HttpGet("Id")]
    public async Task<IActionResult> GetBorroingRecordById(int Id)
    {
        var qury = new GetBorrowingRecordByIdQuery(Id);
        var result = await mediator.Send(qury);

        if (result is null)
            return NotFound();

        return Ok(result);
    }
}
