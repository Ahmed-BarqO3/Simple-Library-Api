using LMS.Application.BookCopys.Commands;
using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.BookCopys.Commands.Handlers;

public class CreateBookCopyCommandHandler : IRequestHandler<CreateBookCopyCommand,BookCopyResponse>
{
    private readonly IUnitOfWork _context;

    public CreateBookCopyCommandHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async ValueTask<BookCopyResponse> Handle(CreateBookCopyCommand request, CancellationToken cancellationToken)
    {
        
        Core.Models.BookCopy copy = request.Adapt<Core.Models.BookCopy>();
         await _context.BookCopies.AddAsync(copy);
         _context.Save();
         
         return copy.Adapt<BookCopyResponse>();
    }
}
