// In Data/Models/ForumCategory.cs
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace rotaryproject.Data.Models
{
    public class ForumCategory
    {
        [Key]
        public int ForumCategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }

        public int DisplayOrder { get; set; } = 0; // For ordering categories on the index page

        // Navigation property for threads within this category
        public virtual ICollection<ForumThread> Threads { get; set; } = new List<ForumThread>();
    }
}