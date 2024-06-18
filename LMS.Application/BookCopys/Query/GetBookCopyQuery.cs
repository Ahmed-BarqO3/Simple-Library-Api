using LMS.Application.Response;
using Mediator;

namespace LMS.Application.BookCopys.Query;

public class GetBookCopyQuery :IRequest<BookCopyResponse>
{
    public int Id { get; set; }

    public GetBookCopyQuery(int id)
    {
        Id = id;
    }
}
