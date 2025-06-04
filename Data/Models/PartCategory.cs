using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace rotaryproject.Data.Models;

[Index("Name", Name = "UQ__PartCate__737584F67434BF54", IsUnique = true)]
public partial class PartCategory
{
    [Key]
    [Column("CategoryID")]
    public int CategoryId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }

    //[InverseProperty("Category")]
    //public virtual ICollection<Part> Parts { get; set; } = new List<Part>();
}
