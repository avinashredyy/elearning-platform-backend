using ELearning.Core.Entities;

namespace ELearning.Core.Interfaces
{
    /// <summary>
    /// Repository interface for Course entity operations
    /// Defines the contract for data access operations
    /// </summary>
    public interface ICourseRepository
    {
        // Read operations - all return IEnumerable for maximum flexibility
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Course?> GetCourseByIdAsync(int id);
        Task<IEnumerable<Course>> GetCoursesByCategoryAsync(string category);
        Task<IEnumerable<Course>> GetPublishedCoursesAsync();

        // Write operations
        Task<Course> CreateCourseAsync(Course course);
        Task<Course?> UpdateCourseAsync(Course course);
        Task<bool> DeleteCourseAsync(int id);

        // Utility operations
        Task<bool> CourseExistsAsync(int id);
        Task<int> GetTotalCoursesCountAsync();
    }
}