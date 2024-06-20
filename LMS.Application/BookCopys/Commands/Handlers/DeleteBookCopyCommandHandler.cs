using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.BookCopys.Commands.Handlers;

public class DeleteBookCopyCommandHandler(IUnitOfWork context)
    : IRequestHandler<DeleteBookCopyCommand, BookCopyResponse>
{
    public async ValueTask<BookCopyResponse> Handle(DeleteBookCopyCommand request, CancellationToken cancellationToken)
    {
        var copy = await  context.BookCopies.GetByIdAsync(request.CopyId);
        if (copy is null)
        {
            return copy.Adapt<BookCopyResponse>();
        }

        await context.BookCopies.Delete(copy);
        context.Save();
        return copy.Adapt<BookCopyResponse>();
    }
}
