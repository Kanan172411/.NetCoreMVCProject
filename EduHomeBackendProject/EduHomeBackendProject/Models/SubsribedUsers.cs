using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Models
{
    public class SubsribedUsers
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
