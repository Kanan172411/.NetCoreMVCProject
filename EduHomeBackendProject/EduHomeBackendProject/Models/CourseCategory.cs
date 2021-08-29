using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Models
{
    public class CourseCategory
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:30)]
        public string Name { get; set; }
        public List<Course> course { get; set; }
    }
}
