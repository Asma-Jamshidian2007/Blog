using Blog_System.CoreLayer.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Blog_System.CoreLayer.DTOs.Users;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Blog_System.WEB.Pages.Auth
{
    [ValidateAntiForgeryToken]
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        // Constructor injection of IUserService
        public LoginModel(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "نام کاربری فقط باید شامل حروف انگلیسی و اعداد باشد")]
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string UserName { get; set; } = string.Empty;

        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "رمز عبور باید حداقل شامل یک حرف بزرگ، یک عدد و 8 کاراکتر باشد")]
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Password { get; set; } = string.Empty;

        public void OnGet()
        {
            // This method handles GET requests; no specific logic is needed here for login.
        }

        public IActionResult OnPost()
        {
            // Validate the model (check if UserName and Password are provided)
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Attempt to log in the user using the provided credentials
            var user = _userService.UserLogin(new LoginUserDto { Password = Password, UserName = UserName });

            // If the user is not found, add a model error and return to the login page
            if (user == null)
            {
                ModelState.AddModelError("Password", "نام کاربری یا رمز عبور اشتباه است");
                return Page();
            }

            // Create claims for the logged-in user
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            // Create a claims identity and principal
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identity);

            // Configure authentication properties (e.g., persistent cookies)
            var props = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Cookie expiration time
            };

            // Sign in the user with the claims principal and authentication properties
            HttpContext.SignInAsync(claimPrincipal, props);

            // Redirect to the home page after successful login
            return RedirectToPage("../Index");
        }
    }
}
