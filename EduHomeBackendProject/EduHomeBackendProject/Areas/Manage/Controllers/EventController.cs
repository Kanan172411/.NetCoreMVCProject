using EduHomeBackendProject.DAL;
using EduHomeBackendProject.Helpers;
using EduHomeBackendProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin, Admin, Editor")]
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<AppUser> _userManager;

        public EventController(AppDbContext context, IWebHostEnvironment env, UserManager<AppUser> userManager)
        {
            _context = context;
            _env = env;
            _userManager = userManager;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.Events.Count() / 2d);
            List<Event> events = _context.Events               
                .Include(x => x.eventCategory)
                .Include(x=>x.EventMessages)
                .Skip((page - 1) * 2).Take(2).ToList();
            return View(events);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _context.EventCategories.ToList();
            ViewBag.Teachers = _context.Teachers.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event _event)
        {
            ViewBag.CAtegories = _context.EventCategories.ToList();
            ViewBag.Teachers = _context.Teachers.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!_context.EventCategories.Any(x => x.Id == _event.EventCategoryId))
            {
                ModelState.AddModelError("EventCategoryId", "Cateqoriya mövcud deyil!");
                return View();
            }

            if (_event.ImageFile != null)
            {
                if (_event.ImageFile.ContentType != "image/jpeg" && _event.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Fayl   .jpg ve ya   .png ola biler!");
                    return View();
                }

                if (_event.ImageFile.Length > 3145728)
                {
                    ModelState.AddModelError("ImageFile", "Fayl olcusu 3mb-dan boyuk ola bilmez!");
                    return View();
                }

                _event.Image = FileManager.Save(_env.WebRootPath, "img/event", _event.ImageFile);
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Image yuklemek mecburidir!");
                return View();
            }
            _event.EventTeachers = new List<EventTeachers>();

            if (_event.TeacherId != null)
            {
                foreach (var teacherId in _event.TeacherId)
                {
                    EventTeachers eventTeacher = new EventTeachers
                    {
                        TeacherId = teacherId
                    };
                    _event.EventTeachers.Add(eventTeacher);
                }
            }
            _context.Events.Add(_event);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Event _event = _context.Events.Include(x => x.eventCategory).Include(x => x.EventTeachers).FirstOrDefault(x => x.Id == id);

            if (_event == null) return RedirectToAction("error", "dashboard");
            _event.TeacherId = _event.EventTeachers.Select(x => x.TeacherId).ToList();

            ViewBag.CAtegories = _context.EventCategories.ToList();
            ViewBag.Teachers = _context.Teachers.ToList();

            return View(_event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Event _event)
        {
           
            if (!ModelState.IsValid)
            {
                return View();
            }

            Event existEvent = _context.Events.Include(x => x.eventCategory).Include(x => x.EventTeachers).FirstOrDefault(x => x.Id == _event.Id);

            if (existEvent == null) return RedirectToAction("error", "dashboard");

            if (!_context.EventCategories.Any(x => x.Id == _event.EventCategoryId))
            {
                ModelState.AddModelError("EventCategoryId", "Cateqoriya mövcud deyil!");
                return View();
            }

            if (_event.ImageFile != null)
            {
                if (_event.ImageFile.ContentType != "image/jpeg" && _event.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Fayl   .jpg ve ya   .png ola biler!");
                    return View();
                }

                if (_event.ImageFile.Length > 3145728)
                {
                    ModelState.AddModelError("ImageFile", "Fayl olcusu 3mb-dan boyuk ola bilmez!");
                    return View();
                }

                string newFileName = FileManager.Save(_env.WebRootPath, "img/event", _event.ImageFile);

                if (!string.IsNullOrWhiteSpace(existEvent.Image))
                {
                    string oldFilePath = Path.Combine(_env.WebRootPath, "img/event", existEvent.Image);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                existEvent.Image = newFileName;
            }
            
            existEvent.EventName = _event.EventName;
            existEvent.StartTime = _event.StartTime;
            existEvent.EndTime = _event.EndTime;
            existEvent.Venue = _event.Venue;
            existEvent.EventContent = _event.EventContent;
            existEvent.StartDateDayMonth = _event.StartDateDayMonth;
            existEvent.EventCategoryId = _event.EventCategoryId;

            existEvent.EventTeachers.RemoveAll(x => !_event.TeacherId.Contains(x.EventId));

            if (_event.TeacherId != null)
            {
                foreach (var teacherId in _event.TeacherId)
                {
                    EventTeachers eventTeachers = existEvent.EventTeachers.FirstOrDefault(x => x.TeacherId == teacherId);

                    if (eventTeachers == null)
                    {
                        eventTeachers = new EventTeachers
                        {
                            EventId = _event.Id,
                            TeacherId = teacherId
                        };
                        existEvent.EventTeachers.Add(eventTeachers);
                    }
                }
            }


            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Event existevent = _context.Events.FirstOrDefault(x => x.Id == id);
            if (existevent == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            if (!string.IsNullOrWhiteSpace(existevent.Image))
            {
                FileManager.Delete(_env.WebRootPath, "img/event", existevent.Image);
            }
            _context.Events.Remove(existevent);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public IActionResult Messages(int eventId, int page = 1)
        {
            List<EventMessages> eventMessages = _context.EventMessages.Include(x => x.Event).Include(x => x.appUser).Where(x => x.EventId == eventId).Skip((page - 1) * 2).Take(2).ToList();
            if (eventMessages==null)
            {
                return RedirectToAction("error", "dashboard");
            }
            ViewBag.eventId = eventId;
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.EventMessages.Where(x => x.EventId == eventId).Count() / 2d);

            return View(eventMessages);
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public IActionResult MessageDetails(int messageId)
        {
            EventMessages eventMessages = _context.EventMessages.Include(x => x.Event).Include(x => x.appUser).FirstOrDefault(x => x.Id == messageId);
            if (eventMessages==null)
            {
                return RedirectToAction("error", "dashboard");
            }

            return View(eventMessages);
        }
    }
}
