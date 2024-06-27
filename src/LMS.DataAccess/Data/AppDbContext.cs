using System.Data;
using Microsoft.EntityFrameworkCore;
using LMS.Core.Models;

namespace LMS.Infrastructure.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookCopy> BookCopies { get; set; }

    public virtual DbSet<BorrowingBooksView> BorrowingBooksViews { get; set; }

    public virtual DbSet<BorrowingRecord> BorrowingRecords { get; set; }

    public virtual DbSet<Fine> Fines { get; set; }

    public virtual DbSet<InformationBook> InformationBooks { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C2276D984D44");

            entity.ToTable(tb => tb.HasTrigger("TR_AfterIsertNewBook"));
        });

        modelBuilder.Entity<BookCopy>(entity =>
        {
            entity.HasKey(e => e.CopyId).HasName("PK__BookCopi__C26CCCE57EB9A8F4");

            entity.ToTable(tb => tb.HasTrigger("TR_AfterDeleteBook"));

            entity.Property(e => e.AvailabilityStatus).HasDefaultValue(true);

            entity.HasOne(d => d.Book).WithMany(p => p.BookCopies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BookCopie__BookI__398D8EEE");
        });

        modelBuilder.Entity<BorrowingBooksView>(entity =>
        {
            entity.ToView("BorrowingBooks_View");
        });

        modelBuilder.Entity<BorrowingRecord>(entity =>
        {
            entity.HasKey(e => e.BorrowingRecordId).HasName("PK__Borrowin__D7C457FC843EDA3E");

            entity.ToTable(tb =>
            {
                tb.HasTrigger("TR_BorrowingBook");
                tb.HasTrigger("TR_CanceleBorrowing");
            });

            entity.HasOne(d => d.Copy).WithMany(p => p.BorrowingRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Borrowing__CopyI__3F466844");

            entity.HasOne(d => d.User).WithMany(p => p.BorrowingRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Borrowing__UserI__3E52440B");
        });

        modelBuilder.Entity<Fine>(entity =>
        {
            entity.HasKey(e => e.FineId).HasName("PK__Fines__9D4A9BCC946078AE");

            entity.HasOne(d => d.BorrowingRecord).WithMany(p => p.Fines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fines_BorrowingRecords");

            entity.HasOne(d => d.User).WithMany(p => p.Fines)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Fines__UserID__4222D4EF");
        });

        modelBuilder.Entity<InformationBook>(entity =>
        {
            entity.ToView("Information_Books");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PK__Reservat__B7EE5F040DE82C50");

            entity.ToTable(tb => tb.HasTrigger("TR_ReservationBook"));

            entity.HasOne(d => d.Copy).WithMany(p => p.Reservations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__CopyI__5070F446");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservati__UserI__4F7CD00D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC23B1124F");
        });

        modelBuilder.Entity<Setting>(e =>
        {
            e.HasNoKey();
            OnModelCreatingPartial(modelBuilder);
        });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
    
