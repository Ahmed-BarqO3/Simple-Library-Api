using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using Mediator;

namespace LMS.Application.Books.Commands.HandlerCommands;
public class CreateBookCommandHandler(IUnitOfWork context) : IRequestHandler<CreateBookCommand, BookResponse>
{
    public async ValueTask<BookResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        Book book = request.Adapt<Book>();

        await context.Books.AddAsync(book);
        context.Save();

        return request.Adapt<BookResponse>();
    }
}
