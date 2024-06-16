using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using Mediator;

namespace LMS.Application.Books.Commands.HandlerCommands;
public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookResponse>

{
    private readonly IUnitOfWork _context;

    public UpdateBookCommandHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async ValueTask<BookResponse> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = request.Adapt<Book>();
        _context.Books.Update(book);
        _context.Save();

        return request.Adapt<BookResponse>();
    }
}
