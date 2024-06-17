using LMS.Application.Response;
using Mediator;

namespace LMS.Application.Users.Commands
{
    public class DeleteUserCommand : IRequest<UserResponse>
    {
        public int UserId { get; set; }

        public DeleteUserCommand(int userId)
        {
            UserId = userId;
        }
    }
}
