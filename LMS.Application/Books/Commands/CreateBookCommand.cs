using LMS.Application.Response;
using MediatR;

namespace LMS.Application.Books.Commands;
public class CreateBookCommand :IRequest<BookResponse>
{
    public  int BookId { get; set; }
    public  string Title { get; set; }
    public  string Isbn { get; set; }
    public DateOnly PublicationDate { get; set; }
    public string? AdditionalDetails { get; set; }
}
