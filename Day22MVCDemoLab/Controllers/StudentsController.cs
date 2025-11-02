using Microsoft.AspNetCore.Mvc;
using MVCDemoLabpart1.Models;

namespace MVCDemoLabpart1.Controllers
{
    public class StudentsController : Controller
    {

       public IActionResult Index()
        {
            var result = Student.students.ToList();
            return View(result);
        }

        // students/ShowMsgWithPara/100
        public IActionResult ShowMsgWithPara(int id)
        {
            var result = new ContentResult();
            result.Content = $"Welcome Message with id {id}";
            return result;
        }

        // students/ShowMsgWithName?name=belal
        public IActionResult ShowMsgWithName(string Name)
        {
            var result = new ContentResult();
            result.Content = $"Welcome Message with Name {Name}";
            return result;
        }

        // students/BackToIndex
        public IActionResult BackToIndex()
        {
            return RedirectToAction("Index");
        }

        // students/BackToIndexHome
        public IActionResult BackToIndexHome()
        {
            return RedirectToAction("Index" , controllerName: "Home");
        }

        //students/showGoogle
        public IActionResult showGoogle()
        {
            return Redirect("http://www.google.com");
        }

        //students/showYahoo
        public IActionResult showYahoo()
        {
            return RedirectPermanent("http://www.Yahoo.com");
        }

        // 1.Content "String"    => ContentResult
        // Students/SayContent
        public IActionResult SayContent() //ContentResult
        {
            ContentResult result = new ContentResult();
            result.Content= "Hello World From MVC......";
            return result;
        }

        // 2. View"HTML"        => ViewResult
        public IActionResult ShowView() //  ViewResult
        {
            ViewResult result = new ViewResult();
            result.ViewName = "~/Views/Students/MyView.cshtml";
            return result;
        }

        //3- Json               => JsonResult
        public IActionResult ShowJson()
        {
            return Json(new { ID = 1, Name = "Osama", Salary = 20000 });
        }
         
        //4- File               => FileResult
        public IActionResult ShowFile()
        {
            return File("~/Smart Welcome in MVC.txt", "text/plain");
        }

        //5- Empty              => Emptyresult
        public IActionResult ShowEmpty()
        {
            return new EmptyResult(); //or return null;
        }
    }
}
