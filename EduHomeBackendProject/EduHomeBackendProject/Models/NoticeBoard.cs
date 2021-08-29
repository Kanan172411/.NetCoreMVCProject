using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Models
{
    public class NoticeBoard
    {
        public int Id { get; set; }
        [Required]
        public DateTime NoticeDate { get; set; }
        [Required]
        [StringLength(maximumLength:500)]
        public string NoticeText { get; set; }
    }
}
