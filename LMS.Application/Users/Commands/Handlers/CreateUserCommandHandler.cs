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
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
{
    private readonly IUnitOfWork _context;

    public CreateUserCommandHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async ValueTask<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.Adapt<User>();
        await _context.Users.AddAsync(user);

        _context.Save();

        return user.Adapt<UserResponse>();
    }
}
