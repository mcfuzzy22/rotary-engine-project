using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace rotaryproject.Data.Models; // Ensure this namespace is correct

[Index("CategoryId", Name = "IX_Parts_CategoryID")] // Note: This references "CategoryId" (lowercase 'd')
[Index("Name", Name = "IX_Parts_Name")]
// If you change Sku C# property to SKU, this Index might need to be [Index("SKU", ...)] if your DB column is SKU
[Index("Sku", Name = "UQ__Parts__CA1ECF0DDFFFB887", IsUnique = true)] // This references "Sku" (lowercase 'k','u')
public partial class Part
{
    [Key]
    // If C# property is PartID and DB column is PartID, [Column("PartID")] is redundant but harmless.
    [Column("PartId")]
    public int PartId { get; set; } // MODIFIED to PartID (all caps ID)

    [Column("CategoryID")]
    public int CategoryId { get; set; } // Kept as CategoryId, ensure Razor uses this casing

    [StringLength(150)]
    public string Name { get; set; } = null!;

    // If C# property is SKU and DB column is SKU, [Column("SKU")] is redundant but harmless.
    [Column("Sku")]
    [StringLength(50)]
    public string? Sku { get; set; } // MODIFIED to SKU (all caps)

    public string? Description { get; set; }

    [StringLength(255)]
    public string ModelPath { get; set; } = null!;

    [StringLength(255)]
    public string? ImagePath { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? BasePrice { get; set; }

    [ForeignKey("CategoryId")] // This references the C# property "CategoryId"
    [InverseProperty("Parts")]
    public virtual PartCategory Category { get; set; } = null!;

    [InverseProperty("PartA")]
    public virtual ICollection<CompatibilityRule> CompatibilityRulePartAs { get; set; } = new List<CompatibilityRule>();

    [InverseProperty("PartB")]
    public virtual ICollection<CompatibilityRule> CompatibilityRulePartBs { get; set; } = new List<CompatibilityRule>();

    [InverseProperty("Part")]
    public virtual ICollection<PartStat> PartStats { get; set; } = new List<PartStat>();
}