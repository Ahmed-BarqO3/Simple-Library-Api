using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LMS.Core.Models;

public partial class Reservation
{
    [Key]
    [Column("ReservationID")]
    public int ReservationId { get; set; }

    [Column("UserID")]
    public int UserId { get; set; }

    [Column("CopyID")]
    public int CopyId { get; set; }

    public DateOnly ReservationDate { get; set; }

    [ForeignKey("CopyId")]
    [InverseProperty("Reservations")]
    public virtual BookCopy Copy { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Reservations")]
    public virtual User User { get; set; } = null!;
}
