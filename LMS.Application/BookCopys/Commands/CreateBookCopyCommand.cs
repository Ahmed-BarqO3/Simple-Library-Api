using LMS.Application.Response;
using Mediator;

namespace LMS.Application.BookCopys.Commands;

public class CreateBookCopyCommand : IRequest<BookCopyResponse>
{
    public int BookId { get; set; }
}
