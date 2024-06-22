using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Users.Commands.Handlers
{
    public class DeleteUserCommandHandler(IUnitOfWork context) : IRequestHandler<DeleteUserCommand, UserResponse>
    {
        public async ValueTask<UserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await context.Users.GetByIdAsync(request.UserId);
            if (user is not null)
            {
                await context.Users.Delete(user);
                context.Save();
            }

            return user.Adapt<UserResponse>();
        }
    }
}
