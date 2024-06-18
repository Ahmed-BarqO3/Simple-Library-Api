using LMS.Application.Response;
using Mediator;

namespace LMS.Application.BookCopys.Commands;

public class DeleteBookCopyCommand : IRequest<BookCopyResponse>
{
    public DeleteBookCopyCommand(int copyId)
    {
        CopyId = copyId;
    }

    public int CopyId { get; set; }
}
