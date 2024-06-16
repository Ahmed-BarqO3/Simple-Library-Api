namespace LMS.Application.Response;


public record UserResponse(
    int UserId,
    string Name,
    string ContactInformation,
    string LibraryCardNumber);

