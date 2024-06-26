namespace LMS.Application.Response;

public record FinesResponse(
    int FineId,
    int UserId,
    int BorrowingRecordId,
    byte? NumberOfLateDays,
    decimal FineAmount,
    bool PaymentStatus,
    BorrowingRecordResponse BorrowingRecord,
    UserResponse User);
