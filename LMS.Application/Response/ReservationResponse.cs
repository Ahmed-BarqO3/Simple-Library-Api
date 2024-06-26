namespace LMS.Application.Response;

public record ReservationResponse(int ReservationId, int UserId, int CopyId, DateTime ReservationDate, BookCopyResponse Copy, UserResponse User);
