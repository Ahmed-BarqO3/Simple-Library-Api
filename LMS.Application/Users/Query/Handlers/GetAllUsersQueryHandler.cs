using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Users.Query.Handlers
{
    public class GetAllUsersQueryHandler(IUnitOfWork context) : IRequestHandler<GetAllUsersQuery, List<UserResponse>>
    {
        public async ValueTask<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await context.Users.GetAllAsync(cancellationToken,request.PaginationQuery.pageSize,
                request.PaginationQuery.pageNumber);

            return users.Adapt<List<UserResponse>>();
        }
    }
}
