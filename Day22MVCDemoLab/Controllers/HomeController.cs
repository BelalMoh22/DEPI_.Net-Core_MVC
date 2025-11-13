using System.Diagnostics;
using MVCDemoLabpart1.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVCDemoLabpart1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("smart/{id:int?}/{name:alpha?}")]
        //[HttpGet("smart/{id:int?}/{name:alpha?}")]
        public IActionResult GetNumber(int id , string name)
        {
            return Content($"Client sent number: {id} , Name is {name}");
        }
    }
}
