using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Users.Query.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUnitOfWork _context;

        public GetUserByIdQueryHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async ValueTask<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.GetByIdAsync(request.UserId);

            return user.Adapt<UserResponse>();
        }
    }
}
