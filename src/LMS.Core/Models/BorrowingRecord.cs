
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LMS.Core.Models;

public partial class BorrowingRecord
{
    [Key]
    [Column("BorrowingRecordID")]
    public int BorrowingRecordId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [Column("CopyID")]
    public int CopyId { get; set; }

    public DateOnly BorrowingDate { get; set; }

    public DateOnly DueDate { get; set; }

    public DateOnly? ActualReturnDate { get; set; }

    [ForeignKey("CopyId")]
    [InverseProperty("BorrowingRecords")]
    public virtual BookCopy Copy { get; set; } = null!;

    [InverseProperty("BorrowingRecord")]
    public virtual ICollection<Fine> Fines { get; set; } = new List<Fine>();

    [ForeignKey("UserId")]
    [InverseProperty("BorrowingRecords")]
    public virtual User User { get; set; } = null!;
}
