using EduHomeBackendProject.DAL;
using EduHomeBackendProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin, Admin, Editor")]
    public class EventCategoryController : Controller
    {
        private readonly AppDbContext _context;

        public EventCategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.EventCategories.Count() / 2d);
            List<EventCategory> eventCategories = _context.EventCategories.Skip((page - 1) * 2).Take(2).ToList();

            return View(eventCategories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EventCategory eventCategories)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.EventCategories.Add(eventCategories);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            EventCategory eventCategory = _context.EventCategories.FirstOrDefault(x => x.Id == id);

            if (eventCategory == null) return RedirectToAction("error", "dashboard");
            return View(eventCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EventCategory eventCategory)
        {
            EventCategory existeventCategory = _context.EventCategories.FirstOrDefault(x => x.Id == eventCategory.Id);

            if (existeventCategory == null) return RedirectToAction("error", "dashboard");

            existeventCategory.Name = eventCategory.Name;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            EventCategory existeventCategory = _context.EventCategories.FirstOrDefault(x => x.Id == id);
            if (existeventCategory == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            _context.EventCategories.Remove(existeventCategory);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
