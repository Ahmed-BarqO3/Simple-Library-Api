using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.BookCopys.Query.Handlers;

public class GetAllCopiesQueryHandler : IRequestHandler<GetAllCopiesQuery,List<BookCopyResponse>>
{
    private readonly IUnitOfWork _context;

    public GetAllCopiesQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async ValueTask<List<BookCopyResponse>> Handle(GetAllCopiesQuery request, CancellationToken cancellationToken)
    {
        var copies = await _context.BookCopies.GetAllAsync(new []{"Book"});
        return copies.Adapt<List<BookCopyResponse>>();
    }
}
