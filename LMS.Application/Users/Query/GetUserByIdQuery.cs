using LMS.Application.Response;
using Mediator;

namespace LMS.Application.Users.Query
{
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public int UserId { get; set; }

        public GetUserByIdQuery(int id)
        {
            UserId = id;
        }
    }
}
