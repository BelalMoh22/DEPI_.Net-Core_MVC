using Day22MVCDemoLab.Data;
using Day22MVCDemoLab.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVCDemoLabpart1.Controllers
{
    public class DemoCategoryController : Controller
    {
        private readonly MVCDbContext _dbcontext;

        public DemoCategoryController(MVCDbContext dbContext)
        {
            this._dbcontext = dbContext;
        }

        public ActionResult Index()
        {
            // ViewData built on Object Dictionary so it needs casting
            // ViewBag built on Dynamic 

            ViewData["AppName"] = "Welcome in ViewData";
            ViewData.Add("Number1" , 150);
            ViewData.Add("Number2", 250);

            ViewBag.N1 = 200;
            ViewBag.N2 = 300;
            // Model : loosely typed data
            var categories = _dbcontext.Categories.ToList();
            return View("Views/DemoCategory/IndexDemo.cshtml" , categories); // here I specify the view name with extension
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(); // here I specify the view name with extension
        }

        [HttpPost]
        public ActionResult Create(Category newCategory)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = ModelState.IsValid.ToString();
                return View(newCategory);
            }
            _dbcontext.Categories.Add(newCategory);
            _dbcontext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
 