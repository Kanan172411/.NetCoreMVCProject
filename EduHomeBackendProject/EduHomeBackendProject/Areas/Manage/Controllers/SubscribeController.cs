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
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class SubscribeController : Controller
    {
        private readonly AppDbContext _context;
        public SubscribeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.subsribedUsers.Count() / 2d);
            List<SubsribedUsers> subsribedUsers = _context.subsribedUsers.Skip((page - 1) * 2).Take(2).ToList();
            return View(subsribedUsers);
        }
    }
}
