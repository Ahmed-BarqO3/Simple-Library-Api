using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LMS.Core.Models;

[Keyless]
public partial class BorrowingBooksView
{
    [Column("CopyID")]
    public int CopyId { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("ISBN")]
    [StringLength(50)]
    public string Isbn { get; set; } = null!;

    public DateOnly PublicationDate { get; set; }

    [StringLength(255)]
    public string? AdditionalDetails { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string? Availability { get; set; }
}
