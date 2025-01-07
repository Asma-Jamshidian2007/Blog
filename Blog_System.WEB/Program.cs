using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Blog_System.DataLayer.Context;
using Blog_System.CoreLayer.Services.Users;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace Blog_System.WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddDbContext<BlogContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("Server=.;Database=BlogDB;Trusted_Connection=True;")));
            var app = builder.Build();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
            });


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
