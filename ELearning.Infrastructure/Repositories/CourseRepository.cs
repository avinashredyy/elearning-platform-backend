using ELearning.Core.Entities;
using ELearning.Core.Interfaces;
using ELearning.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ELearning.Infrastructure.Repositories
{
    /// <summary>
    /// Implementation of course repository using Entity Framework Core
    /// </summary>
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CourseRepository> _logger;

        public CourseRepository(ApplicationDbContext context, ILogger<CourseRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            _logger.LogInformation("Retrieving all courses from database");

            return await _context.Courses
                .OrderBy(c => c.Title)
                .ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(int id)
        {
            _logger.LogInformation("Retrieving course with ID: {CourseId}", id);

            return await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<String>> GetCategoriesAsync()
        {

            return await _context.Courses
                .OrderBy(c => c.Category)
                .Select(c => c.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesByCategoryAsync(string category)
        {
            _logger.LogInformation("Retrieving courses for category: {Category}", category);

            return await _context.Courses
                .Where(c => c.Category.ToLower() == category.ToLower())
                .OrderBy(c => c.Title)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetPublishedCoursesAsync()
        {
            _logger.LogInformation("Retrieving all published courses");

            return await _context.Courses
                .Where(c => c.IsPublished)
                .OrderBy(c => c.Title)
                .ToListAsync();
        }

        public async Task<Course> CreateCourseAsync(Course course)
        {
            _logger.LogInformation("Creating new course: {CourseTitle}", course.Title);

            // Set timestamps
            course.CreatedAt = DateTime.UtcNow;
            course.UpdatedAt = DateTime.UtcNow;

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Course created successfully with ID: {CourseId}", course.Id);
            return course;
        }

        public async Task<Course?> UpdateCourseAsync(Course course)
        {
            _logger.LogInformation("Updating course with ID: {CourseId}", course.Id);

            var existingCourse = await GetCourseByIdAsync(course.Id);
            if (existingCourse == null)
            {
                _logger.LogWarning("Course with ID {CourseId} not found for update", course.Id);
                return null;
            }

            // Update properties
            existingCourse.Title = course.Title;
            existingCourse.Description = course.Description;
            existingCourse.Price = course.Price;
            existingCourse.DurationInHours = course.DurationInHours;
            existingCourse.Level = course.Level;
            existingCourse.Category = course.Category;
            existingCourse.IsPublished = course.IsPublished;
            existingCourse.InstructorId = course.InstructorId;
            existingCourse.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Course updated successfully: {CourseId}", course.Id);
            return existingCourse;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            _logger.LogInformation("Attempting to delete course with ID: {CourseId}", id);

            var course = await GetCourseByIdAsync(id);
            if (course == null)
            {
                _logger.LogWarning("Course with ID {CourseId} not found for deletion", id);
                return false;
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Course deleted successfully: {CourseId}", id);
            return true;
        }

        public async Task<bool> CourseExistsAsync(int id)
        {
            return await _context.Courses.AnyAsync(c => c.Id == id);
        }

        public async Task<int> GetTotalCoursesCountAsync()
        {
            return await _context.Courses.CountAsync();
        }
    }
}