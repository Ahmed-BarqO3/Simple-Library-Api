using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.BorrowingRecords.Query.Handlers;

public class GetBorrowingRecordsQueryHandler(IUnitOfWork context) : IRequestHandler<GetBorrowingRecordsQuery,List<BorrowingRecordResponse>>
{
    public async ValueTask<List<BorrowingRecordResponse>> Handle(GetBorrowingRecordsQuery request, CancellationToken cancellationToken)
    {
        var record = await context.BorrowingRecords.GetAllAsync(cancellationToken, request.PaginationQuery.pageSize,
            request.PaginationQuery.pageNumber,new []{"Copy","User"});
        return record.Adapt<List<BorrowingRecordResponse>>();
    }
}
