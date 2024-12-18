using Blog_System.CoreLayer.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog_System.CoreLayer.DTOs;

namespace Blog_System.WEB.Pages.Auth
{
    public class LoginSignupModel : PageModel
    {
        private readonly IUserService _userService;
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            var result = _userService.UserRegister(new CoreLayer.DTOs.Users.UserRegisterDto() { 
            
            });
            return RedirectToAction("Index");
        }
    }
}
