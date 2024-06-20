namespace LMS.Application.Common;

public static class ApiRoutes
{
    private const string Base = "/api/v1";

    public static class BookCopy
    {
        public const string Get = Base + "/BookCopy";
    }

    public static class Book
    {
        public const string Get = Base + "/Book";
    }

    public static class User
    {
        public const string Get = Base + "/User";
    }

    // Add other classes for other controllers as needed
}

