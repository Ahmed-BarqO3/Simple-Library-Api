using LMS.Api.Common;
using LMS.Application.BookCopys.Commands;
using LMS.Application.BookCopys.Query;
using LMS.Application.Response;
using Mediator;
using LMS.Application.Common;
using LMS.Application.Common.Helpers;
using LMS.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controller;

[ApiController]
[Route("api/v1/[controller]")]
public class BookCopyController(IMediator mediator,IUriService uri) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatBookCopy([FromQuery] CreateBookCopyCommand command)
    {
        var result = await mediator.Send(command);
        return CreatedAtAction(nameof(GetBookCopyById), new { id = result.CopyId }, result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCopy(int id)
    {
        var command = new DeleteBookCopyCommand(id);
        var result = await mediator.Send(command);
        if (result is null)
            return BadRequest();
        
        return NoContent();

    }

    [HttpGet]
    public async Task<IActionResult> GetBooksCopies([FromQuery] PaginationQuery paginationQuery,CancellationToken cancellationToken)
    {
        var pagination = new PaginationFilter(paginationQuery.pageSize,paginationQuery.pageNumber);

        var query = new GetAllCopiesQuery(pagination);
        var result = await mediator.Send(query,cancellationToken);
        var paginationResponse =
            PaginationHelper<BookCopyResponse>.CreatePaginatedResponse(uri, ApiRoutes.BookCopy.Get, pagination,
                result);

        return Ok(paginationResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookCopyById(int id)
    {
        var query = new GetBookCopyQuery(id);
        var result = await mediator.Send(query);
        if (result is null)
            return NotFound();
        
        return Ok(result);
    }
}
