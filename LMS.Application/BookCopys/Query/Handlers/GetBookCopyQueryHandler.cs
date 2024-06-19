using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.BookCopys.Query.Handlers;

public class GetBookCopyQueryHandler(IUnitOfWork context) : IRequestHandler<GetBookCopyQuery, BookCopyResponse>
{
    public async ValueTask<BookCopyResponse> Handle(GetBookCopyQuery request, CancellationToken cancellationToken)
    {
        var copy = await context.BookCopies.GetByIdAsync(request.Id);
        return copy.Adapt<BookCopyResponse>();
    }
}
