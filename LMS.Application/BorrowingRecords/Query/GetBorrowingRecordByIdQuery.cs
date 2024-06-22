using LMS.Application.Response;
using Mediator;

namespace LMS.Application.BorrowingRecords.Query;

public class GetBorrowingRecordByIdQuery(int id): IRequest<BorrowingRecordResponse>
{
    public int Id { get; } = id;
}
