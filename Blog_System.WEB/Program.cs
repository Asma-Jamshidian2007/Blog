using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Blog_System.DataLayer.Context;
using Blog_System.CoreLayer.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using Blog_System.CoreLayer.Services.Categories;

namespace Blog_System.WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add essential services
            ConfigureServices(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline
            ConfigureMiddleware(app);

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Add Razor Pages and Controllers
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();
           builder.Services.AddMvc();
            builder.Services.AddControllersWithViews();


            // Register the UserService for dependency injection
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICategoryService,CategoryService>();
            // Configure the DbContext
            builder.Services.AddDbContext<BlogContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configure cookie-based authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
         .AddCookie(options =>
         {
             options.LoginPath = "/Auth/Login";
             options.LogoutPath = "/Auth/Logout";
             options.AccessDeniedPath = "/Auth/AccessDenied";
             options.ExpireTimeSpan = TimeSpan.FromDays(30);
             options.Cookie.HttpOnly = true;
             options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
             options.SlidingExpiration = true;
         });

        }
        private static void ConfigureMiddleware(WebApplication app)
        {
            // Error handling and HTTPS configuration
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Routing and middleware setup
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // Map routes
            app.MapRazorPages();
            app.MapControllers();
            app.MapControllerRoute(
                name: "Default",
                pattern: "{area:exists}/{controller=home}/{action=Index}/{id?}");
        
        }

    }
}
