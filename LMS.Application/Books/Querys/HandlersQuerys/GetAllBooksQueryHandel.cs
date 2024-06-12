using LMS.Application.Books.Querys;
using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Books.Querys.HandlersQuerys;
public class GetAllBooksQueryHandel : IRequestHandler<GetAllBooksQuery, List<BookResponse>>
{
    private readonly IUnitOfWork _context;

    public GetAllBooksQueryHandel(IUnitOfWork context)
    {
        _context = context;
    }
    public async Task<List<BookResponse>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _context.Books.GetAllAsync();
        return books.Adapt<List<BookResponse>>();
    }
}
