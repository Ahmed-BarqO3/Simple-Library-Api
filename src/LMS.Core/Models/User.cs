using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LMS.Core.Models;

public partial class User
{
    [Key]
    [Column("UserID")]
    public int UserId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string ContactInformation { get; set; } = null!;

    [StringLength(50)]
    public string LibraryCardNumber { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<BorrowingRecord> BorrowingRecords { get; set; } = new List<BorrowingRecord>();

    [InverseProperty("User")]
    public virtual ICollection<Fine> Fines { get; set; } = new List<Fine>();

    [InverseProperty("User")]
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
