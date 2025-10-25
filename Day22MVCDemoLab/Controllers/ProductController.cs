using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCDemoLabpart1.Data;
using MVCDemoLabpart1.Models;

namespace MVCDemoLabpart1.Controllers
{
    public class ProductController : Controller
    {
        private readonly MVCDbContext _context;

        public ProductController(MVCDbContext context)
        {
            this._context = context;
        }

        // GET: ProductController
        public ActionResult Index()
        {
            var products = _context.Products.Include("Category").AsNoTracking().ToList();
            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            //var t = from category in _context.Categories
            //        select new { category.CategoryId , category.Name };
            // or 
            var categories = _context.Categories.AsNoTracking().ToList();
            ViewBag.CategoryId = new SelectList(categories, "CategoryId", "Name"); // Where "CategoryId" is the value field and "Name" is the text field that will be displayed
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = new SelectList(_context.Categories.AsNoTracking().ToList(), "CategoryId", "Name" , product.CategoryId);
                return View(product);
            }
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(product);
            }
        }  
        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var product = _context.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == id);
            ViewBag.CategoryId = new SelectList(_context.Categories.AsNoTracking().ToList(), "CategoryId", "Name", product.CategoryId);
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Products product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CategoryId = new SelectList(_context.Categories.AsNoTracking().ToList(), "CategoryId", "Name", product.CategoryId);
                return View(product);
            }
            try
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var product = _context.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == id);
            return View(product);
        }

        // POST: ProductController/Delete/5
        // POST: WizardProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
