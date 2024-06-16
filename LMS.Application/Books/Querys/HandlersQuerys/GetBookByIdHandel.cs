using LMS.Application.Books.Querys;
using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Books.Querys.HandlersQuerys;
public class GetBookByIdHandel : IRequestHandler<GetBookByIdQuery, BookResponse>
{
    private readonly IUnitOfWork _context;

    public GetBookByIdHandel(IUnitOfWork context)
    {
        _context = context;
    }

    public async ValueTask<BookResponse> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _context.Books.GetByIdAsync(request.Id);
        return book.Adapt<BookResponse>();
    }
}
