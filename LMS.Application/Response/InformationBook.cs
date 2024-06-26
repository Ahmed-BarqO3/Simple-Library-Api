namespace LMS.Application.Response;

public record InformationBook(int BookId, string Title, string Isbn, DateOnly PublicationDate, string? AdditionalDetails, int? NumberOfBooks);
