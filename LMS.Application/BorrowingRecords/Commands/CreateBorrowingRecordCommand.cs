using LMS.Application.Response;
using Mediator;

namespace LMS.Application.BorrowingRecords.Commands;

public class CreateBorrowingRecordCommand : IRequest<BorrowingRecordResponse>
{
    public int CopyId { get; set; }
    public int UserId { get; set; }
    public DateOnly BorrowingDate { get; set; }
    public DateOnly DueDate { get; set; }
}
