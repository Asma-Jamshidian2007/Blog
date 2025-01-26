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
            if (result.Status== OperationResultStatus.Success)
            {
                TempData["SuccessMessage"] = "user deleted successfully.";
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = result.ToString();
            return RedirectToAction("Index");
        }
    }
}
