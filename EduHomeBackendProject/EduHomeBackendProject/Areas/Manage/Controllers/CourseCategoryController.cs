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
    public class CourseCategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CourseCategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.CourseCategories.Count() / 2d);
            List<CourseCategory> courseCategories = _context.CourseCategories.Skip((page - 1) * 2).Take(2).ToList();

            return View(courseCategories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CourseCategory courseCategories)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.CourseCategories.Add(courseCategories);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            CourseCategory courseCategory = _context.CourseCategories.FirstOrDefault(x => x.Id == id);

            if (courseCategory == null) return RedirectToAction("error", "dashboard");

            return View(courseCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CourseCategory courseCategory)
        {
            CourseCategory existCourseCategory = _context.CourseCategories.FirstOrDefault(x => x.Id == courseCategory.Id);

            if (existCourseCategory == null) return RedirectToAction("error", "dashboard");

            existCourseCategory.Name = courseCategory.Name;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            CourseCategory existCourseCategory = _context.CourseCategories.FirstOrDefault(x => x.Id == id);
            if (existCourseCategory == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            _context.CourseCategories.Remove(existCourseCategory);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
