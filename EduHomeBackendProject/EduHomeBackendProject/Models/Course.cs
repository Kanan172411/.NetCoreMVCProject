using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:50)]
        public string CourseName { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string CoursePosterContent { get; set; }
        [Required]
        [StringLength(maximumLength: 1500)]
        public string CourseContent { get; set; }
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength: 1000)]
        public string AboutCourse { get; set; }
        [StringLength(maximumLength: 1000)]
        [Required]
        public string HowToApply { get; set; }
        [StringLength(maximumLength: 1000)]
        [Required]
        public string Certification { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public int ClassDuration { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Language { get; set; }
        [Required]
        public int StudentsCount { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int CourseCategoryId { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public List<int> TagId { get; set; }
        public CourseCategory courseCategory { get; set; }
        public List<CourseTag> courseTags { get; set; }
        public List<JoinedUser> JoinedUsers { get; set; }
        public List<CourseMessages> CourseMessages { get; set; }

    }
}
