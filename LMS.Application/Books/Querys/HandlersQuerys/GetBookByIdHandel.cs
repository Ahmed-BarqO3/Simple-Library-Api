using LMS.Application.Books.Querys;
using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.Books.Querys.HandlersQuerys;
public class GetBookByIdHandel(IUnitOfWork context) : IRequestHandler<GetBookByIdQuery, BookResponse?>
{
    public async ValueTask<BookResponse?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await context.Books.GetByIdAsync(request.Id);
        return book.Adapt<BookResponse>();
    }
}
