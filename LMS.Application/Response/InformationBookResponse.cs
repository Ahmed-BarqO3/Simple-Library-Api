namespace LMS.Application.Response;

public record InformationBookResponse(int BookId, string Title, string Isbn, DateOnly PublicationDate, string? AdditionalDetails, int? NumberOfBooks);
