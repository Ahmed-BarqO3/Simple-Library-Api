using LMS.Core.Models;


namespace LMS.Application.Interface;
public interface IUnitOfWork : IDisposable
{
    IBaseRepository<Book> Books { get; }
    IBaseRepository<BookCopy> BookCopies { get; }
    IBaseRepository<BorrowingRecord> BorrowingRecords { get; }
    IBaseRepository<Fine> Fines { get; }
    IBaseRepository<InformationBook> InformationBooks { get; }
    IBaseRepository<Reservation> Reservations { get; }
    IBaseRepository<Setting> Settings { get; }
    IBaseRepository<User> Users { get; }
        public int Save();
}
