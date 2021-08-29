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
    public class NoticeBoardController : Controller
    {
        private readonly AppDbContext _context;

        public NoticeBoardController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.NoticeBoards.Count() / 2d);
            List<NoticeBoard> noticeBoards = _context.NoticeBoards.Skip((page - 1) * 2).Take(2).ToList();
            return View(noticeBoards);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NoticeBoard noticeBoard)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.NoticeBoards.Add(noticeBoard);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            NoticeBoard notice = _context.NoticeBoards.FirstOrDefault(x => x.Id == id);

            if (notice == null) return RedirectToAction("error", "dashboard");

            return View(notice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NoticeBoard notice)
        {
            NoticeBoard existNotice = _context.NoticeBoards.FirstOrDefault(x => x.Id == notice.Id);

            if (existNotice == null) return RedirectToAction("error", "dashboard");

            existNotice.NoticeText = notice.NoticeText;
            existNotice.NoticeDate = notice.NoticeDate;
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            NoticeBoard existNoticeBoard = _context.NoticeBoards.FirstOrDefault(x => x.Id == id);
            if (existNoticeBoard == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            _context.NoticeBoards.Remove(existNoticeBoard);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
