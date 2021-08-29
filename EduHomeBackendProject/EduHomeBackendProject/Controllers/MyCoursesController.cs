using EduHomeBackendProject.DAL;
using EduHomeBackendProject.Models;
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
    [Authorize(Roles = "Member")]
    public class MyCoursesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public MyCoursesController(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IActionResult> Index(int page=1)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user==null)
            {
                return RedirectToAction("error", "home");
            }
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.JoinedUsers.Where(x => x.AppUserId == user.Id).Count() / 2d);
            var mycourses = _context.JoinedUsers.Include(x=>x.course).Where(x => x.AppUserId == user.Id).Skip((page - 1) * 2).Take(2).ToList();
            return View(mycourses);
        }
    }
}
