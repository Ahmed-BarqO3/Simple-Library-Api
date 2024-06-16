using Microsoft.AspNetCore.Mvc;
using LMS.Application.Books.Querys;
using Mediator;
using LMS.Application.Books.Commands;

namespace LMS.Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetByID), new { BookId = result.BookId }, result);
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
    public async Task<IActionResult> GetAllBooks()
    {
        var qury = new GetAllBooksQuery();
        var result = await _mediator.Send(qury);

        return Ok(result);
    }
}
