using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Blog_System.DataLayer.Context;
using Blog_System.CoreLayer.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Blog_System.CoreLayer.Services.Categories;
using Blog_System.CoreLayer.Services.Posts;
using Blog_System.CoreLayer.Services.FileManager;
using Blog_System.CoreLayer.Services.Commets;

namespace Blog_System.WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder);

            var app = builder.Build();

            ConfigureMiddleware(app);

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddTransient<IPostService , PostService>();
            builder.Services.AddTransient<IFileManager, FileManager>();
            builder.Services.AddTransient<ICommentService, CommentService>();



            builder.Services.AddDbContext<BlogContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();
            app.MapControllerRoute(
                name: "Default",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{Id?}");
        }
    }
}
