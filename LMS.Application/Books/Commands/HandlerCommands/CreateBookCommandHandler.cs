using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Application.Interface;
using LMS.Application.Response;
using LMS.Core.Models;
using Mapster;
using Mediator;

namespace LMS.Application.Books.Commands.HandlerCommands;
internal class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookResponse>
{
    private readonly IUnitOfWork _context;

    public CreateBookCommandHandler(IUnitOfWork context)
    {
        _context = context;
    }

    public async ValueTask<BookResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {


        Book book = request.Adapt<Book>();

        await _context.Books.AddAsync(book);
        _context.Save();

        return request.Adapt<BookResponse>();
    }
}
