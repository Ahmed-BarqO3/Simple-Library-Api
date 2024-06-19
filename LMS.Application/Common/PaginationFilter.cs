namespace LMS.Application.Common;

public class PaginationFilter
{
    public  int pageSize { get; set; }
    public  int pageNumber { get; set; }
    
    public PaginationFilter(int size,int number)
    {
        this.pageNumber = number < 1 ? 1 : number;
        this.pageSize = size > 100
            ? 100 
            : size < 1
            ? 10
            : size;
    }
}
