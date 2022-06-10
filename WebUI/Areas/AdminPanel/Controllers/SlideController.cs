using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using WebUI.DAL;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SlideController : Controller
    {

        private AppDbContext _context { get; }
        private IWebHostEnvironment _env { get; }
        public SlideController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Slides);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slide slide)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!slide.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image size must be smaller than 200kb");
                return View();
            }
            if (!slide.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Type of file  must be image");
                return View();
            }

            slide.Url = await slide.Photo.SaveFileAsync(_env.WebRootPath, "img");
            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var slider = _context.Slides.Find(id);
            if (slider == null)
            {
                return NotFound();
            }
            var path = Helper.GetPath(_env.WebRootPath, "img", slider.Url);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Slides.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


        }


        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var slider = _context.Slides.Find(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Slide slide)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var DBslide = _context.Slides.Find(id);
            if (DBslide == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!slide.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image size must be smaller than 200kb");
                return View();
            }
            if (!slide.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Type of file  must be image");
                return View();
            }
            slide.Url = await slide.Photo.SaveFileAsync(_env.WebRootPath, "img");
            var path = Helper.GetPath(_env.WebRootPath, "img", DBslide.Url);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            DBslide.Url = slide.Url;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
}
 