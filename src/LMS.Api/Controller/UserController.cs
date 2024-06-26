using LMS.Application.Common;
using LMS.Application.Common.Helpers;
using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Application.Users;
using LMS.Application.Users.Commands;
using LMS.Application.Users.Query;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controller
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController(IUriService _uri,IMediator _mediator) : ControllerBase
    {
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

            if (result is null)
                return NotFound();

            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] PaginationQuery paginationQuery,CancellationToken cancellationToken)
        {
            var pagination = new PaginationFilter(paginationQuery.pageSize,paginationQuery.pageNumber);
            var query = new GetAllUsersQuery(pagination);
            var result = await _mediator.Send(query, cancellationToken);

            var paginationResponse =
                PaginationHelper<UserResponse>.CreatePaginatedResponse(_uri, ApiRoutes.User.Get, pagination,
                    result);

            return Ok(paginationResponse);
        }
    }
}
