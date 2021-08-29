using EduHomeBackendProject.DAL;
using EduHomeBackendProject.Models;
using EduHomeBackendProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CourseController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int? categoryId,int? Tagid,int? CourseId, string search, int page=1)
        {
            var queryCourseTag = _context.CourseTags.AsQueryable();
            var query = _context.Courses.AsQueryable();
            if (search != null)
            {
                ViewBag.search = search;
                query = query.Where(x => x.CourseName.ToLower().Contains(search.ToLower()));
            }
            if (Tagid != null)
            {
                var querynew = queryCourseTag.Where(x => x.TagId == Tagid).ToList();
                List<Course> courses1 = new List<Course>();
                foreach (var item in querynew)
                {
                    courses1.Add(_context.Courses.Find(item.CourseId));
                }
                query = courses1.AsQueryable();
            }
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(query.Count() / 3d);
            List<Course> courses = query.Skip((page - 1) * 3).Take(3).ToList();
            return View(courses);
        }
        public IActionResult Details(int? id)
        {
            CourseCategory  courseCategory = _context.CourseCategories.Where(x => x.Id == id).FirstOrDefault();

            if (courseCategory==null)
            {
                return RedirectToAction("error", "home");
            }

            CourseDetailsViewModel courseDetailsVM = new CourseDetailsViewModel
            {
                CourseCategories = _context.CourseCategories.Include(x=>x.course).ToList(),
                course = _context.Courses
                .Where(x => x.Id == id)
                .Include(x=>x.courseTags)
                .ThenInclude(x=>x.Tag).FirstOrDefault()
            };
            return View(courseDetailsVM);
        }
        public IActionResult Search(string search)
        {
            List<Course> courses = _context.Courses.Where(x => x.CourseName.ToLower().Contains(search.ToLower())).ToList();
            return PartialView("_SearchPartial", courses);
        }

        [Authorize]
        public async Task<IActionResult> Join (int CourseId)
        {

            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null || user.IsAdmin == true)
            {
                return RedirectToAction("login", "account");
            }
            if (!ModelState.IsValid)  return RedirectToAction("details", "course", new { id = CourseId });

            Course course = _context.Courses.Include(x => x.JoinedUsers).FirstOrDefault(x => x.Id == CourseId);
            if (course == null)
                return NotFound();

            if (_context.JoinedUsers.Any(x => x.CourseId == CourseId && x.AppUserId == user.Id))
            {
                TempData["Error"] = "Siz bu kursa təkrar qosula bilməzsiniz!";
                return RedirectToAction("details", "course", new { id = CourseId });
            }

            JoinedUser joined = new JoinedUser
            {
                CourseId = CourseId,
                AppUserId = user.Id,
                JoinedAt = DateTime.UtcNow
            };

            course.JoinedUsers.Add(joined);
            _context.SaveChanges();

            return RedirectToAction("details", "course", new { id = CourseId });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(CourseMessages courseMessages,int courseId)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null || user.IsAdmin == true)
            {
                return RedirectToAction("login", "account");
            }
            if (!ModelState.IsValid) return RedirectToAction("details", "course", new { id = courseId });

            Course course = _context.Courses.FirstOrDefault(x => x.Id == courseId);
            if (course==null)
            {
                return RedirectToAction("error", "home");
            }

            CourseMessages courseMessages1 = new CourseMessages
            {
                Subject = courseMessages.Subject,
                Message = courseMessages.Message,
                AppUserId = user.Id,
                CourseId = courseId,
                SendedAt = DateTime.UtcNow
            };

            _context.CourseMessages.Add(courseMessages1);
            _context.SaveChanges();
            return RedirectToAction("details", "course", new { id = courseId });
        }


    }
}
