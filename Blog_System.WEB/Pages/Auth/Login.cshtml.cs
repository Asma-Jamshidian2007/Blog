using Blog_System.CoreLayer.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Blog_System.CoreLayer.DTOs.Users;
using Blog_System.CoreLayer.Utilities.OperationResult;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Blog_System.WEB.Pages.Auth
{
    [ValidateAntiForgeryToken]
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly IUserService? _userService;
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage ="{0}را وارد کنید")]
        public string UserName { get; set; }
        [Display(Name = "رمز عبور ")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Password { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            var user = _userService.UserLogin(new LoginUserDto() { 
            Password=Password, UserName=UserName
            });
            if (user == null) {
                ModelState.AddModelError("UserName","اطلاعات ثبت نشده است");
             return Page();
            }
            List<Claim> claims = new List<Claim>()
            {
               new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
               new Claim(ClaimTypes.Name,user.FullName.ToString())
            };
            var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var claimPrincipal = new ClaimsPrincipal(identity);
            var props = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            HttpContext.SignInAsync(claimPrincipal,props);
            return RedirectToPage("../Index");
        }
    }
}
