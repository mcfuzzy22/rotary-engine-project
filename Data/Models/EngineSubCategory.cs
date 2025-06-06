// In Data/Models/EngineSubCategory.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace rotaryproject.Data.Models
{
    public class EngineSubCategory
    {
        [Key]
        public int EngineSubCategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; } = string.Empty;

        public int DisplayOrder { get; set; } = 0;

        // Link to the top-level Main Category
        [Required]
        public int EngineMainCategoryId { get; set; }
        [ForeignKey("EngineMainCategoryId")]
        public virtual EngineMainCategory? MainCategory { get; set; }

        // --- For Self-Referencing Hierarchy ---
        public int? ParentEngineSubCategoryId { get; set; } // Nullable FK to itself

        [ForeignKey("ParentEngineSubCategoryId")]
        public virtual EngineSubCategory? ParentSubCategory { get; set; } // Navigation to parent

        public virtual ICollection<EngineSubCategory> ChildSubCategories { get; set; } = new List<EngineSubCategory>();
        // --- End Self-Referencing Hierarchy ---
        public string? CssColorClass { get; set; }
        // Parts are typically associated with the "leaf" subcategories
        public virtual ICollection<Part> Parts { get; set; } = new List<Part>();
    }
}