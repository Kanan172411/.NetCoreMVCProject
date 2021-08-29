using EduHomeBackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.ViewModels
{
    public class AboutViewModel
    {
        public List<Teacher> teachers { get; set; }
        public List<NoticeBoard> noticeBoards { get; set; }
        public Setting settings { get; set; }
    }
}
