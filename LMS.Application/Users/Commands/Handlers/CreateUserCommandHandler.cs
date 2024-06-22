using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using Mediator;

namespace LMS.Application.Users.Commands.Handlers;
public class CreateUserCommandHandler(IUnitOfWork context) : IRequestHandler<CreateUserCommand, UserResponse>
{
    public async ValueTask<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.Adapt<User>();
        await context.Users.AddAsync(user);

        context.Save();

        return user.Adapt<UserResponse>();
    }
}
