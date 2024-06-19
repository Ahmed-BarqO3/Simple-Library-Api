namespace LMS.Application.Common;

public class PaginationFilter
{
    public  int pageSize { get; set; }
    public  int pageNumber { get; set; }
    
    public PaginationFilter()
    {
        this.pageNumber = pageNumber < 1 ? 1 : pageNumber;
        this.pageSize = pageSize > 100
            ? 100 
            : pageSize < 1
            ? 10
            : pageSize;
    }
}
