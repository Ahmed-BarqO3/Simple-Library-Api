using LMS.Application.Response;
using Mediator;

namespace LMS.Application.Users.Commands
{
    public class UpdateUserCommand : IRequest<UserResponse>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string ContactInformation { get; set; }
    }
}
