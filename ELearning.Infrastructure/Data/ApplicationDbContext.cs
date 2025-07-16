using Microsoft.EntityFrameworkCore;
using ELearning.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearning.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Course entity
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                // Add other property configurations
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            // Seed sample data
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Title = "Introduction to C# Programming",
                    Description = "Learn the fundamentals of C# programming language including variables, data types, control structures, and object-oriented programming concepts.",
                    Price = 99.99m,
                    DurationInHours = 20,
                    Category = "Programming",
                    IsPublished = true,
                    CreatedAt = new DateTime(2024, 1, 1),
                    UpdatedAt = new DateTime(2024, 1, 1),
                    InstructorId = 1
                },
                new Course
                {
                    Id = 2,
                    Title = "Advanced ASP.NET Core Development",
                    Description = "Master advanced ASP.NET Core concepts including middleware, dependency injection, authentication, and building scalable web APIs.",
                    Price = 149.99m,
                    DurationInHours = 35,
                    Category = "Web Development",
                    IsPublished = true,
                    CreatedAt = new DateTime(2024, 1, 15),
                    UpdatedAt = new DateTime(2024, 1, 15),
                    InstructorId = 1
                },
                new Course
                {
                    Id = 3,
                    Title = "Angular Frontend Development",
                    Description = "Build modern web applications using Angular framework with TypeScript, components, services, and reactive programming.",
                    Price = 129.99m,
                    DurationInHours = 30,
                    Category = "Frontend",
                    IsPublished = false,
                    CreatedAt = new DateTime(2024, 2, 1),
                    UpdatedAt = new DateTime(2024, 2, 1),
                    InstructorId = 2
                }
            );
        }
    }
}
