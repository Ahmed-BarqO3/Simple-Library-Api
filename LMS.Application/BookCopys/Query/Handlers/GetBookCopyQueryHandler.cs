using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.BookCopys.Query.Handlers;

public class GetBookCopyQueryHandler : IRequestHandler<GetBookCopyQuery,BookCopyResponse>
{
    private IUnitOfWork _context;

    public GetBookCopyQueryHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async ValueTask<BookCopyResponse> Handle(GetBookCopyQuery request, CancellationToken cancellationToken)
    {
        var copy = await _context.BookCopies.GetByIdAsync(request.Id);
        return copy.Adapt<BookCopyResponse>();
    }
}
