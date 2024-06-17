using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Users.Commands.Validation
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserResponse>
    {
        readonly IUnitOfWork _context;
        public DeleteUserCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async ValueTask<UserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.GetByIdAsync(request.UserId);
            if (user is not null)
            {
                _context.Users.Delete(user);
                _context.Save();
            }

            return user.Adapt<UserResponse>();
        }
    }
}
