using Microsoft.AspNetCore.Mvc;
using Blog_System.CoreLayer.Services.Users;
using Blog_System.CoreLayer.Utilities.OperationResult;

namespace Blog_System.WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();
            ViewData["Users"] = users;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int userId)
        {
            var result = _userService.Delete(userId);
            var messageKey = result.Status == OperationResultStatus.Success ? "SuccessMessage" : "ErrorMessage";
            var messageValue = result.Status == OperationResultStatus.Success
                ? "The user was successfully removed"
                : result.ToString();

            TempData[messageKey] = messageValue;
            return RedirectToAction("Index");
        }
    }
}
