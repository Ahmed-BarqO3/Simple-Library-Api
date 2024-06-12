using MediatR;

namespace LMS.Application.Response;
public record BookResponse 
(
         int BookId ,
         string Title ,
         string Isbn ,
         DateOnly PublicationDate ,
         string? AdditionalDetails
);
