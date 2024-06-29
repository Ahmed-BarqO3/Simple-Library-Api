using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using Mediator;

namespace LMS.Application.Books.Commands.HandlerCommands;
public class UpdateBookCommandHandler(IUnitOfWork context) : IRequestHandler<UpdateBookCommand, BookResponse>

{
    public async ValueTask<BookResponse> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = request.Adapt<Book>();
        await context.Books.Update(book);
        context.Save();

        return request.Adapt<BookResponse>();
    }
}
