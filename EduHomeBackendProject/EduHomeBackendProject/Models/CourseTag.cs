using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Models
{
    public class CourseTag
    {
        public int Id { get; set; }
        [Required]
        public int TagId { get; set; }
        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public Tag Tag { get; set; }
    }
}
