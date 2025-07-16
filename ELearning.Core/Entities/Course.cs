using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearning.Core.Entities
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public  string Description { get; set; } = string.Empty;

        [Range(0, 5000)]
        public decimal Price { get; set; }

        [Range(1, 10000)]  // At least 1 hour
        public int DurationInHours { get; set; }

        public string Level { get; set; } = "Beginner";

        [Required]
        [StringLength(50)]  // Just to prevent accidents
        public string Category { get; set; } = string.Empty;

        public bool IsPublished { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int InstructorId { get; set; }
    }
}
