using LMS.Application.Common;
using LMS.Application.InformationBooks;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controller;

[Route(ApiRoutes.InformationBook.Get)]
[ApiController]
public class InformationBookController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetBooksInfo()
    {
        var query = new GetInformationBooksQury();
        var books = await mediator.Send(query);
        
        return Ok(books);
    }
}
