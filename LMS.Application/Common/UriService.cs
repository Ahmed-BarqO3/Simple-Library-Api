using LMS.Application.Interface;
using Microsoft.AspNetCore.WebUtilities;

namespace LMS.Application.Common;

public class UriService(string _baseUri) : IUriService
{
    public Uri GetAllPageUri(string route, PaginationQuery? paginationQuery)
    {
        if (paginationQuery is null)
            return new Uri(_baseUri);
        
        var modifiedUri = QueryHelpers.AddQueryString(_baseUri + route , "pageNumber", paginationQuery.pageNumber.ToString());
        modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", paginationQuery.pageSize.ToString());
 
        return new Uri(modifiedUri);
    }

    public Uri GetPageUri(string route,int id)
    {
        var uri = QueryHelpers.AddQueryString(_baseUri + route,"Id",id.ToString());
        return new Uri(uri);
    }
}
