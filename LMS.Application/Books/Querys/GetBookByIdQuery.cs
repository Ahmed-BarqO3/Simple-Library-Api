using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Application.Response;
using Mediator;

namespace LMS.Application.Books.Querys;
public class GetBookByIdQuery : IRequest<BookResponse>
{
    public int Id { get; set; }

    public GetBookByIdQuery(int id)
    {
        Id = id;
    }
}
