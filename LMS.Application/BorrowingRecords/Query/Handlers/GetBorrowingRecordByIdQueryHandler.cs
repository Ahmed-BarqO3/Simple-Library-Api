using LMS.Application.Interface;
using LMS.Application.Response;
using Mapster;
using Mediator;

namespace LMS.Application.BorrowingRecords.Query.Handlers;

public class GetBorrowingRecordByIdQueryHandler(IUnitOfWork context) : IRequestHandler<GetBorrowingRecordByIdQuery,BorrowingRecordResponse>
{
    public async ValueTask<BorrowingRecordResponse> Handle(GetBorrowingRecordByIdQuery request, CancellationToken cancellationToken)
    {
        var record  = await context.BorrowingRecords.GetByIdAsync(request.Id);
        return record.Adapt<BorrowingRecordResponse>();
    }
}
