using LMS.Api.Common;
using LMS.Application.Interface;

namespace LMS.Application.Common.Helpers;

public static class PaginationHelper<T>
{
    public static PageResponse<T>? CreatePaginatedResponse(IUriService uri,string route,PaginationFilter pagination, List<T> response)
    {
        var nextPage = pagination.pageNumber >= 1
            ? uri.GetAllPageUri(route,new PaginationQuery(pagination.pageNumber + 1, pagination.pageSize)).ToString()
            : null;
        
        var previousPage = pagination.pageNumber - 1 >= 1
            ? uri.GetAllPageUri(route,new PaginationQuery(pagination.pageNumber - 1, pagination.pageSize)).ToString()
            : null;
        
        return new PageResponse<T>(response)
        {
            NextPage = response.Any() ? nextPage : null,
            PreviousPage = previousPage ,
            PageSize = pagination.pageSize >= 1? pagination.pageSize : (int?)null,
            PageNumber = pagination.pageNumber >= 1 ? pagination.pageNumber : (int?)null
        };
    }
}
