using EduHomeBackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Sliders { get; set; }
        public List<NoticeBoard> NoticeBoards { get; set; }
        public List<Feature> Features { get; set; }
        public List<Course> Courses { get; set; }
        public List<Event> Events { get; set; }
        public Setting Setting { get; set; }
    }
}
