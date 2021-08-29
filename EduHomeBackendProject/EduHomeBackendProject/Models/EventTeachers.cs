using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Models
{
    public class EventTeachers
    {
        public int Id { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int EventId { get; set; }
        public Teacher Teacher { get; set; }
        public Event Event { get; set; }
    }
}
