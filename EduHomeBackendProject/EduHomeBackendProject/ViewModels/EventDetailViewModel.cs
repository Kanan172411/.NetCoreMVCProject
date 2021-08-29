using EduHomeBackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.ViewModels
{
    public class EventDetailViewModel
    {
        public List<EventCategory> eventCategories { get; set; }
        public Event _event { get; set; }
    }
}
