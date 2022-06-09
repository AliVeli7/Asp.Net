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
        private IEnumerable<Category> categories;
        private IEnumerable<Product> products;


        public ProductController(AppDbContext context)
        {
            _context = context;
            categories = _context.Categories.Where(ct => !ct.IsDeleted);
            products = _context.Products.Where(pr => !pr.isDeleted);
        }
        public IActionResult Index()
        {
            return View(categories);
        }

        public IActionResult Show(int? ctId)
        {
            products = _context.Products.Where(pr => pr.CategoryId == ctId);
            return View(products);
        }
    }
}
