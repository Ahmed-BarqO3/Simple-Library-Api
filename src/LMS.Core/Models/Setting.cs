using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LMS.Core.Models;

[Keyless]
public partial class Setting
{
    public byte DefualtBorrrowDays { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal DefaultFinePerDay { get; set; }
}
