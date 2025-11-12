using Microsoft.AspNetCore.Mvc;

namespace MVCDemoLabpart1.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "This is TestController Index Action Method";
            return View();
        }

        public double div(double a, double b)
        {
            return a / b;
        }
    }
}