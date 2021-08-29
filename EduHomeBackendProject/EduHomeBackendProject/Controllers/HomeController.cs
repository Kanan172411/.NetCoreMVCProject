using EduHomeBackendProject.DAL;
using EduHomeBackendProject.Models;
using EduHomeBackendProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            if (_context.Events.Count()%2==0)
            {
                ViewBag.Length = _context.Events.Count();
                ViewBag.Addition = null;
            }
            else
            {
                ViewBag.Length = _context.Events.Count()-1;
                ViewBag.Addition = "Exist";
            }
            HomeViewModel homeVm = new HomeViewModel
            {
                Sliders = _context.Sliders.OrderBy(x => x.Order).ToList(),
                NoticeBoards = _context.NoticeBoards.ToList(),
                Features = _context.Features.ToList(),
                Courses = _context.Courses.Take(3).ToList(),
                Events = _context.Events.ToList(),
                Setting = _context.Setting.FirstOrDefault()
            };
            return View(homeVm);
        }
        public IActionResult About()
        {
            AboutViewModel aboutVM = new AboutViewModel
            {
                settings = _context.Setting.FirstOrDefault(),
                teachers = _context.Teachers.Take(4).ToList(),
                noticeBoards = _context.NoticeBoards.ToList()
            };
            return View(aboutVM);
        }
        public IActionResult Contact()
        {
            Setting setting = _context.Setting.FirstOrDefault();
            return View(setting);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Subscribe(SubsribedUsers subsribedUsers)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("index");
            if (_context.subsribedUsers.Any(x => x.Email.ToUpper() == subsribedUsers.Email.ToUpper()))
            {
                ModelState.AddModelError("", "Bu email artiq abune olub");
                return RedirectToAction("index");
            }
            SubsribedUsers subsribed = new SubsribedUsers
            {
                Email = subsribedUsers.Email
            };
            _context.Add(subsribed);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage (ContactMessages contactMessages)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("contact");
            }
            ContactMessages contactMessages1=new ContactMessages();
            if (!User.Identity.IsAuthenticated || User.IsInRole("Member") == false)
            {
                if (contactMessages.Name == null || string.IsNullOrWhiteSpace(contactMessages.Name))
                {
                    ModelState.AddModelError("", "Name deyeri bos ola bilmez");
                    return RedirectToAction("contact");
                }
                if (contactMessages.Email == null || string.IsNullOrWhiteSpace(contactMessages.Email))
                {
                    ModelState.AddModelError("", "Name deyeri bos ola bilmez");
                    return RedirectToAction("contact");
                }
                contactMessages1.Name = contactMessages.Name;
                contactMessages1.Email = contactMessages.Email;
            }
            else
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                contactMessages1.AppUserId = user.Id;
            }
            contactMessages1.Subject = contactMessages.Subject;
            contactMessages1.Message = contactMessages.Message;
            contactMessages1.SendedAt = DateTime.UtcNow;
            _context.ContactMessages.Add(contactMessages1);
            _context.SaveChanges();
            return RedirectToAction("contact");
        }
    }
}
