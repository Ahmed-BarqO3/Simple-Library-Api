namespace LMS.Api.Common;

public class Response<T>(T data)
{
    public T Data => data;
}
