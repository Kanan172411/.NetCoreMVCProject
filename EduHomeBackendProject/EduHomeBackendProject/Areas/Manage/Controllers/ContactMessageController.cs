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

namespace EduHomeBackendProject.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ContactMessageController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ContactMessageController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.ContactMessages.Count() / 2d);
            List<ContactMessages> contactMessages = _context.ContactMessages.Skip((page - 1) * 2).Take(2).Include(x=>x.appUser).ToList();
            return View(contactMessages);
        }
        public IActionResult Details(int messageId)
        {
            ContactMessages contactMessages = _context.ContactMessages.Include(x => x.appUser).FirstOrDefault(x => x.Id == messageId);
            if(contactMessages==null)
            {
                return RedirectToAction("error", "dashboard");
            }
            return View(contactMessages);
        }
    }
}
