using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.BookCopys.Query.Handlers;

public class GetAllCopiesQueryHandler(IUnitOfWork context) : IRequestHandler<GetAllCopiesQuery, List<BookCopyResponse>>
{
    public async ValueTask<List<BookCopyResponse>> Handle(GetAllCopiesQuery request, CancellationToken cancellationToken)
    {
        var copies =
            await context.BookCopies.GetAllAsync(request.PaginationQuery.pageSize, request.PaginationQuery.pageNumber,
                includes: new[] { "Book" });
        
        return copies.Adapt<List<BookCopyResponse>>();
    }
}
