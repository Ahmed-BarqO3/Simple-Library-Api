using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using Mediator;

namespace LMS.Application.Users.Commands.Handlers
{
    public class UpdateUserCommandHandler(IUnitOfWork context) : IRequestHandler<UpdateUserCommand, UserResponse>
    {
        public async ValueTask<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.Adapt<User>();

            await context.Users.Update(user);
            context.Save();

            return user.Adapt<UserResponse>();
        }
    }
}
