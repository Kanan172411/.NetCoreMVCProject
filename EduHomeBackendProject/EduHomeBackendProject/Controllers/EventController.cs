using EduHomeBackendProject.DAL;
using EduHomeBackendProject.Models;
using EduHomeBackendProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public EventController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int? categoryId, string search, int page=1)
        {
            var query = _context.Events.AsQueryable();
            if (categoryId != null)
            {
                query = query.Where(x => x.EventCategoryId == categoryId);
                ViewBag.Id = categoryId; 
            }
            if (search != null)
            {
                ViewBag.search = search;

                query = query.Where(x => x.EventName.ToLower().Contains(search.ToLower()));
                if (categoryId != null)
                {
                    query = query.Where(x => x.EventCategoryId == categoryId);
                }
            }

            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(query.Count() / 2d);
            EventViewModel eventVM = new EventViewModel
            {
                eventCategories = _context.EventCategories.ToList(),
                events = query.Skip((page - 1) * 2).Take(2).ToList()
            };
            return View(eventVM);
        }

        public IActionResult Details(int? id)
        {
            
            Event @event = _context.Events.FirstOrDefault(x => x.Id == id);
            if (@event==null)
            {
                return RedirectToAction("error", "home");
            }
            EventDetailViewModel eventDetailVM = new EventDetailViewModel
            {
                eventCategories = _context.EventCategories.Include(x=>x.events).ToList(),
                _event = _context.Events
                .Where(x => x.Id == id)
                .Include(x => x.eventCategory)
                .Include(x => x.EventTeachers)
                .ThenInclude(x => x.Teacher)
                .FirstOrDefault()
            };
            return View(eventDetailVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(EventMessages eventMessages,int eventId)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("details", "Event", new { id = eventId });
            }
            EventMessages eventMessages1 = new EventMessages();
            if (!User.Identity.IsAuthenticated || User.IsInRole("Member") == false)
            {
                if (eventMessages.Name == null || string.IsNullOrWhiteSpace(eventMessages.Name))
                {
                    ModelState.AddModelError("", "Name deyeri bos ola bilmez");
                    return RedirectToAction("details", "Event", new { id = eventId });
                }
                if (eventMessages.Email == null || string.IsNullOrWhiteSpace(eventMessages.Email))
                {
                    ModelState.AddModelError("", "Name deyeri bos ola bilmez");
                    return RedirectToAction("details", "Event", new { id = eventId });
                }
                eventMessages1.Name = eventMessages.Name;
                eventMessages1.Email = eventMessages.Email;
            }
            else
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                eventMessages1.AppUserId = user.Id;
            }
            eventMessages1.Subject = eventMessages.Subject;
            eventMessages1.Message = eventMessages.Message;
            eventMessages1.SendedAt = DateTime.UtcNow;
            eventMessages1.EventId = eventId;
            _context.EventMessages.Add(eventMessages1);
            _context.SaveChanges();
            return RedirectToAction("details", "Event", new { id = eventId });
        }
    }
}
