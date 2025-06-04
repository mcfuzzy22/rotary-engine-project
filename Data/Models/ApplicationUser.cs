// In Data/Models/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;

namespace rotaryproject.Data.Models // Ensure this namespace is correct
{
    public class ApplicationUser : IdentityUser
    {
        // You can add custom properties here later, for example:
        // public string? FirstName { get; set; }
        // public string? LastName { get; set; }
        public virtual ICollection<ForumPost> ForumPosts { get; set; } = new List<ForumPost>();
        public virtual ICollection<ForumThread> ForumThreads { get; set; } = new List<ForumThread>();
        public virtual ICollection<UserSavedBuild> SavedBuilds { get; set; } = new List<UserSavedBuild>();
    }
}