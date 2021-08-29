using EduHomeBackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.ViewModels
{
    public class EventViewModel
    {
        public List<EventCategory> eventCategories { get; set; }
        public List<Event> events { get; set; }
    }
}
