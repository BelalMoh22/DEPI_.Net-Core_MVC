using Microsoft.AspNetCore.Mvc;

namespace MVCDemoLabpart1.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DemoRouting(int id , string name)
        {
            return Content($"ID :{id} \n Name: {name}");
        }
    }
}
