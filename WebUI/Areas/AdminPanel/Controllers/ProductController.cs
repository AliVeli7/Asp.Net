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
        private List<Product> GetProducts;


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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Show(int? ctId)
        {
            if (ctId == null)
            {
                return BadRequest();
            }
            var category = _context.Categories.Find(ctId);
            if (category == null)
            {
                return NotFound();
            }
            foreach (var item in products)
            {
                if (item.CategoryId==ctId)
                {
                    GetProducts.Add(item);
                }
            }
            return View(GetProducts);
        }
    }
}
