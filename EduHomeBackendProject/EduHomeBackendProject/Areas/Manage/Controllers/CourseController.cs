using EduHomeBackendProject.DAL;
using EduHomeBackendProject.Helpers;
using EduHomeBackendProject.Models;
using EduHomeBackendProject.Services;
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
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IEmailService _emailService;
        private readonly UserManager<AppUser> _usermanager;

        public CourseController(AppDbContext context, IWebHostEnvironment env,IEmailService emailService, UserManager<AppUser> userManager )
        {
            _context = context;
            _env = env;
            _emailService = emailService;
            _usermanager = userManager;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.Courses.Count() / 2d);
            List<Course> courses = _context.Courses
                .Include(x => x.courseCategory)
                .Include(x=>x.CourseMessages)
                .Include(x=>x.JoinedUsers)
                .Skip((page - 1) * 2).Take(2).ToList();
            return View(courses);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _context.CourseCategories.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            ViewBag.Categories = _context.CourseCategories.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!_context.CourseCategories.Any(x => x.Id == course.CourseCategoryId))
            {
                ModelState.AddModelError("CourseCategoryId", "Cateqoriya mövcud deyil!");
                return View();
            }

            if (course.ImageFile != null)
            {
                if (course.ImageFile.ContentType != "image/jpeg" && course.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Fayl   .jpg ve ya   .png ola biler!");
                    return View();
                }

                if (course.ImageFile.Length > 3145728)
                {
                    ModelState.AddModelError("ImageFile", "Fayl olcusu 3mb-dan boyuk ola bilmez!");
                    return View();
                }

                course.Image = FileManager.Save(_env.WebRootPath, "img/course", course.ImageFile);
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Image yuklemek mecburidir!");
                return View();
            }
            course.courseTags = new List<CourseTag>();

            if (course.TagId != null)
            {
                foreach (var tagId in course.TagId)
                {
                    CourseTag courseTag = new CourseTag
                    {
                        TagId = tagId
                    };
                    course.courseTags.Add(courseTag);
                }
            }
            _context.Courses.Add(course);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Course course = _context.Courses.Include(x => x.courseCategory).Include(x => x.courseTags).FirstOrDefault(x => x.Id == id);
            if (course == null) return RedirectToAction("error", "dashboard");
            course.TagId = course.courseTags.Select(x => x.TagId).ToList();


            ViewBag.Categories = _context.CourseCategories.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            Course existCourse = _context.Courses.Include(x => x.courseCategory).Include(x => x.courseTags).FirstOrDefault(x => x.Id == course.Id);

            if (existCourse == null) return RedirectToAction("error", "dashboard");

            if (!_context.CourseCategories.Any(x => x.Id == course.CourseCategoryId))
            {
                ModelState.AddModelError("CourseCategoryId", "Cateqoriya mövcud deyil!");
                return View();
            }

            if (course.ImageFile != null)
            {
                if (course.ImageFile.ContentType != "image/jpeg" && course.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Fayl   .jpg ve ya   .png ola biler!");
                    return View();
                }

                if (course.ImageFile.Length > 3145728)
                {
                    ModelState.AddModelError("ImageFile", "Fayl olcusu 3mb-dan boyuk ola bilmez!");
                    return View();
                }

                string newFileName = FileManager.Save(_env.WebRootPath, "img/course", course.ImageFile);

                if (!string.IsNullOrWhiteSpace(existCourse.Image))
                {
                    string oldFilePath = Path.Combine(_env.WebRootPath, "img/course", existCourse.Image);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                existCourse.Image = newFileName;
            }

            existCourse.CourseName = course.CourseName;
            existCourse.CoursePosterContent = course.CoursePosterContent;
            existCourse.CourseContent = course.CourseContent;
            existCourse.AboutCourse = course.AboutCourse;
            existCourse.CourseContent = course.CourseContent;
            existCourse.HowToApply = course.HowToApply;
            existCourse.Certification = course.Certification;
            existCourse.StartDate = course.StartDate;
            existCourse.Duration = course.Duration;
            existCourse.Language = course.Language;
            existCourse.StudentsCount = course.StudentsCount;
            existCourse.Price = course.Price;
            existCourse.ClassDuration = course.ClassDuration;
            existCourse.CourseCategoryId = course.CourseCategoryId;

            existCourse.courseTags.RemoveAll(x => !course.TagId.Contains(x.CourseId));

            if (course.TagId != null)
            {
                foreach (var tagId in course.TagId)
                {
                    CourseTag courseTag = existCourse.courseTags.FirstOrDefault(x => x.TagId == tagId);

                    if (courseTag == null)
                    {
                        courseTag = new CourseTag
                        {
                            CourseId = course.Id,
                            TagId = tagId
                        };
                        existCourse.courseTags.Add(courseTag);
                    }
                }
            }


            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Course existCourse = _context.Courses.FirstOrDefault(x => x.Id == id);
            if (existCourse == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            if (!string.IsNullOrWhiteSpace(existCourse.Image))
            {
                FileManager.Delete(_env.WebRootPath, "img/course", existCourse.Image);
            }
            _context.Courses.Remove(existCourse);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public IActionResult Joined (int courseId,int forid, int page = 1)
        {
            ViewBag.courseId = forid; 
            ViewBag.SelectedPage1 = page;
            ViewBag.TotalPage1 = Math.Ceiling(_context.JoinedUsers.Include(x => x.appUser).Where(x => x.CourseId == courseId).Count() / 2d);
            if (!_context.Courses.Any(x => x.Id == courseId)) return RedirectToAction("error","dashboard");

            var joined = _context.JoinedUsers.Include(x => x.appUser).Where(x => x.CourseId == courseId).Skip((page - 1) * 2).Take(2).ToList();

            return View(joined);
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public IActionResult JoinAccept(int joinedId)
        {
            JoinedUser joined = _context.JoinedUsers.Include(x=>x.appUser).Include(x=>x.course).FirstOrDefault(x => x.Id == joinedId);

            if (joined == null) return RedirectToAction("error", "dashboard");

            joined.Status = true;
            _context.SaveChanges();
            _emailService.Send(joined.appUser.Email, "Kursa qatilma isteyiniz qebul edildi", "Isteyiniz qebul edildi, Kurs:" + joined.course.CourseName);
            Course course = _context.Courses.Include(x => x.JoinedUsers).FirstOrDefault(x => x.Id == joined.CourseId);
            return RedirectToAction("Joined", new { forid = course.Id, courseId = course.Id });
        }

        [Authorize(Roles = "SuperAdmin, Admin")]
        public IActionResult JoinReject(int joinedId)
        {
            JoinedUser joined = _context.JoinedUsers.Include(x => x.appUser).Include(x => x.course).FirstOrDefault(x => x.Id == joinedId);

            if (joined == null) return RedirectToAction("error", "dashboard");

            joined.Status = false;
            _context.SaveChanges();
            _emailService.Send(joined.appUser.Email, "Kursa qatilma isteyiniz redd edildi", "Kursa qatilma isteyiniz redd edildi, Kurs:" + joined.course.CourseName);
            Course course = _context.Courses.Include(x => x.JoinedUsers).FirstOrDefault(x => x.Id == joined.CourseId);
            return RedirectToAction("Joined", new { forid = course.Id, courseId = course.Id });
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public  IActionResult Messages(int courseId,int page=1)
        {
            ViewBag.courseId = courseId;
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.CourseMessages.Where(x=>x.CourseId==courseId).Count() / 2d);
            List<CourseMessages> courseMessages = _context.CourseMessages.Include(x=>x.Course).Include(x=>x.appUser).Where(x => x.CourseId == courseId).Skip((page - 1) * 2).Take(2).ToList();
            if (courseMessages==null)
            {
                return RedirectToAction("error", "dashboard");
            }
            return View(courseMessages);
        }
        [Authorize(Roles = "SuperAdmin, Admin")]
        public IActionResult MessageDetails(int messageId)
        {
            CourseMessages courseMessages = _context.CourseMessages.Include(x => x.Course).Include(x => x.appUser).FirstOrDefault(x => x.Id == messageId);
            if(courseMessages==null) return RedirectToAction("error", "dashboard");

            return View(courseMessages);
        }
    }
}
