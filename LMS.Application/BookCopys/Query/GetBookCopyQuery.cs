using LMS.Application.Response;
using Mediator;

namespace LMS.Application.BookCopys.Query;

public class GetBookCopyQuery(int id) : IRequest<BookCopyResponse>
{
    public int Id => id;
}
