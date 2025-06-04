using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using rotaryproject.Data.Models;
using Microsoft.AspNetCore.Identity; // Add this for IdentityUser (if not using ApplicationUser directly)
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Add this for IdentityDbContext
namespace rotaryproject.Data;

public partial class RotaryEngineDbContext : IdentityDbContext<ApplicationUser> // <<< MODIFIED HERE
{
    public RotaryEngineDbContext()
    {
    }

    public RotaryEngineDbContext(DbContextOptions<RotaryEngineDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<ForumCategory> ForumCategories { get; set; } // <<< ADD THIS
    public virtual DbSet<ForumThread> ForumThreads { get; set; }     // <<< ADD THIS
    public virtual DbSet<ForumPost> ForumPosts { get; set; }         // <<< ADD THIS
    public virtual DbSet<CompatibilityRule> CompatibilityRules { get; set; }

    public virtual DbSet<Part> Parts { get; set; }
    public virtual DbSet<EngineMainCategory> EngineMainCategories { get; set; } // <<< ADD THIS
    public virtual DbSet<EngineSubCategory> EngineSubCategories { get; set; }
    public virtual DbSet<PartCategory> PartCategories { get; set; }

    public virtual DbSet<PartStat> PartStats { get; set; }
    public virtual DbSet<UserSavedBuild> UserSavedBuilds { get; set; }
   // In your RotaryEngineDbContext.cs

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    // Intentionally left blank or only calls base.OnConfiguring if needed.
    // Since Program.cs configures the DbContext with the connection string,
    // we don't need to (and shouldn't) configure it here with a hardcoded string.
    // This will remove the CS1030 warning.

    // If you want to be explicit about not doing anything if already configured:
    // if (!optionsBuilder.IsConfigured)
    // {
    //     // This block would now typically be empty or log an error if somehow not configured by Program.cs,
    //     // but for most cases, an empty method is fine.
    // }
}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CompatibilityRule>(entity =>
        {
            entity.HasKey(e => e.RuleId).HasName("PK__Compatib__110458C2D19B7CFD");

            entity.Property(e => e.IsCompatible).HasDefaultValue(true);

            entity.HasOne(d => d.PartA).WithMany(p => p.CompatibilityRulePartAs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Compatibi__PartA__4316F928");

            entity.HasOne(d => d.PartB).WithMany(p => p.CompatibilityRulePartBs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Compatibi__PartB__440B1D61");
        });

        modelBuilder.Entity<Part>(entity =>
        {
            //entity.HasKey(e => e.PartId).HasName("PK__Parts__7C3F0D3095AB3286");

            //entity.HasOne(/d => d.Category).WithMany(p => p.Parts)
            //.OnDelete(DeleteBehavior.ClientSetNull)
            //.HasConstraintName("FK__Parts__CategoryI__3B75D760");
            entity.HasKey(e => e.PartId);
            entity.HasOne(d => d.SubCategory) // Using the navigation property name in Part.cs
                  .WithMany(p => p.Parts)       // Using the ICollection<Part> in EngineSubCategory.cs
                  .HasForeignKey(d => d.EngineSubCategoryId) // The FK property in Part.cs
                  .OnDelete(DeleteBehavior.Restrict);     
        });

        modelBuilder.Entity<PartCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__PartCate__19093A2B0B380B12");
        });

        modelBuilder.Entity<PartStat>(entity =>
        {
            entity.HasKey(e => e.PartStatId).HasName("PK__PartStat__AFCC32BA505FA52A");

            entity.HasOne(d => d.Part).WithMany(p => p.PartStats).HasConstraintName("FK__PartStats__PartI__3E52440B");
        });
        modelBuilder.Entity<UserSavedBuild>(entity =>
            {
                entity.HasKey(e => e.UserSavedBuildId);

                entity.Property(e => e.BuildName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.BuildConfigurationString).IsRequired(); // Can be long, so no MaxLength by default
                entity.Property(e => e.UserId).IsRequired();

                // Define the relationship to ApplicationUser
                // An ApplicationUser can have many UserSavedBuilds
                // A UserSavedBuild belongs to one ApplicationUser
                entity.HasOne(d => d.User)
                      .WithMany(p => p.SavedBuilds) // Corresponds to ICollection in ApplicationUser
                      .HasForeignKey(d => d.UserId)
                      .OnDelete(DeleteBehavior.Cascade); // Or DeleteBehavior.ClientSetNull if you prefer
            });
            modelBuilder.Entity<EngineMainCategory>(entity =>
        {
            entity.HasKey(e => e.EngineMainCategoryId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Name).IsUnique(); // Main category names should be unique
        });

        // Configuration for EngineSubCategory
        modelBuilder.Entity<EngineSubCategory>(entity =>
        {
            entity.HasKey(e => e.EngineSubCategoryId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

            // Relationship: SubCategory to MainCategory
            entity.HasOne(d => d.MainCategory)
                  .WithMany(p => p.SubCategories)
                  .HasForeignKey(d => d.EngineMainCategoryId)
                  .OnDelete(DeleteBehavior.Cascade); // If a main category is deleted, its subcategories are deleted
            entity.HasMany(e => e.ChildSubCategories)       // An EngineSubCategory has many ChildSubCategories
          .WithOne(e => e.ParentSubCategory)        // Each ChildSubCategory has one ParentSubCategory
          .HasForeignKey(e => e.ParentEngineSubCategoryId) // Using ParentEngineSubCategoryId as the FK
          .OnDelete(DeleteBehavior.Restrict); // Or .ClientSetNull or .NoAction, avoid Cascade for self-ref on SQL Server if possible to prevent cycles, or handle carefully.
                                     
        });
            modelBuilder.Entity<ForumCategory>(entity =>
        {
            entity.HasKey(e => e.ForumCategoryId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            // Optional: Add a unique constraint on ForumCategory.Name if needed
            // entity.HasIndex(e => e.Name).IsUnique();
        });

        modelBuilder.Entity<ForumThread>(entity =>
        {
            entity.HasKey(e => e.ThreadId);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.ForumCategoryId).IsRequired();

            // Relationship: Thread to User (one User can have many Threads)
            entity.HasOne(d => d.User)
                  .WithMany(p => p.ForumThreads)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.Restrict); // Or Cascade, or ClientSetNull depending on desired behavior

            // Relationship: Thread to ForumCategory (one Category can have many Threads)
            entity.HasOne(d => d.ForumCategory)
                  .WithMany(p => p.Threads)
                  .HasForeignKey(d => d.ForumCategoryId)
                  .OnDelete(DeleteBehavior.Cascade); // If category is deleted, delete threads
        });

        modelBuilder.Entity<ForumPost>(entity =>
        {
            entity.HasKey(e => e.PostId);
            entity.Property(e => e.Content).IsRequired(); // Content can be long, default max length
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.ThreadId).IsRequired();

            // Relationship: Post to User (one User can have many Posts)
            entity.HasOne(d => d.User)
                  .WithMany(p => p.ForumPosts)
                  .HasForeignKey(d => d.UserId)
                  .OnDelete(DeleteBehavior.Restrict); // Or Cascade, or ClientSetNull

            // Relationship: Post to ForumThread (one Thread can have many Posts)
            entity.HasOne(d => d.Thread)
                  .WithMany(p => p.Posts)
                  .HasForeignKey(d => d.ThreadId)
                  .OnDelete(DeleteBehavior.Cascade); // If thread is deleted, delete posts
        });
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
