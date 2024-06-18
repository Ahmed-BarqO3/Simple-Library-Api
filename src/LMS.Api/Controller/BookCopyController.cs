using LMS.Application.BookCopys.Commands;
using LMS.Application.BookCopys.Query;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class BookCopyController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookCopyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatBookCopy([FromBody]CreateBookCopyCommand command)
    {
        var result = await _mediator.Send(command);
        if (result is not null )
         return CreatedAtAction(nameof(GetBookCopyById),new{ id = result.CopyId },result);

        return BadRequest();

    }
    [HttpDelete]
    public async Task<IActionResult> DeleteCopy(int id)
    {
        var command = new DeleteBookCopyCommand(id);
        var result = await _mediator.Send(command);
        if(result is not null)
            return NoContent();

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetBooksCopies()
    {
        var query = new GetAllCopiesQuery();
        var result = await _mediator.Send(query);

        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookCopyById(int id)
    {
        var query = new GetBookCopyQuery(id);
        var result = await _mediator.Send(query);

        if(result is not null)
            return Ok(result);
        return NotFound();
    }
}
