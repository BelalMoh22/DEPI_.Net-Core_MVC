 using Microsoft.EntityFrameworkCore;

namespace MVCDemoLabpart1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Registering the WebApplication builder(Service container)
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext service
            builder.Services.AddDbContext<Data.MVCDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();
           // builder.Services.AddRazorPages().AddSessionStateTempDataProvider(); // To use TempData with session state provider by default Razor Pages use cookie based TempData provider
           // builder.Services.AddControllersWithViews().AddSessionStateTempDataProvider(); // To use TempData with session state provider by default MVC use cookie based TempData provider
            builder.Services.AddSession(config =>
            {
                config.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout default is 20 minutes
            });

            // Dependency Injection for ServiceCategory and Repository and UnitOfWork
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IServiceCategory, ServiceCategory>();

            var app = builder.Build();

            //Middleware :  it is a software that is assembled into an application pipeline to handle requests and responses.
            // Add this line BEFORE app.UseRouting() or app.MapControllers()
            //app.UseMiddleware<CookieAutoExtendMiddleware>();

            // Note : Any thing in middleware functions start with Use means that it execute some code and then call the next middleware in the pipeline.
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); // Adds HTTP Strict Transport Security (HSTS) headers to responses.
            }

            app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS.
            app.UseRouting(); // Adds route matching to the middleware pipeline.

            app.UseAuthorization(); // Adds authorization capabilities to the middleware pipeline.

            // Session : it is a way to store user data while the user is browsing your web application where the data is stored on the server and a unique identifier is sent to the client as a cookie.
            app.UseSession(); // Enables session state for the application.

            // Any thing in middleware functions start with Map means that it is take a specified path and then execute some code.
            app.MapStaticAssets(); // Custom extension method to map static assets (access files that in the wwwroot folder)

            /* MapControllerRoute : it is used to define a route for controller actions in an ASP.NET Core MVC application.
            consists of a name and a URL pattern that determines how incoming requests are routed to specific controllers and actions.
            name: A unique identifier for the route used to reference it in the application.
            pattern: A URL pattern that defines the structure of the incoming request URL.
             */
            app.MapControllerRoute( // Defines a route for controller actions
                name: "default",
                //pattern: "{controller=Users}/{action=Login}/{id?}")
                //pattern: "{controller=Sites}/{action=Index}/{id:int?}/{Name:alpha?}")
                pattern: "{controller=Sites}/{action=Index}/{id:int?}")
                //pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets(); // Custom extension method to associate static assets with the route

            /* https://learn.microsoft.com:90/home/index/90
             1- http Protocol Request : Client (Browser) sends Http Request to the Server (Web Application)
             2- World Wide Web Server (IIS, Apache, Nginx, etc) : The server receives the request and forwards it to the ASP.NET Core application.
            3- Domain : The domain name in the URL is used to identify the web application.
            4- Domain Type : The type of domain (e.g., .com, .org, .net) is part of the URL structure.
            5.Port Number : The port number (e.g., 80 for HTTP, 443 for HTTPS) is used to establish the connection.
            6- Controller Name : The controller processes the request and determines the appropriate action to take.
            7- Action Method : The action method within the controller executes the logic to handle the request.
            8- Routing Values : Additional parameters in the URL are used to pass data to the action method.
            9- Parameters : The parameters are extracted from the URL and passed to the action method. (after the ?)
            */

            #region Custom Middleware
            //// Use
            //app.Use(async (HttpContext, next) =>
            //{
            //    // Custom Middleware logic before the next middleware
            //    await HttpContext.Response.WriteAsync("1) Write From MiddleWare Request....\n");
            //    await next.Invoke(); // Call the next middleware in the pipeline

            //    // When RollBack As Response  : here it will not execute the below line it first go to the next middleware and after that it will come back to this middleware and execute the below line.
            //    await HttpContext.Response.WriteAsync("5) Write From MiddleWare Response....\n");
            //});

            //app.Use(async (HttpContext, next) =>
            //{
            //    // Custom Middleware logic before the next middleware
            //    await HttpContext.Response.WriteAsync("2) Write From MiddleWare Request....\n");
            //    await next(); // Call the next middleware in the pipeline

            //    // When RollBack As Response  : here it will not execute the below line it first go to the next middleware and after that it will come back to this middleware and execute the below line.
            //    await HttpContext.Response.WriteAsync("4) Write From MiddleWare Response....\n");
            //});
            ////Map
            //app.Map("/Home/Index", apiApp =>
            //{
            //    apiApp.Use(async (context, next) => // context represents the HttpContext for the current request
            //    {
            //        // Custom Middleware logic before the next middleware
            //        await context.Response.WriteAsync(" Request Mapped Middleware for /Home/Index Path\n");
            //        await next(); // Call the next middleware in the pipeline
            //        await context.Response.WriteAsync(" Response Mapped Middleware for /Home/Index Path\n");
            //    });
            //    apiApp.Run(async context =>
            //    {
            //        // Terminal Middleware logic for the mapped path
            //        await context.Response.WriteAsync("Request Running Terminal Mapped Middleware for /Home/Index Path\n");
            //    }); // This Run will be the terminal middleware for this mapped path not the run below 
            //});

            //// Terminal Middleware (Run)
            //app.Run(async (HttpContext) =>
            //{
            //    // Custom Middleware logic before the next middleware
            //    await HttpContext.Response.WriteAsync("3)Running Write From MiddleWare Request....\n");
            //});
            #endregion

            // Any thing in middleware functions start with Run means that it is the terminal middleware in the pipeline.
            app.Run(); // Runs the application
        }
    }
}
