using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using Mediator;

namespace LMS.Application.Users.Commands.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponse>
    {
        readonly IUnitOfWork _context;

        public UpdateUserCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async ValueTask<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.Adapt<User>();

            _context.Users.Update(user);
            _context.Save();

            return user.Adapt<UserResponse>();
        }
    }
}
