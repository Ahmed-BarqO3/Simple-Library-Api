using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LMS.Core.Models;

public partial class Book
{
    [Key]
    [Column("BookID")]
    public int BookId { get; set; }

    [StringLength(255)]
    public string Title { get; set; } 

    [Column("ISBN")]
    [StringLength(50)]
    public string Isbn { get; set; } 

    public DateOnly PublicationDate { get; set; }

    [StringLength(255)]
    public string? AdditionalDetails { get; set; }

    [InverseProperty("Book")]
    public virtual ICollection<BookCopy> BookCopies { get; set; } = new List<BookCopy>();
}
