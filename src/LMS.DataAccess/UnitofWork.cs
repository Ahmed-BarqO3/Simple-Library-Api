using LMS.Application;
using LMS.Application.Interface;
using LMS.Core.Models;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Repositories;


namespace LMS.Infrastructure;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        Books = new BaseRepository<Book>(_context);
        BookCopies = new BaseRepository<BookCopy>(_context);
        BorrowingRecords = new BaseRepository<BorrowingRecord>(_context);
        Fines = new BaseRepository<Fine>(_context);
        InformationBooks = new BaseRepository<InformationBook>(_context);
        Reservations = new BaseRepository<Reservation>(_context);
        Settings = new BaseRepository<Setting>(_context);
        Users = new BaseRepository<User>(_context);
        BorrowingRecords = new BaseRepository<BorrowingRecord>(_context);
        Fines = new BaseRepository<Fine>(_context);
        InformationBooks = new BaseRepository<InformationBook>(_context);
        Reservations = new BaseRepository<Reservation>(_context);
        Settings = new BaseRepository<Setting>(_context);
        Users = new BaseRepository<User>(_context);

    }
    public IBaseRepository<Book> Books { get; private set; }

    public IBaseRepository<BookCopy> BookCopies { get; private set; }

    public IBaseRepository<BorrowingRecord> BorrowingRecords { get; private set; }

    public IBaseRepository<Fine> Fines { get; private set; }

    public IBaseRepository<InformationBook> InformationBooks { get; private set; }

    public IBaseRepository<Reservation> Reservations { get; private set; }

    public IBaseRepository<Setting> Settings { get; private set; }

    public IBaseRepository<User> Users { get; private set; }


    public void Dispose()
    {
        _context.Dispose();
    }

    public int Save()
    {
        return _context.SaveChanges();
    }
}
