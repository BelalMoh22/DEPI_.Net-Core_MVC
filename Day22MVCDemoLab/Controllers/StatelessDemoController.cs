using Microsoft.AspNetCore.Mvc;

namespace MVCDemoLabpart1.Controllers
{
    public class StatelessDemoController : Controller
    {
        //Query String: is a part of a URL that contains data to be passed to web applications.
        public IActionResult Index()
        {
            var name = HttpContext.Request.Query["name"];

            return Content("Hello " + name);
        }
        //=========================================================================================
        // TempData: is used to store data temporarily between requests.
        // it uses session state to store the data, but the data is only available for the next request.
        public IActionResult SetTempData()
        {
            TempData["AppName"] = "MVC Demo Application";
            return Content("Save Data Into TempData");
        }

        // here TempData value will be removed after read once
        //public IActionResult GetFirst()
        //{
        //    string name = "Empty Name ";
        //    if (TempData.ContainsKey("AppName"))
        //    {
        //        //Normal read : Here TempData value will be removed after read
        //        name = TempData["AppName"].ToString();
        //    }
        //    return Content(" get Data " + name + " Please check cookies ...");
        //}

        // Peek and Keep methods to retain TempData values for subsequent requests
        /*
          2. Keep() vs Peek()
            Feature	TempData.Keep()	                                        |                TempData.Peek()
            Purpose :	Keeps specific or all TempData items for the next request |	Reads a TempData item without marking it for deletion
            How it works :	Prevents data from being deleted after it’s read  |	Retrieves the value without removing it from TempData
            When used :	When you want to read TempData and still keep it for the next request |	When you just want to read a value without consuming it
            Example :	TempData.Keep("Message");   |	var msg = TempData.Peek("Message");
            Effect :	Data will survive one more request even if already   | read	Data remains until explicitly removed or session ends
         */
        public IActionResult GetFirst()
        {
            string name = "Empty Name ";
            if (TempData.ContainsKey("AppName"))
            {
                name = TempData.Peek("AppName").ToString();  // Peek : Here TempData value will NOT be removed after read
                //TempData.Keep(); // Keep : Here All TempData values will NOT be removed after read
                //TempData.Keep("AppName"); // Keep : Here TempData value will NOT be removed after read
            }
            return Content(" get Data " + name + " Please check cookies From GetFirst Method ...");
        }

        // here I cannot read TempData value as it is already read in previous request(GetFirst) but I can use Keep or peek  to retain the value for next request
        public IActionResult GetSecond()
        {
            string name = "No Name";
            if (TempData.ContainsKey("AppName"))
            {
                name = TempData["AppName"].ToString();
            }
            return Content(" get Data " + name + " Please check cookies From GetSSecond Method ...");
        }
//==============================================================================

        // Cookies : is a small piece of data that is stored on the client-side (browser) and sent to the server with each request.
        public IActionResult SetCookies()
        {
            Response.Cookies.Append("AppName", "Smart software");  //Session Cookies 20 Min
            Response.Cookies.Append("Number", "120");

            return Content("Cookies Saving ....");
        }

        public IActionResult GetCookies()
        {
            string appName = Request.Cookies["AppName"];
            int Number = int.Parse(Request.Cookies["Number"]);

            return Content($"Cookies:{appName} & {Number}");
        }

        //Persistent Cookies: is stored on the client-side (browser) with an expiration date.
        //public IActionResult SetCookiesPersistent()
        //{
        //    CookieOptions cookieOptions = new CookieOptions(); // Creating CookieOptions object to set the expiration
        //    cookieOptions.Expires = DateTimeOffset.Now.AddDays(15); // Setting expiration date to 15 days from now
        //    //if condition if the cookie close to be end add 5 days -> Task

        //    //cookieOptions.Expires = DateTimeOffset.Now.AddDays(-1); // To Remove Cookie
        //    Response.Cookies.Append("CompanyName", "Smart software", cookieOptions);
        //    return Content("Cookies Persistent Saving ....");
        //}
        public IActionResult SetCookiesPersistent()
        {
            /* 
             Why we store expiry inside the cookie
            ASP.NET doesn’t send the expiration date back with cookies — the server can’t know it.
            So, we save the date inside the cookie value itself to check it later.
             */
            string cookieName = "CompanyName";
            string cookieValue = "Smart software";
            int daysToExpire = 15; // default expiration
            int extendThresholdDays = 3; // if 3 days or less left, extend by 5

            // Try to read existing cookie
            if (Request.Cookies.TryGetValue(cookieName, out string existingValue))
            {
                // Cookie exists — simulate checking remaining time.
                // Note: Cookies themselves don’t store expiration info on the client.
                // So we can store the expiration date *inside* the cookie value itself.
                var parts = existingValue.Split('|');
                if (parts.Length == 2 && DateTimeOffset.TryParse(parts[1], out DateTimeOffset expiry))
                {
                    // Check if expiry is within 3 days
                    if ((expiry - DateTimeOffset.Now).TotalDays <= extendThresholdDays)
                    {
                        expiry = expiry.AddDays(5); // extend 5 more days

                        CookieOptions options = new CookieOptions
                        {
                            Expires = expiry
                        };

                        // Update cookie with same company name + new expiry info
                        Response.Cookies.Append(cookieName, $"{parts[0]}|{expiry}", options);
                        return Content("Cookie was near expiry, extended 5 more days!");
                    }
                    else
                    {
                        return Content("Cookie still valid, no extension needed.");
                    }
                }
            }

            // Cookie doesn’t exist — create a new one
            DateTimeOffset newExpiry = DateTimeOffset.Now.AddDays(daysToExpire);
            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = newExpiry
            };

            // Save company name and expiry inside cookie value
            Response.Cookies.Append(cookieName, $"{cookieValue}|{newExpiry}", cookieOptions);
            return Content("Cookies Persistent Saving ....");
        }


        public IActionResult RemoveCookies()
        {
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTimeOffset.Now.AddDays(-1);
            Response.Cookies.Append("CompanyName", "Smart software", cookieOptions);
            return Content("Cookies Remove ....");
        }
//===============================================================================================]
        // Session : is a way to store user data while the user is browsing your web application where the data is stored on the server and a unique identifier is sent to the client as a cookie.
        // - To use session in ASP.NET Core MVC, you need to configure it in the Program.cs file by adding services.AddSession() in the service container and app.UseSession() in the middleware pipeline.
        //- Session data can be set and retrieved using the HttpContext.Session property.
        //- Sessions can store only string and integer values directly. For other data types, you need to serialize them before storing.
        //- Session data is temporary and will be lost when the session expires or the user closes the browser. 
        // - Session store data on the server side not in the client , while cookies store data on the client side.

        public IActionResult SetSession()
        {
            HttpContext.Session.SetString("name", "Sayed");
            HttpContext.Session.SetInt32("Counter", 100);
            return Content("Save Session ");
        }
        public IActionResult GetSession()
        {
            string name = HttpContext.Session.GetString("name");
            int? counter = HttpContext.Session.GetInt32("Counter");
            return Content($"Name {name} & Counter {counter}  ");
        }
    }
}
