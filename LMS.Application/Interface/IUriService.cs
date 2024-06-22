using LMS.Application.Common;

namespace LMS.Application.Interface;
public interface IUriService
{
    Uri GetPageUri(string route,int id);
    Uri GetAllPageUri(string route,PaginationQuery? paginationQuery);
}
