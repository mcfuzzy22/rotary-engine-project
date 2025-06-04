// In Data/Models/ForumPost.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rotaryproject.Data.Models
{
    public class ForumPost
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        public int ThreadId { get; set; } // FK to ForumThread

        [Required]
        public string UserId { get; set; } = string.Empty; // FK to ApplicationUser

        [Required]
        public string Content { get; set; } = string.Empty; // The actual post content (can be long)

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedAt { get; set; }

        // Navigation properties
        [ForeignKey("ThreadId")]
        public virtual ForumThread? Thread { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }
    }
}