using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string FullName { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Profession { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string About { get; set; }
        [StringLength(maximumLength: 50)]
        [Required]
        public string Degree { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Experience { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string ContactInfoMail { get; set; }
        [StringLength(maximumLength: 50)]
        [Required]
        public string ContactInfoSkype { get; set; }
        [StringLength(maximumLength: 50)]
        [Required]
        public string ContactInfoNumber { get; set; }
        [Required]
        [Range(0, 100)]
        public int Language { get; set; }
        [Required]
        [Range(0, 100)]
        public int TeamLeader { get; set; }
        [Required]
        [Range(0, 100)]
        public int Development { get; set; }
        [Required]
        [Range(0, 100)]
        public int Design { get; set; }
        [Required]
        [Range(0, 100)]
        public int Innovation { get; set; }
        [Required]
        [Range(0, 100)]
        public int Communication { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public List<EventTeachers> EventTeachers { get; set; }
    }
}
