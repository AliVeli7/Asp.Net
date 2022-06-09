using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.DAL;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context { get; }
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel home = new HomeViewModel
            {
                Slides = _context.Slides.ToList(),
                Categories = _context.Categories.Where(c => !c.IsDeleted).ToList(),
                Summary = _context.Summary.FirstOrDefault(),
                Products = _context.Products.Where(c => !c.isDeleted)
                .Include(p => p.Images).Include(p => p.Category).ToList(),
            };
            return View(home);
        }
    }
}
