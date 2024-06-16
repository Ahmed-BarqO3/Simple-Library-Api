using LMS.Application.Users.Query;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var Query = new GetAllUsersQuery();
            var result = await _mediator.Send(Query);

            if (result is not null)
                return Ok(result);

            return NoContent();

        }
    }
}
