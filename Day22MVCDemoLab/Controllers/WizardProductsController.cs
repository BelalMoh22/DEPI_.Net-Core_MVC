using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCDemoLabpart1.Data;
using MVCDemoLabpart1.Models;

namespace MVCDemoLabpart1.Controllers
{
    public class WizardProductsController : Controller
    {
        // TempData Attribute to hold messages
        // Note : TempData uses only in MVC Controllers and Razor Pages Not in API Controllers
        [TempData]
        public string MessageAdd { get; set; }
        [TempData]
        public string MessageDelete { get; set; }

        private readonly MVCDbContext _context;

        public WizardProductsController(MVCDbContext context)
        {
            _context = context;
        }

        // GET: WizardProducts
        public async Task<IActionResult> Index()
        {
            var MVCDbContext = _context.Products.Include(p => p.Category);
            //ImagePath = 1.jpg
            //wwwroot/Images/Products/1.jpg
            List<Products> products = new List<Products>();
            foreach (var item in MVCDbContext)
            {
                if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", item.ImagePath)))
                {
                    products.Add(item);
                }
                else
                {
                    item.ImagePath = "";
                    products.Add(item);
                }
            }
            return View(products.ToList());
        }

        // GET: WizardProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            if (!System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", product.ImagePath)))
            {
                product.ImagePath = "";
            }
            return View(product);
        }

        // GET: WizardProducts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: WizardProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ProductId,Name,Price,Description,ImagePath,CategoryId")] Products products)
        public async Task<IActionResult> Create([ModelBinder(typeof(ProductBinder))] Products product , IFormFile ImagePath) 
        {
            //if(products.CategoryId == 0)
            //{
            //    ModelState.AddModelError("CategoryId", "Please select a category.");
            //    ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", products.CategoryId);
            //    return View(products);
            //}
            //if (ModelState.IsValid)
            //{
            //    _context.Add(products);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", products.CategoryId);
            //return View(products);
            // or 

            // Upload the photo file
            string _fileName = string.Empty;
            if (ImagePath != null && ImagePath.Length > 0) // Here we will handle the file upload and saving the productImage in wwwroot/images/products
            {
                //~
                //Naming File on the server is important to avoid overwriting files with same name
                string _Extenstion = Path.GetExtension(ImagePath?.FileName);  //.jpg
                _fileName = DateTime.Now.ToString("yyMMddhhssfff") + _Extenstion;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products", _fileName);
                 
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImagePath.CopyToAsync(stream);
                }
                product.ImagePath = _fileName;
            }
            else
            {
                // Optional: assign a default image if no file was uploaded
                product.ImagePath = "default.png"; // <-- or leave it null if not needed
                ModelState.Remove("ImagePath"); // 👈 clear model validation for missing image
            }
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    // Set TempData message
                    MessageAdd = $"Product {product.Name} added...";
                    TempData.Keep(nameof(MessageAdd)); // nameof() : Get the string name of the variable MessageAdd
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
                return View(product);
            }
            catch (Exception ex)
            {
                // if I make it string.empty so i must make the asp-validation-summary="ModelOnly" in the view
                ModelState.AddModelError(string.Empty, "An error occurred while creating the product: " + ex.Message);
                return View(product);
            }
        }

        //[Authorize] // Only Authenticated Users Can Access this Action Method and i can Make Role base Authorization [Authorize(Roles ="Admin")]
        // GET: WizardProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", products.CategoryId);
            return View(products);
        }

        // POST: WizardProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [ModelBinder(typeof(ProductBinder))] Products product , IFormFile? ImagePath)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }
            //Task 3 : If Old Image Exist , so Delete it first 
            product.ImagePath = _context.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == id)?.ImagePath;
            // Check if a new image file is uploaded
            if(ImagePath != null && ImagePath.Length > 0)
            {
                // Delete the old image file if it exists
                if (!string.IsNullOrEmpty(product.ImagePath))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products", product.ImagePath);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                // Upload the new photo file
                //Naming File on the server is important to avoid overwriting files with same name
                string _Extenstion = Path.GetExtension(ImagePath.FileName);  //.jpg
                string _fileName = DateTime.Now.ToString("yyMMddhhssfff") + _Extenstion;

                //~
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products", _fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImagePath.CopyToAsync(stream);
                }
                product.ImagePath = _fileName;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
                        return View(product);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
            return View(product);
        } 

        // GET: WizardProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: WizardProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);
            }
            // Set TempData message
            MessageDelete = $"Product {products?.Name} deleted...";
            TempData.Keep(nameof(MessageDelete)); // nameof() : Get the string name of the variable MessageDelete
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }

        public async Task<IActionResult> Card(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/products", product.ImagePath)))
            {
                return View(product);
            }
            else
            {
                product.ImagePath = "";
                return View(product);
            }
        }

        //public async Task<IActionResult> Gallery()
        //{
        //    return View(await _context.Products.ToListAsync());
        //}

        // Gallery With Pagination
        public async Task<IActionResult> Gallery(int pageNumber = 1, int pageSize = 3)
        {
            var totalProducts = await _context.Products.CountAsync();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            // Ensure page number is within valid range
            if (pageNumber < 1)
                pageNumber = 1;
            if (pageNumber > totalPages)
                pageNumber = totalPages;

            var products = await _context.Products
                .OrderBy(p => p.ProductId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalProducts = totalProducts;
            return View(products);
        }
    }
}