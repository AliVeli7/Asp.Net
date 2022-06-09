﻿using System;
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

        public ProductController(AppDbContext context)
        {
            _context = context;
            categories = _context.Categories.Where(ct => !ct.IsDeleted);
        }
        public IActionResult Index()
        {
            return View(categories);
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
