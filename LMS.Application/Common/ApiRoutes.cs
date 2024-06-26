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
    
    public static class BorrowingRecord
    {
        public const string Get = Base + "/borrowingrecord";
    }
    
    public static class Fine
    {
        public const string Get = Base + "/fine";
    }
    public static class Reservation
    {
        public const string Get = Base + "/reservation";
    }

    // Add other classes for other controllers as needed
}

