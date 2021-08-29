using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Models
{
    public class CourseMessages
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:100)]
        public string Subject { get; set; }
        [Required]
        [StringLength(maximumLength: 100)]
        public string Message { get; set; }
        public string AppUserId { get; set; }
        public DateTime SendedAt { get; set; }
        public int CourseId { get; set; }

        public Course Course { get; set; }
        public AppUser appUser { get; set; }
     }
}
