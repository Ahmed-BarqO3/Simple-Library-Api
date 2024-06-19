using Microsoft.AspNetCore.Mvc;
using LMS.Application.Books.Querys;
using Mediator;
using LMS.Application.Books.Commands;
using LMS.Application.Common;
using LMS.Application.Common.Helpers;
using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;

namespace LMS.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class BookController(IUriService _uri,IMediator _mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetByID), new { id = result.BookId }, result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBooke([FromBody] UpdateBookCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }


    [HttpGet("{BookId:int}")]
    public async Task<IActionResult> GetByID(int BookId)
    {
        var query = new GetBookByIdQuery(BookId);
        var result = await _mediator.Send(query);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllBooks([FromQuery]PaginationQuery paginationQuery)
    {
        var pagination = paginationQuery.Adapt<PaginationFilter>();
        var qury = new GetAllBooksQuery(pagination);
        var result = await _mediator.Send(qury);
        
        var paginationResponse =
            PaginationHelper<BookResponse>.CreatePaginatedResponse(_uri, ApiRoutes.Book.Get, pagination,
                result);

        return Ok(paginationResponse);
    }
}
