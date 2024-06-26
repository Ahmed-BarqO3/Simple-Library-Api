using LMS.Application.BookCopys.Commands;
using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using Mediator;

namespace LMS.Application.BookCopys.Commands.Handlers;

public class CreateBookCopyCommandHandler(IUnitOfWork context)
    : IRequestHandler<CreateBookCopyCommand, BookCopyResponse?>
{
    public async ValueTask<BookCopyResponse?> Handle(CreateBookCopyCommand request, CancellationToken cancellationToken)
    {
        var copy = request.Adapt<BookCopy>();
        await context.BookCopies.AddAsync(copy);


        context.Save();
        return copy.Adapt<BookCopyResponse>();
    }
}
