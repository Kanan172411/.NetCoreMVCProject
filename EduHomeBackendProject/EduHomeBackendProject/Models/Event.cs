using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:50)]
        public string EventName { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Venue { get; set; }
        [Required]
        [StringLength(maximumLength: 1500)]
        public string EventContent { get; set; }
        [StringLength(maximumLength: 100)]
        public string Image { get; set; }
        [Required]
        public DateTime StartDateDayMonth { get; set; }
        [Required]
        public int EventCategoryId { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public List<int> TeacherId { get; set; }
        public EventCategory eventCategory { get; set; }
        public List<EventTeachers> EventTeachers { get; set; }
        public List<EventMessages> EventMessages { get; set; }
    }
}
