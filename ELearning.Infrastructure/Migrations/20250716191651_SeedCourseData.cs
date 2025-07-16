using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ELearning.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedCourseData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "DurationInHours", "InstructorId", "IsPublished", "Level", "Price", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Programming", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learn the fundamentals of C# programming language including variables, data types, control structures, and object-oriented programming concepts.", 20, 1, true, "Beginner", 99.99m, "Introduction to C# Programming", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Web Development", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master advanced ASP.NET Core concepts including middleware, dependency injection, authentication, and building scalable web APIs.", 35, 1, true, "Beginner", 149.99m, "Advanced ASP.NET Core Development", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Frontend", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Build modern web applications using Angular framework with TypeScript, components, services, and reactive programming.", 30, 2, false, "Beginner", 129.99m, "Angular Frontend Development", new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
