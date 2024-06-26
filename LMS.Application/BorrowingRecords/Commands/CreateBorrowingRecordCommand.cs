using LMS.Application.Response;
using Mediator;

namespace LMS.Application.BorrowingRecords.Commands;

public record CreateBorrowingRecordCommand : IRequest<BorrowingRecordResponse>
{
    public int CopyId { get; set; }
    public int UserId { get; set; }
    public DateOnly DueDate { get; set; }
    public readonly DateOnly BorrowingDate = DateOnly.FromDateTime(DateTime.Now);

}
