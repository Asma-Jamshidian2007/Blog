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

        private const string UserNameErrorMessage = "نام کاربری فقط باید شامل حروف انگلیسی و اعداد باشد";
        private const string PasswordErrorMessage = "رمز عبور باید حداقل شامل یک حرف بزرگ، یک عدد و 8 کاراکتر باشد";
        private const string RequiredErrorMessage = "{0} را وارد کنید";
        private const string LoginErrorMessage = "نام کاربری یا رمز عبور اشتباه است";

        public LoginModel(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = UserNameErrorMessage)]
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string UserName { get; set; } = string.Empty;

        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[a-zA-Z]).{8,}$", ErrorMessage = PasswordErrorMessage)]
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Password { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _userService.UserLogin(new LoginUserDto { Password = Password, UserName = UserName });

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, LoginErrorMessage);
                return Page();
            }

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identity);

            var props = new AuthenticationProperties
            {
                IsPersistent = true, 
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) 
            };

            await HttpContext.SignInAsync(claimPrincipal, props);

            return RedirectToPage("../Index");
        }
    }
}
