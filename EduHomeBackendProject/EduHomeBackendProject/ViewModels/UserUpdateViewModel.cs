using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.ViewModels
{
    public class UserUpdateViewModel
    {
        [Required]
        [StringLength(maximumLength: 25)]
        public string UserName { get; set; }

        [Required]
        [StringLength(maximumLength: 100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(maximumLength: 20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(maximumLength: 20)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassowrd { get; set; }

        [StringLength(maximumLength: 20)]
        [DataType(DataType.Password)]
        public string CurrentPassowrd { get; set; }
    }
}
