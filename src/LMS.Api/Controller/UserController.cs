using LMS.Application.Users;
using LMS.Application.Users.Commands;
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

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result is not null)
                return CreatedAtAction(nameof(GetUserById), new { id = result.UserId }, result);

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (result is not null)
                return NoContent();

            return BadRequest();
        }
    

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var command = new DeleteUserCommand(id);
            var result = await _mediator.Send(command);
            if (result is not null)
                return NoContent();

            return BadRequest();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var Query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(Query);

            if (result is not null)
                return Ok(result);

            return NotFound();
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
