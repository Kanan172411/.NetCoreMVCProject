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
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            Setting settings = _context.Settings.FirstOrDefault();
            return View(settings);
        }
        public IActionResult Edit(int id)
        {

            Setting setting = _context.Settings.FirstOrDefault(x => x.Id == id);

            if (setting == null) return RedirectToAction("error", "dashboard");

            return View(setting);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Setting existsetting = _context.Settings.FirstOrDefault(x => x.Id == setting.Id);

            if (existsetting == null) return RedirectToAction("error", "dashboard");

            existsetting.Address = setting.Address;
            existsetting.ContactPhone1 = setting.ContactPhone1;
            existsetting.Email = setting.Email;
            existsetting.BannerinAboutText = setting.BannerinAboutText;
            existsetting.ContactPhone2 = setting.ContactPhone2;
            existsetting.FirstBannerTextPart = setting.FirstBannerTextPart;
            existsetting.FooterDescription = setting.FooterDescription;
            existsetting.SecondBannerText = setting.SecondBannerText;
            existsetting.Website = setting.Website;

            if (setting.BannerImageFile != null)
            {
                if (setting.BannerImageFile.ContentType != "image/jpeg" && setting.BannerImageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("BannerImageFile", "Fayl   .jpg ve ya   .png ola biler!");
                    return View();
                }

                if (setting.BannerImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("BannerImageFile", "Fayl olcusu 2mb-dan boyuk ola bilmez!");
                    return View();
                }
                string newFileName = FileManager.Save(_env.WebRootPath, "img/about", setting.BannerImageFile);

                if (!string.IsNullOrWhiteSpace(existsetting.BannerImage))
                {
                    string oldFilePath = Path.Combine(_env.WebRootPath, "img/about", existsetting.BannerImage);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                existsetting.BannerImage = newFileName;
            }
            if (setting.FooterImage != null)
            {
                if (setting.FooterImage.ContentType != "image/jpeg" && setting.FooterImage.ContentType != "image/png")
                {
                    ModelState.AddModelError("FooterImage", "Fayl   .jpg ve ya   .png ola biler!");
                    return View();
                }

                if (setting.FooterImage.Length > 2097152)
                {
                    ModelState.AddModelError("FooterImage", "Fayl olcusu 2mb-dan boyuk ola bilmez!");
                    return View();
                }


                string newFileName = FileManager.Save(_env.WebRootPath, "img/logo", setting.FooterImage);

                if (!string.IsNullOrWhiteSpace(existsetting.FooterLogo))
                {
                    string oldFilePath = Path.Combine(_env.WebRootPath, "img/logo", existsetting.FooterLogo);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                existsetting.FooterLogo = newFileName;
            }
            if (setting.HeaderImage != null)
            {
                if (setting.HeaderImage.ContentType != "image/jpeg" && setting.HeaderImage.ContentType != "image/png")
                {
                    ModelState.AddModelError("HeaderImage", "Fayl   .jpg ve ya   .png ola biler!");
                    return View();
                }

                if (setting.HeaderImage.Length > 2097152)
                {
                    ModelState.AddModelError("HeaderImage", "Fayl olcusu 2mb-dan boyuk ola bilmez!");
                    return View();
                }


                string newFileName = FileManager.Save(_env.WebRootPath, "img/logo", setting.HeaderImage);

                if (!string.IsNullOrWhiteSpace(existsetting.HeaderLogo))
                {
                    string oldFilePath = Path.Combine(_env.WebRootPath, "img/logo", existsetting.HeaderLogo);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                existsetting.HeaderLogo = newFileName;
            }
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
