using LMS.Application.Common;
using LMS.Application.Response;
using Mediator;

namespace LMS.Application.Users.Query
{
    public class GetAllUsersQuery(PaginationFilter paginationQuery) : IRequest<List<UserResponse>>
    {
        public PaginationFilter PaginationQuery { get; } = paginationQuery;
    }
}
