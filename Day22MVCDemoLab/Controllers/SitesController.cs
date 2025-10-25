using Microsoft.AspNetCore.Mvc;

namespace MVCDemoLabpart1.Controllers
{
    public class SitesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
