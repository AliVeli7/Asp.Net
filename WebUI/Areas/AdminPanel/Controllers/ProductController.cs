using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.DAL;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        private AppDbContext _context { get; }
        private IEnumerable<Category> categories;
        private IEnumerable<Product> products;
        private IWebHostEnvironment _env { get; }


        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            categories = _context.Categories.Where(ct => !ct.IsDeleted);
            _env = env;
            products = _context.Products.Where(pr => !pr.isDeleted);
        }
        public IActionResult Index()
        {
            return View(categories);
        }

        public IActionResult ShowById(int? Id)
        {
            products = _context.Products.Where(pr => pr.Id == Id&&pr.isDeleted==false);
            return View(products);
        }

        public IActionResult Show(int Id)
        {
            products = _context.Products.Where(pr => pr.CategoryId == Id && pr.isDeleted == false);
            return View(products);
        }
        public IActionResult Create()
        {
            ViewBag.categories = _context.Categories.ToList() ;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, int categoryId)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!product.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image size must be smaller than 200kb");
                return View();
            }
            if (!product.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Type of file  must be image");
                return View();
            }
            Product NewProduct = new Product
            {
                Title = product.Title,
                Count=product.Count,
                Price=product.Price,
                CategoryId = categoryId
            };
            await _context.Products.AddAsync(NewProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();
            Product productDb = _context.Products.Where(c =>!c.isDeleted).FirstOrDefault(c => c.Id == id);
            if (productDb == null)
                return NotFound();
            // _context.Products.Remove(ProductDb);
            productDb.isDeleted = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.categories = _context.Categories.ToList();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Product newProduct,int categoryId)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var ProductDb = _context.Products.Find(id);
            if (ProductDb == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!newProduct.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image size must be smaller than 200kb");
                return View();
            }
            if (!newProduct.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Type of file  must be image");
                return View();
            }
            Product product = new Product
            {
                CategoryId = categoryId,
                Title = newProduct.Title,
                Count = newProduct.Count,
                Price = newProduct.Price
            };
            Product productDb = _context.Products.Where(c => !c.isDeleted).FirstOrDefault(c => c.Id == id);
            productDb.isDeleted = true;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
