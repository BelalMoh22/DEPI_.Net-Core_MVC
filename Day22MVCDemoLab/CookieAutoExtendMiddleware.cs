//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

//Create a new C# file, e.g. CookieAutoExtendMiddleware.cs:

using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public class CookieAutoExtendMiddleware
{
    private readonly RequestDelegate _next;

    public CookieAutoExtendMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string cookieName = "CompanyName";
        string cookieValue = "Smart software";

        // Check if the cookie exists
        if (context.Request.Cookies.TryGetValue(cookieName, out string existingValue))
        {
            // Cookie exists → refresh it if close to expiry
            // (We can’t read actual expiry, so we just re-append it to keep it alive)
            CookieOptions options = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(5), // extend 5 days
                HttpOnly = true,
                Secure = false // change to true if using HTTPS
            };

            context.Response.Cookies.Append(cookieName, cookieValue, options);
        }
        else
        {
            // Cookie doesn’t exist → create new one for 15 days
            CookieOptions options = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(15),
                HttpOnly = true,
                Secure = false
            };

            context.Response.Cookies.Append(cookieName, cookieValue, options);
        }

        // Continue to the next middleware / controller
        await _next(context);
    }
}