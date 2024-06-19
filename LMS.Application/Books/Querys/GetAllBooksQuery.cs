using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Application.Common;
using LMS.Application.Response;
using Mediator;

namespace LMS.Application.Books.Querys;
public class GetAllBooksQuery(PaginationFilter paginationQuery) : IRequest<List<BookResponse>>
{
    public PaginationFilter PaginationQuery { get; } = paginationQuery;
}
