using Blog_System.CoreLayer.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog_System.CoreLayer.DTOs.Users;
using Blog_System.CoreLayer.Utilities.OperationResult;
using System.ComponentModel.DataAnnotations;

namespace Blog_System.WEB.Pages.Auth
{
    [BindProperties]
    [ValidateAntiForgeryToken]
    public class SignUpModel : PageModel
    {
        private readonly IUserService _userService;

        // Constructor injection of IUserService
        public SignUpModel(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "نام کاربری فقط باید شامل حروف انگلیسی و اعداد باشد")]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string FullName { get; set; } = string.Empty;

        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "رمز عبور باید حداقل شامل یک حرف بزرگ، یک عدد و 8 کاراکتر باشد")]
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(8, ErrorMessage = "رمز عبور حداقل باید 8 کاراکتر داشته باشد")]
        public string Password { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = _userService.UserRegister(new UserRegisterDto()
            {
                UserName = UserName,
                FullName = FullName,
                Password = Password
            });

            if (result?.Status == OperationResultStatus.Error)
            {
                ModelState.AddModelError("UserName", result.Message);
                return Page();
            }

            TempData["SuccessMessage"] = "ثبت نام با موفقیت انجام شد!";
            return RedirectToPage("./Login");
        }
    }
}
