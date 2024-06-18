using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.BookCopys.Commands.Handlers;

public class DeleteBookCopyCommandHandler : IRequestHandler<DeleteBookCopyCommand,BookCopyResponse>
{
    private readonly IUnitOfWork _context;

    public DeleteBookCopyCommandHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async ValueTask<BookCopyResponse> Handle(DeleteBookCopyCommand request, CancellationToken cancellationToken)
    {
        var copy = await _context.BookCopies.GetByIdAsync(request.CopyId);
        if (copy is not null)
        {
            _context.BookCopies.Delete(copy);
            _context.Save();
        }
        return copy.Adapt<BookCopyResponse>();
    }
}
