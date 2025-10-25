using MVCDemoLabpart1.Data;
using MVCDemoLabpart1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVCDemoLabpart1.Controllers
{
    /*
     -Restful conventions for controllers:
        -Index: List all items
        -Details: Show details of a single item
        -Create: Show form to create a new item (GET) and handle form submission (POST)
        -Edit: Show form to edit an existing item (GET) and handle form submission (POST)
        -Delete: Show confirmation to delete an item (GET) and handle deletion (POST)
    -Routing Map :
        -URL : http://<host>:<port>/Controller/Action/Id?param1=value1&param2=value2
        -{controller=Home}/{action=Index}/{id?}
            -controller: The name of the controller (without "Controller" suffix)
            -action: The action method to invoke
            -id: An optional parameter, often used to specify a resource identifier
     */
    public class CategoryController : Controller
    {
        private readonly MVCDbContext _context;

        public CategoryController(MVCDbContext context)
        {
            this._context = context;
        }
        // CRUD Operations : In MVC verbals are HTTP GET or POST

        // GET
        [HttpGet]
        public IActionResult Index()
        {
            // pass data from Controller to View using ViewBag, ViewData, TempData, or Model

            // Model : loosely typed data
            var result = _context.Categories.ToList();
            return View(result);
        }

        // Create 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(category);
                }
                _context.Categories.Add(category);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Category created successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error creating category: {ex.Message}";
                return View(category);
            }
        }

        // Edit 
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit( int id, Category updateCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(updateCategory);
            }
            _context.Entry(updateCategory).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var category = _context.Categories.Find(id);
            return View(category);
        }

        //Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int id)
        {
            var category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
