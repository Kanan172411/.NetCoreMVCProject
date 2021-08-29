using EduHomeBackendProject.Areas.Manage.ViewModels;
using EduHomeBackendProject.DAL;
using EduHomeBackendProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBackendProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin, Admin, Editor")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var query = _context.JoinedUsers.Include(x=>x.course).AsQueryable();
            List<JoinedUser> joinedUsers = query.Where(x=>x.Status==true).ToList();
            List<JoinedUser> joinedUsers1 = query.Where(x => x.JoinedAt > DateTime.UtcNow.AddMonths(-1) && x.Status==true).ToList();
            List<JoinedUser> joinedUsers2 = query.Where(x=>x.Status == null).Include(x => x.course).ToList();
            List<JoinedUser> joinedUsers3 = query.Where(x=>x.Status == true).Include(x => x.course).ToList();

            int totalEarning = 0;

            for (int i = 0; i < joinedUsers.Count; i++)
            {
                totalEarning += joinedUsers[i].course.Price;
            }

            ViewBag.TotalEarnings = totalEarning;

            int monthlyEarnings = 0;
            for (int i = 0; i < joinedUsers1.Count; i++)
            {
                monthlyEarnings += joinedUsers1[i].course.Price;
            }

            ViewBag.MonthlyEarning = monthlyEarnings;

            ViewBag.Pendings = joinedUsers2.Count();

            ViewBag.AcceptedPercent = joinedUsers3.Count * 100 / query.Count();

            List<CourseCategory> courseCategories = _context.CourseCategories.Include(x => x.course).ToList();

            DashboardViewModel dashboardViewModel = new DashboardViewModel()
            {
                courseCategories = _context.CourseCategories.ToList(),
            };

            List<SimpleReportViewModel> simpleReportViewModels = new List<SimpleReportViewModel>();
            for (int i=0; i<courseCategories.Count; i++)
            {
                simpleReportViewModels.Add(new SimpleReportViewModel
                {
                    DimensionOne = courseCategories[i].Name,
                    Quantity = courseCategories[i].course.Count()
                });
            }
            dashboardViewModel.simpleReportViewModels = simpleReportViewModels;

            return View(dashboardViewModel);
        }
        public IActionResult Error()
        {
            return View();
        }

    }
}
