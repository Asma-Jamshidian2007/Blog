using Blog_System.CoreLayer.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Blog_System.CoreLayer.DTOs.Users;
using Blog_System.CoreLayer.Utilities.OperationResult;
using System.ComponentModel.DataAnnotations;

namespace Blog_System.WEB.Pages.Auth
{
    // Handles the SignUp page logic
    [BindProperties]
    [ValidateAntiForgeryToken]
    public class SignUpModel : PageModel
    {
        // Dependency injection of the user service to handle registration logic
        private readonly IUserService? _userService;

        // Username input for user registration (only letters and numbers allowed)
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "نام کاربری فقط باید شامل حروف انگلیسی و اعداد باشد")]
        public string UserName { get; set; }

        // Full name input for the user (required field)
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string FullName { get; set; }

        // Password input for the user (minimum 8 characters required)
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(8, ErrorMessage = "رمز عبور حداقل باید 8 کاراکتر داشته باشد")]
        public string Password { get; set; }

        // OnGet is used to handle the page load for the SignUp page
        public void OnGet()
        {
        }

        // OnPost is used to handle the form submission for user registration
        public IActionResult OnPost()
        {
            // If the model is invalid, return the page with validation errors
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Call the user service to register the user
            var result = _userService?.UserRegister(new UserRegisterDto()
            {
                UserName = UserName,
                FullName = FullName,
                Password = Password
            });

            // If there is an error, add the error message to the ModelState and return the page
            if (result?.Status == OperationResultStatus.Error)
            {
                ModelState.AddModelError("UserName", result.Message);
                return Page();
            }

            // If registration is successful, redirect to the Login page
            return RedirectToPage("./Login");
        }
    }
}
