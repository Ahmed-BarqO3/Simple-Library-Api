using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Books.Querys.HandlersQuerys;
public class GetAllBooksQueryHandel : IRequestHandler<GetAllBooksQuery, List<BookResponse>>
{
    private readonly IUnitOfWork _context;
    public GetAllBooksQueryHandel(IUnitOfWork context)
    {
        _context = context;
    }
    public async ValueTask<List<BookResponse>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _context.Books.GetAllAsync(cancellationToken,request.PaginationQuery.pageSize,
            request.PaginationQuery.pageNumber);
        
        var result = books.Adapt<List<BookResponse>>();

        return result;
    }
}
