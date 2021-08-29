using EduHomeBackendProject.DAL;
using EduHomeBackendProject.Helpers;
using EduHomeBackendProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin, Admin, Editor")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeacherController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.Teachers.Count() / 2d);
            List<Teacher> teachers = _context.Teachers.Skip((page - 1) * 2).Take(2).ToList();
            return View(teachers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (teacher.ImageFile != null)
            {
                if (teacher.ImageFile.ContentType != "image/jpeg" && teacher.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Fayl   .jpg ve ya   .png ola biler!");
                    return View();
                }

                if (teacher.ImageFile.Length > 3145728)
                {
                    ModelState.AddModelError("ImageFile", "Fayl olcusu 3mb-dan boyuk ola bilmez!");
                    return View();
                }

                teacher.Image = FileManager.Save(_env.WebRootPath, "img/teacher", teacher.ImageFile);
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Image yuklemek mecburidir!");
                return View();
            }

            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id)
        {
            Teacher teacher = _context.Teachers.FirstOrDefault(x => x.Id == id);

            if (teacher == null) return RedirectToAction("error", "dashboard");

            return View(teacher);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Teacher existteacher = _context.Teachers.FirstOrDefault(x => x.Id == teacher.Id);

            if (existteacher == null) return RedirectToAction("error", "dashboard");
            if (teacher.ImageFile != null)
            {
                if (teacher.ImageFile.ContentType != "image/jpeg" && teacher.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Fayl   .jpg ve ya   .png ola biler!");
                    return View();
                }

                if (teacher.ImageFile.Length > 3145728)
                {
                    ModelState.AddModelError("ImageFile", "Fayl olcusu 3mb-dan boyuk ola bilmez!");
                    return View();
                }


                string newFileName = FileManager.Save(_env.WebRootPath, "img/teacher", teacher.ImageFile);

                if (!string.IsNullOrWhiteSpace(existteacher.Image))
                {
                    string oldFilePath = Path.Combine(_env.WebRootPath, "img/teacher", existteacher.Image);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                existteacher.Image = newFileName;
            }
            existteacher.FullName = teacher.FullName;
            existteacher.Profession = teacher.Profession;
            existteacher.About = teacher.About;
            existteacher.Degree = teacher.Degree;
            existteacher.Experience = teacher.Experience;
            existteacher.ContactInfoMail = teacher.ContactInfoMail;
            existteacher.ContactInfoSkype = teacher.ContactInfoSkype;
            existteacher.ContactInfoNumber = teacher.ContactInfoNumber;
            existteacher.Language = teacher.Language;
            existteacher.TeamLeader = teacher.TeamLeader;
            existteacher.Development = teacher.Development;
            existteacher.Design = teacher.Design;
            existteacher.Innovation = teacher.Innovation;
            existteacher.Communication = teacher.Communication;
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Teacher existteacher = _context.Teachers.FirstOrDefault(x => x.Id == id);
            if (existteacher == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            if (!string.IsNullOrWhiteSpace(existteacher.Image))
            {
                FileManager.Delete(_env.WebRootPath, "img/teacher", existteacher.Image);
            }
            _context.Teachers.Remove(existteacher);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
