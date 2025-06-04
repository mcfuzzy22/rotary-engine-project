// In Data/Models/ForumThread.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace rotaryproject.Data.Models
{
    public class ForumThread
    {
        [Key]
        public int ThreadId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string UserId { get; set; } = string.Empty; // FK to ApplicationUser (AspNetUsers.Id)

        [Required]
        public int ForumCategoryId { get; set; } // FK to ForumCategory

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastPostAt { get; set; } // When the latest reply was made
        public int ViewCount { get; set; } = 0;
        public bool IsLocked { get; set; } = false;
        public bool IsPinned { get; set; } = false; // For sticky threads

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        [ForeignKey("ForumCategoryId")]
        public virtual ForumCategory? ForumCategory { get; set; }

        public virtual ICollection<ForumPost> Posts { get; set; } = new List<ForumPost>();
    }
}