using EduHomeBackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.ViewModels
{
    public class CourseDetailsViewModel
    {
        public List<CourseCategory> CourseCategories { get; set; }
        public Course course { get; set; }
    }
}
