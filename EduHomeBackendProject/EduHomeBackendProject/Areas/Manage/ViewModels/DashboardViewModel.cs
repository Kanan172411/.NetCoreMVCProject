using EduHomeBackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Areas.Manage.ViewModels
{
    public class DashboardViewModel
    {
        public List<CourseCategory> courseCategories { get; set; }
        public List<SimpleReportViewModel> simpleReportViewModels { get; set; }

    }
}
