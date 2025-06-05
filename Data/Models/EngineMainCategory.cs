// In Data/Models/EngineMainCategory.cs
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace rotaryproject.Data.Models // Verify your project's namespace
{
    public class EngineMainCategory
    {
        [Key]
        public int EngineMainCategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty; // e.g., "BLOCK COMPONENTS"

        public int DisplayOrder { get; set; } = 0;

        // Navigation property to its subcategories
        public string? CssColorClass { get; set; }
        public virtual ICollection<EngineSubCategory> SubCategories { get; set; } = new List<EngineSubCategory>();
    }
}