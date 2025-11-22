using Microsoft.AspNetCore.Mvc;

namespace MVCDemoLabpart1.Controllers
{
    public class TestOneController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetString("session1", "welcome in My site Until Brower Closes");
            //send data Between
            TempData["FullRequest"] = "TempData";
            ViewData["ViewDataVal"] = "View Data ";
            ViewBag.viewbagval = "View bag ";

            /*
             here Session will go to Show action method and the DefaultController Index action method
            but TempData Will go to Show action method only
            and ViewData and ViewBag will not go to any action method because they are only for the current request

            so , Session transfer data between multiple requests from Method to Method or Page to Page or from Controller to Controller
            TempData transfer data between two requests from Method to Method only in the same controller
            ViewData and ViewBag transfer data only for the current request from Method to View only      
             */
            return RedirectToAction("Show");
        }

        public IActionResult Show()
        {
            return View();
        }
    }
}