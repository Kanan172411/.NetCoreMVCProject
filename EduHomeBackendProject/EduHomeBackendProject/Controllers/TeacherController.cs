using EduHomeBackendProject.DAL;
using EduHomeBackendProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Controllers
{
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.Teachers.Count() / 4d);
            List<Teacher> teachers = _context.Teachers.Skip((page - 1) * 4).Take(4).ToList();
            return View(teachers);
        }
        public IActionResult Details(int? id)
        {
            Teacher teacher = _context.Teachers.FirstOrDefault(x => x.Id == id);
            if (teacher==null)
            {
                return RedirectToAction("error", "home");
            }
            return View(teacher);
        }
    }
}
