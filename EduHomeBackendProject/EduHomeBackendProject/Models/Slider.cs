using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [StringLength(maximumLength:100)]
        public string Image { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string Title { get; set; }
        [Required]
        [StringLength(maximumLength: 500)]
        public string Description { get; set; }
        [Required]
        public int Order { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
