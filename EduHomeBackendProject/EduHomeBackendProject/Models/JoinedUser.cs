using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Models
{
    public class JoinedUser
    {
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string AppUserId { get; set; }
        [Required]
        public DateTime JoinedAt { get; set; }

        public bool? Status { get; set; }
        public Course course { get; set; }
        public AppUser appUser { get; set; }
    }
}
