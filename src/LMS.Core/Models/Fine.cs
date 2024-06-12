using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LMS.Core.Models;

public partial class Fine
{
    [Key]
    [Column("FineID")]
    public int FineId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [Column("BorrowingRecordID")]
    public int BorrowingRecordId { get; set; }

    public byte? NumberOfLateDays { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal FineAmount { get; set; }

    public bool PaymentStatus { get; set; }

    [ForeignKey("BorrowingRecordId")]
    [InverseProperty("Fines")]
    public virtual BorrowingRecord BorrowingRecord { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Fines")]
    public virtual User User { get; set; } = null!;
}
