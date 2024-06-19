using LMS.Api.Common;
using LMS.Application.BookCopys.Commands;
using LMS.Application.BookCopys.Query;
using LMS.Application.Response;
using Mediator;
using LMS.Api.Common;
using LMS.Application.Common;
using LMS.Application.Common.Helpers;
using LMS.Application.Interface;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class BookCopyController(IMediator _mediator,IUriService _uri) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatBookCopy([FromBody] CreateBookCopyCommand command)
    {
        var result = await _mediator.Send(command);
        if (result is not null)
            return CreatedAtAction(nameof(GetBookCopyById), new { id = result.CopyId }, result);

        return BadRequest();

    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCopy(int id)
    {
        var command = new DeleteBookCopyCommand(id);
        var result = await _mediator.Send(command);
        if (result is not null)
            return NoContent();

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetBooksCopies([FromQuery] PaginationQuery paginationQuery)
    {
        var pagination = paginationQuery.Adapt<PaginationFilter>();

        var query = new GetAllCopiesQuery(pagination);
        var result = await _mediator.Send(query);
        var paginationResponse =
            PaginationHelper<BookCopyResponse>.CreatePaginatedResponse(_uri, ApiRoutes.BookCopy.Get, pagination,
                result);

        return Ok(paginationResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookCopyById(int id)
    {
        var query = new GetBookCopyQuery(id);
        var result = await _mediator.Send(query);

        if (result is not null)
            return Ok(result);
        return NotFound();
    }
}
