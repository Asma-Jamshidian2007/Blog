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

        private const string RequiredErrorMessage = "{0} را وارد کنید";
        private const string UserNameErrorMessage = "نام کاربری فقط باید شامل حروف انگلیسی و اعداد باشد";
        private const string PasswordErrorMessage = "رمز عبور باید حداقل شامل یک حرف بزرگ، یک عدد و 8 کاراکتر باشد";
        private const string PasswordMinLengthErrorMessage = "رمز عبور حداقل باید 8 کاراکتر داشته باشد";
        private const string UserRegisterErrorMessage = "ثبت نام ناموفق بود. لطفاً دوباره تلاش کنید.";

        public SignUpModel(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = UserNameErrorMessage)]
        public string UserName { get; set; } = string.Empty;

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = RequiredErrorMessage)]
        public string FullName { get; set; } = string.Empty;

        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[a-zA-Z]).{8,}$", ErrorMessage = PasswordErrorMessage)]
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = RequiredErrorMessage)]
        [MinLength(8, ErrorMessage = PasswordMinLengthErrorMessage)]
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
