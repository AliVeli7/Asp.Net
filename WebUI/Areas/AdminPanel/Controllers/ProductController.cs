using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebUI.DAL;
using WebUI.Models;

namespace WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        private AppDbContext _context { get; }

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show(Category Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            var product = _context.Categories.Find(Id);
            if (product == null)
            {
                return NotFound();
            }
            return View(_context.Products);
        }
    }
}
