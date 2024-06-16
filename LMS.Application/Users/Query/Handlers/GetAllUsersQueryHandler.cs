using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Users.Query.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponse>>
    {
        private readonly IUnitOfWork _context;

        public GetAllUsersQueryHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async ValueTask<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.GetAllAsync();

            return users.Adapt<List<UserResponse>>();
        }
    }
}
