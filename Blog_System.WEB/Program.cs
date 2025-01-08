using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Blog_System.DataLayer.Context;
using Blog_System.CoreLayer.Services.Users;

namespace Blog_System.WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create the web application builder
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container for Razor Pages
            builder.Services.AddRazorPages();

            // Register the UserService for dependency injection
            builder.Services.AddScoped<IUserService, UserService>();

            // Configure the DbContext to use SQL Server with the connection string from configuration
            builder.Services.AddDbContext<BlogContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configure authentication using Cookies
            builder.Services.AddAuthentication(options =>
            {
                // Set the default schemes for authentication, challenge, and sign-in
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                // Define paths for login and logout
                options.LoginPath = "/Auth/Login"; // Path to the login page
                options.LogoutPath = "/Auth/Logout"; // Path to the logout page

                // Set the expiration time for authentication cookies
                options.ExpireTimeSpan = TimeSpan.FromDays(30); // Cookies expire in 30 days
            });

            // Build the application
            var app = builder.Build();

            // Configure the HTTP request pipeline for non-development environments
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error"); // Handle exceptions in production
                app.UseHsts(); // Use HTTP Strict Transport Security
            }

            // Ensure HTTPS redirection and serve static files
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Set up routing for the app
            app.UseRouting();

            // Enable authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Map Razor Pages to the request pipeline
            app.MapRazorPages();

            // Run the application
            app.Run();
        }
    }
}
