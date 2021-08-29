using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Models
{
    public class AppUser:IdentityUser
    {
        public bool IsAdmin { get; set; }
    }
}
