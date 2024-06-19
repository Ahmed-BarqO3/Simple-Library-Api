using LMS.Application.Common;
using LMS.Application.Response;
using Mediator;

namespace LMS.Application.BookCopys.Query;

public class GetAllCopiesQuery(PaginationFilter paginationQuery) : IRequest<List<BookCopyResponse>>
{
   public  PaginationFilter PaginationQuery  => paginationQuery;
}
