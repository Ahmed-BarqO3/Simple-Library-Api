namespace LMS.Application.Common;

public class PaginationQuery
{
    public PaginationQuery()
    { }
    public PaginationQuery(int pageNumber, int pageSize)
    {
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;
    }
    public  int pageSize { get; set; }
    public  int pageNumber { get; set; }
}
