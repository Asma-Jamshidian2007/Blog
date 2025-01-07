using Blog_System.CoreLayer.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog_System.CoreLayer.DTOs.Users;
using Blog_System.CoreLayer.Utilities.OperationResult;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace Blog_System.WEB.Pages.Auth
{
    [BindProperties]
    [ValidateAntiForgeryToken]
    public class SignUpModel : PageModel
    {
        private readonly IUserService? _userService;
        [Display(Name = "نام کاربری")]
        [Required (ErrorMessage ="{0} را وارد کنید")]
        public string UserName { get; set; } = string.Empty;
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string FullName { get; set; } = string.Empty;
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(8,ErrorMessage ="رمز عبور حداق باید 8 کاراکتر داشته باشد")]
        public string Password { get; set; } = string.Empty;
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) { 
            return Page();
            }
            var result = _userService?.UserRegister(new UserRegisterDto() { 
            UserName = UserName,
            FullName = FullName,
            Password = Password
            });
            if (result?.Status == OperationResultStatus.Error)
            {
                ModelState.AddModelError("UserName",result.Message);
                return Page();
            }
            return RedirectToAction("Index");
        }
        
    }

}
