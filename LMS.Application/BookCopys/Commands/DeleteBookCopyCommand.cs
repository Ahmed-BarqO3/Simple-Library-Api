using LMS.Application.Response;
using Mediator;

namespace LMS.Application.BookCopys.Commands;

public class DeleteBookCopyCommand(int copyId) : IRequest<BookCopyResponse>
{
    public int CopyId { get;  } = copyId;
}
