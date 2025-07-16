using Microsoft.EntityFrameworkCore;
using System;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Note> Notes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure TaskItem entity
            modelBuilder.Entity<TaskItem>()
                .Property(t => t.Priority)
                .HasMaxLength(10);
            
            // Seed data for development
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem
                {
                    Id = 1,
                    Title = "Complete Project Plan",
                    Description = "Finalize the project plan for the client meeting",
                    DueDate = DateTime.Now.AddDays(2),
                    Priority = "High",
                    IsCompleted = false
                },
                new TaskItem
                {
                    Id = 2,
                    Title = "Send Weekly Report",
                    Description = "Compile and send the weekly progress report",
                    DueDate = DateTime.Now.AddDays(1),
                    Priority = "Medium",
                    IsCompleted = false
                }
            );
            
            modelBuilder.Entity<Note>().HasData(
                new Note
                {
                    Id = 1,
                    Content = "Remember to schedule a team meeting for project kickoff",
                    CreatedAt = DateTime.Now
                }
            );
        }
    }
}
