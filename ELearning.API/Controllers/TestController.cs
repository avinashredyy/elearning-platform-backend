using ELearning.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ELearning.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("database-status")]
        public async Task<IActionResult> GetDatabaseStatus()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();
                var courseCount = await _context.Courses.CountAsync();

                return Ok(new
                {
                    Status = "Database connection successful",
                    CanConnect = canConnect,
                    CourseCount = courseCount,
                    Timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Database connection failed", Error = ex.Message });
            }
        }

        [HttpGet("sample-courses")]
        public async Task<IActionResult> GetSampleCourses()
        {
            try
            {
                var courses = await _context.Courses
                    .Select(c => new { c.Id, c.Title, c.Category, c.Price, c.IsPublished })
                    .ToListAsync();

                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}