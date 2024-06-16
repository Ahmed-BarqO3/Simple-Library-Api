using LMS.Application.Response;
using Mediator;

namespace LMS.Application.Books.Commands;
public class UpdateBookCommand : IRequest<BookResponse>
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string Isbn { get; set; }
    public DateOnly PublicationDate { get; set; }
    public string? AdditionalDetails { get; set; }
}
