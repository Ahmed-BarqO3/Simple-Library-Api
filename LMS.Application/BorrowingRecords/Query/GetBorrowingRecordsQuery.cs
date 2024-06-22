using LMS.Application.Common;
using LMS.Application.Response;
using Mediator;

namespace LMS.Application.BorrowingRecords.Query;

public class GetBorrowingRecordsQuery(PaginationFilter paginationQuery) : IRequest<List<BorrowingRecordResponse>>
{
    public PaginationFilter PaginationQuery = paginationQuery;
}
