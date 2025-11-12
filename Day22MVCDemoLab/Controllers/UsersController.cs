using Microsoft.AspNetCore.Mvc;
using MVCDemoLabpart1.Data;
using MVCDemoLabpart1.ViewModels;

namespace MVCDemoLabpart1.Controllers
{
    public class UsersController : Controller
    {
        private readonly MVCDbContext _context;

        public UsersController(MVCDbContext context)
        {
            this._context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            // Perform login logic here
            var Loggeduser = _context.Users.FirstOrDefault(u => u.UserName == user.UserName 
            && u.Password == user.Password);
            if (Loggeduser == null)
            {
                //ModelState.AddModelError(string.Empty, "Invalid username or password");
                ViewBag.ErrorLogin = "Invalid username or password";
                return View(user);
            }
            Response.Cookies.Append("UserName", user.UserName);
            // If successful, redirect to a different action
            return RedirectToAction("Index", controllerName: "Sites");
        }
    }
}
