using ELearning.Core.Entities;
using ELearning.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ELearning.API.Controllers
{
    /// <summary>
    /// API Controller for managing courses
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ICourseRepository courseRepository, ILogger<CoursesController> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }

        /// <summary>
        /// GET /api/courses - Retrieve all courses
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCourses()
        {
            try
            {
                _logger.LogInformation("Getting all courses");

                var courses = await _courseRepository.GetAllCoursesAsync();

                _logger.LogInformation("Retrieved {CourseCount} courses", courses.Count());
                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all courses");
                return StatusCode(500, "An error occurred while retrieving courses");
            }
        }

        /// <summary>
        /// GET /api/courses/{id} - Retrieve a specific course
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            try
            {
                _logger.LogInformation("Getting course with ID: {CourseId}", id);

                var course = await _courseRepository.GetCourseByIdAsync(id);

                if (course == null)
                {
                    _logger.LogWarning("Course with ID {CourseId} not found", id);
                    return NotFound($"Course with ID {id} was not found");
                }

                return Ok(course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving course {CourseId}", id);
                return StatusCode(500, "An error occurred while retrieving the course");
            }
        }

        /// <summary>
        /// POST /api/courses - Create a new course
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Course>> CreateCourse([FromBody] Course course)
        {
            try
            {
                _logger.LogInformation("Creating new course: {CourseTitle}", course.Title);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Invalid model state for course creation");
                    return BadRequest(ModelState);
                }

                var createdCourse = await _courseRepository.CreateCourseAsync(course);

                _logger.LogInformation("Course created successfully with ID: {CourseId}", createdCourse.Id);

                return CreatedAtAction(
                    nameof(GetCourse),
                    new { id = createdCourse.Id },
                    createdCourse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating course: {CourseTitle}", course.Title);
                return StatusCode(500, "An error occurred while creating the course");
            }
        }

        /// <summary>
        /// PUT /api/courses/{id} - Update an existing course
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<Course>> UpdateCourse(int id, [FromBody] Course course)
        {
            try
            {
                _logger.LogInformation("Updating course with ID: {CourseId}", id);

                if (id != course.Id)
                {
                    _logger.LogWarning("Course ID mismatch. URL ID: {UrlId}, Course ID: {CourseId}", id, course.Id);
                    return BadRequest("Course ID mismatch");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedCourse = await _courseRepository.UpdateCourseAsync(course);

                if (updatedCourse == null)
                {
                    return NotFound($"Course with ID {id} was not found");
                }

                _logger.LogInformation("Course updated successfully: {CourseId}", id);
                return Ok(updatedCourse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating course {CourseId}", id);
                return StatusCode(500, "An error occurred while updating the course");
            }
        }

        /// <summary>
        /// DELETE /api/courses/{id} - Delete a course
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            try
            {
                _logger.LogInformation("Deleting course with ID: {CourseId}", id);

                var deleted = await _courseRepository.DeleteCourseAsync(id);

                if (!deleted)
                {
                    return NotFound($"Course with ID {id} was not found");
                }

                _logger.LogInformation("Course deleted successfully: {CourseId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting course {CourseId}", id);
                return StatusCode(500, "An error occurred while deleting the course");
            }
        }

        /// <summary>
        /// GET /api/courses/category - Get all category
        /// </summary>
        [HttpGet("categories")]
        public async Task<ActionResult<List<Course>>> GetCategories()
        {
            try
            {

                var categories = await _courseRepository.GetCategoriesAsync();

                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving categoreis");
                return StatusCode(500, "An error occurred while retrieving courses");
            }
        }


        /// <summary>
        /// GET /api/courses/category/{category} - Get courses by category
        /// </summary>
        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<Course>>> GetCoursesByCategory(string category)
        {
            try
            {
                _logger.LogInformation("Getting courses for category: {Category}", category);

                var courses = await _courseRepository.GetCoursesByCategoryAsync(category);

                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving courses for category {Category}", category);
                return StatusCode(500, "An error occurred while retrieving courses");
            }
        }

        /// <summary>
        /// GET /api/courses/published - Get only published courses
        /// </summary>
        [HttpGet("published")]
        public async Task<ActionResult<IEnumerable<Course>>> GetPublishedCourses()
        {
            try
            {
                _logger.LogInformation("Getting all published courses");

                var courses = await _courseRepository.GetPublishedCoursesAsync();

                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving published courses");
                return StatusCode(500, "An error occurred while retrieving published courses");
            }
        }
    }
}