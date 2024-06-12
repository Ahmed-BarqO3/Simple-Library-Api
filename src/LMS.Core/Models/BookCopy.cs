using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LMS.Core.Models;

public partial class BookCopy
{
    [Key]
    [Column("CopyID")]
    public int CopyId { get; set; }

    [Column("BookID")]
    public int BookId { get; set; }

    public bool AvailabilityStatus { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("BookCopies")]
    public virtual Book Book { get; set; } = null!;

    [InverseProperty("Copy")]
    public virtual ICollection<BorrowingRecord> BorrowingRecords { get; set; } = new List<BorrowingRecord>();

    [InverseProperty("Copy")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
