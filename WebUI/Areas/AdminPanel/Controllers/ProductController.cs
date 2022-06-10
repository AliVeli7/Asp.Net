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

        public IActionResult Show(int Id)
        {
            products = _context.Products.Where(pr => pr.CategoryId == Id&&pr.isDeleted==false);
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            Product productDb = _context.Products.Where(c =>!c.isDeleted).FirstOrDefault(c => c.Id == id);
            if (productDb == null)
                return NotFound();
            // _context.Categories.Remove(categoryDb);
            productDb.isDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
