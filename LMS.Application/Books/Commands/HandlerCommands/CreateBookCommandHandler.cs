using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using MediatR;

namespace LMS.Application.Books.Commands.HandlerCommands;
internal class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookResponse>
{
    private readonly IUnitOfWork _context;

    public CreateBookCommandHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async Task<BookResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        //Book book = new Book
        //{
        //    Title = request.Title,
        //    Isbn = request.Isbn,
        //    PublicationDate = request.PublicationDate,
        //    AdditionalDetails = request.AdditionalDetails
        //};

            Book book = request.Adapt<Book>();

            await _context.Books.AddAsync(book);
           _context.Save();

        return request.Adapt<BookResponse>();
    }
}
