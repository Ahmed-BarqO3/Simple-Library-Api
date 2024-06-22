namespace LMS.Application.Response;

public record BorrowingRecordResponse(
    int BorrowingRecordId,
    int UserId,
    int CopyId,
    DateOnly BorrowingDate,
    DateOnly DueDate,
    DateOnly? ActualReturnDate);
