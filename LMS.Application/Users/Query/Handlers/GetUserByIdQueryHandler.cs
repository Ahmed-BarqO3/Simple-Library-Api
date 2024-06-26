using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Users.Query.Handlers
{
    public class GetUserByIdQueryHandler(IUnitOfWork context) : IRequestHandler<GetUserByIdQuery, UserResponse?>
    {
        public async ValueTask<UserResponse?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await context.Users.GetByIdAsync(request.UserId);

            return user.Adapt<UserResponse?>();
        }
    }
}
