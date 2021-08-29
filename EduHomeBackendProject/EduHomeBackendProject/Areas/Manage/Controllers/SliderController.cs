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
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.SelectedPage = page;
            ViewBag.TotalPage = Math.Ceiling(_context.Sliders.Count() / 2d);
            List<Slider> sliders = _context.Sliders.Skip((page - 1) * 2).Take(2).ToList();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Fayl   .jpg ve ya   .png ola biler!");
                    return View();
                }

                if (slider.ImageFile.Length > 3145728)
                {
                    ModelState.AddModelError("ImageFile", "Fayl olcusu 3mb-dan boyuk ola bilmez!");
                    return View();
                }

                slider.Image = FileManager.Save(_env.WebRootPath, "img/slider", slider.ImageFile);
            }
            else
            {
                ModelState.AddModelError("ImageFile", "Image yuklemek mecburidir!");
                return View();
            }

            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);

            if (slider == null) return RedirectToAction("error", "dashboard");

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Slider existSlider = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);

            if (existSlider == null) return RedirectToAction("error", "dashboard");

            existSlider.Title = slider.Title;
            existSlider.Description = slider.Description;
            existSlider.Order = slider.Order;
            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFile", "Fayl   .jpg ve ya   .png ola biler!");
                    return View();
                }

                if (slider.ImageFile.Length > 3145728)
                {
                    ModelState.AddModelError("ImageFile", "Fayl olcusu 3mb-dan boyuk ola bilmez!");
                    return View();
                }


                string newFileName = FileManager.Save(_env.WebRootPath, "img/slider", slider.ImageFile);

                if (!string.IsNullOrWhiteSpace(existSlider.Image))
                {
                    string oldFilePath = Path.Combine(_env.WebRootPath, "img/slider", existSlider.Image);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }


                existSlider.Image = newFileName;
            }
            _context.SaveChanges();

            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Slider existSlider = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (existSlider == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            if (!string.IsNullOrWhiteSpace(existSlider.Image))
            {
                FileManager.Delete(_env.WebRootPath, "img/slider", existSlider.Image);
            }
            _context.Sliders.Remove(existSlider);
            _context.SaveChanges();

            return Json(new { status = 200 });
        }
    }
}
